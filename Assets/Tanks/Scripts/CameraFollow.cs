using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject m_target;
    private Vector3 m_offset;
    private float damping = 3f;

	// Use this for initialization
	void Start () {
        m_offset = new Vector3(0f, 1f, -3f);
	}
	
	void LateUpdate () {

        if (m_target == null)
            return;

        Quaternion desiredRotation = Quaternion.Euler(0, m_target.transform.eulerAngles.y, 0);

        Vector3 currentPosition = transform.position;
        Vector3 desiredPosition = m_target.transform.position + (desiredRotation * m_offset);

        transform.position = Vector3.Lerp(currentPosition, desiredPosition, Time.deltaTime * damping);

        transform.LookAt(m_target.transform);

	}
}
