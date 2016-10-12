using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;
using System;


public class TankControl : NetworkBehaviour
{

    public GameObject m_bulletPrefab;

    [HideInInspector]
    public Transform m_bulletSpawn;
    [HideInInspector]
    public Transform m_gun;
    [HideInInspector]
    public int m_launchPower = 10;
    [HideInInspector]
    public int m_moveSpeed = 1;
    [HideInInspector]
    public int m_rotateSpeed = 1;

    public References m_ref;

    private int m_maxAimAngle = 30;
    private int m_minAimAngle = 0;

    [SyncVar(hook = "OnAimAngleChange")]
    private float m_aimAngle;

    private float m_moveInput;
    private float m_rotateInput;
    private float m_aimInput;

    public bool m_isPractice = false;

    private float NormalizeAngle(float angle)
    {
        angle = angle % 360;
        if (angle > 180)
            angle -= 360;
        else if (angle <= -180)
            angle += 360;

        return angle;
    }

    public void SetAimLimits(int minAim, int maxAim)
    {
        m_maxAimAngle = Convert.ToInt32(NormalizeAngle(maxAim));
        m_minAimAngle = Convert.ToInt32(NormalizeAngle(minAim));
    }

    private void StandaloneUpdate()
    {
        m_moveInput = Input.GetAxis("Vertical") * Time.deltaTime;
        m_rotateInput = Input.GetAxis("Horizontal") * Time.deltaTime;
        m_aimInput = Input.GetAxis("Aim") * Time.deltaTime;

        if (Input.GetButtonDown("Fire1"))
        {
            CmdFire(m_launchPower);
        }
    }

    void Start()
    {
        m_ref = GameObject.Find("References").GetComponent<References>();

        if (!isLocalPlayer)
            return;

        if (m_ref.regularCamera.activeSelf)
            m_ref.regularCamera.GetComponent<CameraFollow>().m_target = gameObject;
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (!CrossPlatformInputManager.AxisExists("MoveForward"))
        {
            StandaloneUpdate();
            return;
        }
            

        m_moveInput = CrossPlatformInputManager.GetAxis("MoveForward") * Time.deltaTime;
        m_rotateInput = CrossPlatformInputManager.GetAxis("Rotate") * Time.deltaTime;
        m_aimInput = CrossPlatformInputManager.GetAxis("Aim") * Time.deltaTime;

        if (CrossPlatformInputManager.GetButtonDown("Fire"))
            CmdFire(m_launchPower);
    }

    [Command]
    private void CmdFire(int launchPower)
    {
        var bullet = (GameObject)Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * m_launchPower;

        NetworkServer.Spawn(bullet);

        Destroy(bullet, 2.0f);
    } 

    void FixedUpdate()
    {
        if (!isLocalPlayer && !m_isPractice)
            return;

        if (Mathf.Abs(m_rotateInput) > 0)
            Rotate();

        if (Mathf.Abs(m_moveInput) > 0)
            Move();

        if (Mathf.Abs(m_aimInput) > 0)
            Aim();
    }

    void Move()
    {
        Vector3 move = new Vector3(0, 0, m_moveInput * m_moveSpeed);
        transform.Translate(move);
    }

    void Rotate()
    {
        Vector3 rotate = new Vector3(0f, m_rotateInput * 50 * m_rotateSpeed);
        transform.Rotate(rotate);
    }

    void Aim()
    {
        Vector3 aimVector = m_gun.rotation.eulerAngles;

        float currentAngle = NormalizeAngle(-aimVector.x);

        float aimAngle = NormalizeAngle(currentAngle + m_aimInput * 30);

        if (aimAngle > m_maxAimAngle)
            aimAngle = m_maxAimAngle;
        else if (aimAngle < m_minAimAngle)
            aimAngle = m_minAimAngle;

        aimAngle = -aimAngle;
        m_gun.rotation = Quaternion.Euler(aimAngle, aimVector.y, aimVector.z);

        CmdAim(aimAngle);
    }

    [Command]
    private void CmdAim(float aimAngle)
    {
        m_aimAngle = aimAngle;
    }

    private void OnAimAngleChange (float aimAngle)
    {
        if (isLocalPlayer)
            return;

        Vector3 aimVector = m_gun.rotation.eulerAngles;
        m_gun.rotation = Quaternion.Euler(aimAngle, aimVector.y, aimVector.z);

    }


}
