using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Tanks/LevelSettings")]
public class LevelSettings : ScriptableObject {

    [SerializeField]
    private GameObject[] m_levelPrefas;
    [SerializeField]
    private int m_selectedLevelIndex = 0;

    private int m_snapshotLevelIndex = 0;

    public void CreateSnapshot()
    {
        m_snapshotLevelIndex = m_selectedLevelIndex;
    }

    public void RestoreSnapshot()
    {
        m_selectedLevelIndex = m_snapshotLevelIndex;
    }

    public GameObject LevelPrefab
    {
        get
        {
            return m_levelPrefas[m_selectedLevelIndex];
        }
    }

    public void ShiftLevel(bool forward)
    {
        if (m_levelPrefas.Length == 0)
            return;

        int step = forward ? 1 : -1;

        m_selectedLevelIndex = (m_levelPrefas.Length + m_selectedLevelIndex + step) % m_levelPrefas.Length;
    }

}
