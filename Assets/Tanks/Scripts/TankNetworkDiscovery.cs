using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public struct gameInfo
{
    public gameInfo(string name, string address)
    {
        this.name = name;
        this.address = address;
    }

    public readonly string name;
    public readonly string address;
}


public class TankNetworkDiscovery : NetworkDiscovery
{

    private Dictionary<int, gameInfo> m_games = new Dictionary<int, gameInfo>();

    public UIPlayLocal m_UI;

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (AddGame(fromAddress, data))
            m_UI.RecreateJoinButtons(m_games);
    }

    private bool AddGame(string address, string data)
    {
        int splitAt = data.LastIndexOf('@');
        string gameName = data.Substring(0, splitAt);
        int gameId = int.Parse(data.Substring(splitAt + 1));

        if (m_games.ContainsKey(gameId))
            return false;

        m_games.Add(gameId, new gameInfo(gameName, address));

        return true;
    }

    public void ClearGames()
    {
        m_games.Clear();
        m_UI.RecreateJoinButtons(m_games);
    }


}
