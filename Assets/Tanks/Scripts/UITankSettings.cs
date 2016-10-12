using UnityEngine;

public class UITankSettings : MonoBehaviour {

    
    public GameObject m_preveiwPrefab;
    private GameObject m_preveiw;
    private GameObject m_tank;

    [SerializeField]
    private GameObject m_tankPrefab;
    [SerializeField]
    private TankSettings m_settings;

    

    void OnEnable()
    {
        m_preveiw = (GameObject)Instantiate(m_preveiwPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
        m_settings.CreateSnapshot();
        RebuildTank();
    }

    private void RebuildTank()
    {
        Destroy(m_tank);
        m_tank = (GameObject)Instantiate(m_tankPrefab, m_preveiw.transform);
        m_tank.transform.localPosition = Vector3.zero;
        m_tank.transform.localRotation = Quaternion.identity;
        m_tank.GetComponent<TankBuild>().Build(m_settings.m_selectedBodyIndex, m_settings.m_selectedGunIndex);
    }

    void OnDisable()
    {
        if (m_preveiw != null)
            Destroy(m_preveiw);
    }

    public void ShiftBody(bool forward)
    {
        m_settings.ShiftBody(forward);
        RebuildTank();
    }

    public void ShiftGun(bool forward)
    {
        m_settings.ShiftGun(forward);
        RebuildTank();
    }

    public void HideUiTankSettings ()
    {
        gameObject.SetActive(false);
    }

    public void RestoreTank()
    {
        m_settings.RestoreSnapshot();
        RebuildTank();
    }

}
