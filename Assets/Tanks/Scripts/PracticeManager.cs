using UnityEngine;
using System.Collections;

public class PracticeManager : MonoBehaviour {

    public GameObject m_tankPrefab;
    public TankSettings m_tankSettings;
    public LevelSettings m_levelSettings;

    private GameObject m_tank;
    private GameObject m_level;

    public bool running
    {
        get { return m_level != null; }
    }

    void Awake()
    {
        Debug.LogWarning("Scheduled for deletion!");
    }

    public void StartPractice () {
        m_level = (GameObject)Instantiate(m_levelSettings.LevelPrefab);
        Transform spawnPosition = m_level.transform.FindChild("Spawn Position 1");

        m_tank = (GameObject)Instantiate(m_tankPrefab, spawnPosition.position, spawnPosition.rotation);
        m_tank.GetComponent<TankControl>().m_isPractice = true;
        m_tank.GetComponent<TankBuild>().Build(m_tankSettings.m_selectedBodyIndex, m_tankSettings.m_selectedGunIndex);
	}
	
	public void EndPractice () {
        Destroy(m_tank);
        Destroy(m_level);
	}
}
