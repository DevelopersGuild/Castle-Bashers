using UnityEngine;
using System.Collections;

[RequireComponent(typeof (NotificationManager))]
public class GameManager : MonoBehaviour, IListener
{

    public static GameManager Instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return m_instance;
        }
    }

    public static NotificationManager Notifications
    {
        get
        {
            if(m_notifications == null)
            {
                m_notifications = m_instance.GetComponent<NotificationManager>();
            }
            return m_notifications;
        }
    }


    private static GameManager m_instance = null;
    private static NotificationManager m_notifications = null;
    private int numberOfPlayersDead = 0;
    private bool allPlayersDead = false;
    private Player[] players;

    void Awake()
    {
        if((m_instance) && (m_instance.GetInstanceID() != GetInstanceID()))
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        GameManager.Notifications.AddListener(this, MessageTypes.PLAYER_DEATH);
        players = GameObject.FindObjectOfType<PlayerHolder>().GetComponentsInChildren<Player>();
    }

    //TODO add the rest of the game manager as development progresses.

    public bool CheckIfAllPlayersAreDead()
    {
        return allPlayersDead;
    }

    public void OnReceived(Message message)
    {
        if(message.GetMessageType() == MessageTypes.PLAYER_DEATH)
        {
            numberOfPlayersDead++;
            if(numberOfPlayersDead == players.Length)
            {
                allPlayersDead = true;
            }
        }
    }
}
