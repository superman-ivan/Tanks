using UnityEngine;

[CreateAssetMenu(menuName = "Tanks/TankSettings")]
public class TankSettings : ScriptableObject
{

    public GameObject[] m_tankBodies;
    public int m_selectedBodyIndex = 0;

    public GameObject[] m_tankCabins;
    public int m_selectedCabin = 0;

    private int m_snapshotBodyIndex = 0;
    private int m_snapshotCabinIndex = 0;

    public void CreateSnapshot()
    {
        m_snapshotBodyIndex = m_selectedBodyIndex;
        m_snapshotCabinIndex = m_selectedCabin;
    }

    public void RestoreSnapshot()
    {
        m_selectedBodyIndex = m_snapshotBodyIndex;
        m_selectedCabin = m_snapshotCabinIndex;
    }

    public void ShiftBody(bool forward)
    {
        if (m_tankBodies.Length == 0)
            return;

        int step = forward ? 1 : -1;

        m_selectedBodyIndex = (m_tankBodies.Length + m_selectedBodyIndex + step) % m_tankBodies.Length;
    }

    public void ShiftCabin(bool forward)
    {
        if (m_tankCabins.Length == 0)
            return;

        int step = forward ? 1 : -1;

        m_selectedCabin = (m_tankCabins.Length + m_selectedCabin + step) % m_tankCabins.Length;
    }

}
