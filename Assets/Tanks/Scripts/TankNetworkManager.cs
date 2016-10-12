using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Threading;

public class TankNetworkManager : NetworkManager
{

    public TankNetworkDiscovery m_discovery;
    public ApplicationManager m_applicationManager;
    public GameManager m_gameManager;

    public bool m_broadcastOnStartHost = false;

    private const int c_waitMiliseconds = 100;

    public static new TankNetworkManager singleton
    {
        get { return (TankNetworkManager)NetworkManager.singleton; }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {

        if (conn.address == "localServer")
        {
            m_gameManager.SetupGame();
            return;
        }

        if (conn.address == "localClient")
        {
            if (m_broadcastOnStartHost)
            {
                m_broadcastOnStartHost = false;
                BroadcastStart();
            }
  
            return;
        }
    }

    public void BroadcastStart()
    {
        m_discovery.Initialize();
        m_discovery.StartAsServer();
        Thread.Sleep(c_waitMiliseconds);
    }

    public void DiscoveryStop()
    {
        if (m_discovery.isClient)
        {
            m_discovery.StopBroadcast();
            Thread.Sleep(c_waitMiliseconds);
            m_discovery.ClearGames();
        }
    }

    public void BroadcastStop()
    {
        if (!m_discovery.isServer)
            return;

        m_discovery.StopBroadcast();
        Thread.Sleep(c_waitMiliseconds);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        m_applicationManager.playMode = true;

        base.OnClientConnect(conn);
    }
}
