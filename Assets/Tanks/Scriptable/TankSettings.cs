using UnityEngine;

[CreateAssetMenu(menuName = "Tanks/TankSettings")]
public class TankSettings : ScriptableObject
{

    public GameObject[] m_tankBodies;
    public int m_selectedBodyIndex = 0;

    public GameObject[] m_tankGuns;
    public int m_selectedGunIndex = 0;

    private int m_snapshotBodyIndex = 0;
    private int m_snapshotGunIndex = 0;

    public void CreateSnapshot()
    {
        m_snapshotBodyIndex = m_selectedBodyIndex;
        m_snapshotGunIndex = m_selectedGunIndex;
    }

    public void RestoreSnapshot()
    {
        m_selectedBodyIndex = m_snapshotBodyIndex;
        m_selectedGunIndex = m_snapshotGunIndex;
    }

    public void ShiftBody(bool forward)
    {
        if (m_tankBodies.Length == 0)
            return;

        int step = forward ? 1 : -1;

        m_selectedBodyIndex = (m_tankBodies.Length + m_selectedBodyIndex + step) % m_tankBodies.Length;
    }

    public void ShiftGun(bool forward)
    {
        if (m_tankGuns.Length == 0)
            return;

        int step = forward ? 1 : -1;

        m_selectedGunIndex = (m_tankGuns.Length + m_selectedGunIndex + step) % m_tankGuns.Length;
    }

}
