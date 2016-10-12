using UnityEngine;

public class UILevelSettings : MonoBehaviour {

    public GameObject m_preveiwPrefab;
    private GameObject m_preveiw;
    private GameObject m_level;

    [SerializeField]
    private LevelSettings m_settings;

    void OnEnable()
    {
        m_preveiw = (GameObject)Instantiate(m_preveiwPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
        m_settings.CreateSnapshot();
        RecreateLevel();
    }

    void OnDisable()
    {
        if (m_preveiw != null)
            Destroy(m_preveiw);
    }

    public void HideUiLevelSettings()
    {
        gameObject.SetActive(false);
    }

    public void ShiftLevel(bool forward)
    {
        m_settings.ShiftLevel(forward);
        RecreateLevel();
    }

    public void RestoreLevel ()
    {
        m_settings.RestoreSnapshot();
        RecreateLevel();
    }

    private void RecreateLevel()
    {
        if (m_level != null)
            Destroy(m_level);

        m_level = (GameObject)Instantiate(m_settings.LevelPrefab, m_preveiw.transform);
    }
}
