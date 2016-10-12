using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class TankHealth : NetworkBehaviour
{
    public const int maxHealth = 100;
    public Image m_fillImage;
    public Slider m_slider;

    public Color m_fullHealthColor = Color.green;
    public Color m_zeroHealthColor = Color.red;

    private References m_ref;

    [SyncVar(hook = "OnChangeHealth")]
    public int m_currentHealth = maxHealth;

    void Start()
    {
        if (isLocalPlayer)
        {
            m_ref = GameObject.Find("References").GetComponent<References>();

            m_ref.messageText.SetActive(false);
            m_ref.controls.SetActive(true);
        }

        OnChangeHealth(m_currentHealth);
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
            return;

        m_currentHealth -= amount;
        if (m_currentHealth <= 0)
        {
            m_currentHealth = 0;
            RpcLose();
        }
    }

    void OnChangeHealth(int health)
    {
        m_fillImage.color = Color.Lerp(m_zeroHealthColor, m_fullHealthColor, 1f * health / maxHealth);
        m_slider.value = health;
    }

    [ClientRpc]
    private void RpcLose()
    {
        if (!isLocalPlayer)
            return;

        GetComponent<TankControl>().enabled = false;
        m_ref.controls.SetActive(false);
        m_ref.messageText.SetActive(true);
    }
}