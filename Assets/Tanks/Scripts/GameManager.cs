using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {

    public LevelSettings m_levelSettings;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetupGame()
    {
        GameObject level = Instantiate(m_levelSettings.LevelPrefab);
        NetworkServer.Spawn(level);
    }


}
