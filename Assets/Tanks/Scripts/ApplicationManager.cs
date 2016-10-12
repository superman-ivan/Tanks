using UnityEngine;

public class ApplicationManager : MonoBehaviour
{

    [SerializeField]
    private GameObject m_uiSettings;
    [SerializeField]
    private GameObject m_uiControls;

    private GameObject m_cameraStaticPosition;

    public References m_ref;

    public bool playMode
    {
        set
        {
            m_uiSettings.SetActive(!value);
            m_uiControls.SetActive(value);

            if (!value)
                ResetCamera();
        }
    }

    public bool aR
    {
        set
        {
            m_ref.regularCamera.SetActive(!value);
            m_ref.arCamera.SetActive(value);
            m_ref.arTarget.SetActive(value);
        }
    }

    void Awake()
    {
        m_cameraStaticPosition = new GameObject("CameraStaticPosition");
        m_cameraStaticPosition.transform.position = m_ref.regularCamera.transform.position;
        m_cameraStaticPosition.transform.rotation = m_ref.regularCamera.transform.rotation;
    }

    private void ResetCamera()
    {
        m_ref.regularCamera.GetComponent<CameraFollow>().m_target = null;
        m_ref.regularCamera.transform.position = m_cameraStaticPosition.transform.position;
        m_ref.regularCamera.transform.rotation = m_cameraStaticPosition.transform.rotation;
    }

}
