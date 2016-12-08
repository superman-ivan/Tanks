using UnityEngine;
using UnityEngine.Networking;

public class TankBuild : NetworkBehaviour
{

    [SyncVar]
    private int m_bodyId;
    [SyncVar]
    private int m_gunId;
    [SyncVar]
    private bool m_readyToBuild = false;

    public TankSettings m_settings;
    
    private Transform m_cabinPosition;
    private TankControl m_tankControl;

    void Awake()
    {
        m_tankControl = GetComponent<TankControl>();
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            int myBodyId = m_settings.m_selectedBodyIndex;
            int myGunId = m_settings.m_selectedCabin;
            CmdUpdate(myBodyId, myGunId);
        }
        else if (!isServer && m_readyToBuild)
            Build(m_bodyId, m_gunId);
            
    }

    [Command]
    void CmdUpdate(int bodyId, int gunId)
    {
        m_bodyId = bodyId;
        m_gunId = gunId;
        RpcBuild(m_bodyId, m_gunId);
        if (!isClient)
            Build(m_bodyId, m_gunId);
        m_readyToBuild = true;
    }

    [ClientRpc]
    void RpcBuild(int bodyId, int gunId)
    {
        Build(bodyId, gunId);
    }


    public void Build(int bodyId, int gunId)
    {
        BuildBody(bodyId);
        BuildCabin(gunId);
    }

    private void BuildBody(int id)
    {
        GameObject body = (GameObject)Instantiate(
            m_settings.m_tankBodies[id],
            transform.position,
            transform.rotation,
            transform);

        TankBody bodyProperties = body.GetComponent<TankBody>();
        this.GetComponent<Rigidbody>().mass = bodyProperties.mass;

        BoxCollider bodyColider = body.GetComponent<BoxCollider>();
        BoxCollider tankColider = gameObject.AddComponent<BoxCollider>();

        tankColider.size = bodyColider.size;
        tankColider.center = bodyColider.center;

        bodyColider.enabled = false;

        if (bodyProperties == null)
            throw new MissingComponentException("Tank Body Prefab must have Tank Body script component");

        m_tankControl.m_moveSpeed = bodyProperties.moveSpeed;
        m_tankControl.m_rotateSpeed = bodyProperties.rotateSpeed;

        m_cabinPosition = body.transform.FindChild("CabinPosition");
        if (m_cabinPosition == null)
            throw new MissingComponentException("No CabinPosition child found on Tank Body");
    }

    
    private void BuildCabin(int id)
    {
        GameObject cabin = (GameObject)Instantiate(m_settings.m_tankCabins[id], m_cabinPosition.position, transform.rotation, transform);

        Transform gun = cabin.transform.FindChild("Gun");

        if (gun == null)
            throw new MissingComponentException("No Gun child found on Tank Cabin");

        m_tankControl.m_gun = gun;

        Transform bulletSpawn = gun.FindChild("BulletSpawn");

        if (bulletSpawn == null)
            throw new MissingComponentException("No BulletSpawn child found on Tank Gun");

        m_tankControl.m_bulletSpawn = bulletSpawn;

        TankGun gunProperties = cabin.GetComponent<TankGun>();

        if (gunProperties == null)
            throw new MissingComponentException("Gun Prefab must have TankGun script component");

        m_tankControl.SetAimLimits(gunProperties.minAngle, gunProperties.maxAngle);
        m_tankControl.m_launchPower = gunProperties.launchPower;
    }
    
}