using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;

public class UIPlayLocal : MonoBehaviour
{

    public TankNetworkDiscovery m_discovery;
    public Button m_buttonPrefab;
    public InputField m_gameNameInput;

    void Awake()
    {
        m_discovery.m_UI = this;
    }

    public void StartGame()
    {
        if (TankNetworkManager.singleton.isNetworkActive)
            throw new System.Exception("Start Game button shouldn't be available to click when network is active");

        string gameName = m_gameNameInput.text.Length > 0 ? m_gameNameInput.text : "Unnamed Tanks Game";
        int m_gameId = Random.Range(0, 10000);

        m_discovery.broadcastData = gameName + "@" + m_gameId;

        TankNetworkManager.singleton.DiscoveryStop();
        TankNetworkManager.singleton.m_broadcastOnStartHost = true;
        TankNetworkManager.singleton.StartHost();
    }

    public void FindGame()
    {
        if (m_discovery.running)
        {
            m_discovery.StopBroadcast();
            m_discovery.ClearGames();
        }
        else
        {
            m_discovery.Initialize();
            m_discovery.StartAsClient();
        }
    }

    public void Refresh()
    {
        m_discovery.ClearGames();
    }

    public void RecreateJoinButtons(Dictionary<int, gameInfo> games)
    {
        foreach (GameObject joinButton in GameObject.FindGameObjectsWithTag("Join Button"))
            Destroy(joinButton);

        int position = 0;
        foreach (gameInfo game in games.Values)
        {
            Button joinButton = (Button)Instantiate(m_buttonPrefab, transform);
            joinButton.GetComponent<RectGridPosition>().set(position);
            joinButton.transform.FindChild("Text").GetComponent<Text>().text = game.name;

            gameInfo g = game;
            joinButton.onClick.AddListener(() => JoinGame(g));

            position++;
        }
    }

    void JoinGame(gameInfo game)
    {
        TankNetworkManager.singleton.DiscoveryStop();
        TankNetworkManager.singleton.networkAddress = game.address;
        TankNetworkManager.singleton.StartClient();
    }

}
