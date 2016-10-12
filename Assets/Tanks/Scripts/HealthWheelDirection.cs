using UnityEngine;
using System.Collections;

public class HealthWheelDirection : MonoBehaviour {

    public static bool m_useRelativeRotation = true;

    private Quaternion m_RelativeRotation;

	// Use this for initialization
	void Start () {
        m_RelativeRotation = transform.parent.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (m_useRelativeRotation)
            transform.rotation = m_RelativeRotation;
	}
}
