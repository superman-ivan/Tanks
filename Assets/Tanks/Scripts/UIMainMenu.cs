using UnityEngine;
using UnityEngine.Networking;

public class UIMainMenu : MonoBehaviour
{

    public ApplicationManager m_applicationManager;

    private GameObject[] m_submenus;

    public void ShowOnly(GameObject submenu)
    {
        if (submenu.activeSelf)
            return;

        foreach (GameObject current_submenu in GameObject.FindGameObjectsWithTag("Submenu"))
            current_submenu.SetActive(false);

        submenu.SetActive(true);
    }

    public void StartPractice()
    {
        TankNetworkManager.singleton.StartHost();
        m_applicationManager.playMode = true;
    }

    public void Return()
    {
        m_applicationManager.playMode = false;

        TankNetworkManager.singleton.BroadcastStop();

        TankNetworkManager.singleton.StopClient();
        TankNetworkManager.singleton.StopServer();
    }

}
