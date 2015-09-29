using UnityEngine;
using System.Collections;

[RequireComponent(typeof (NotificationManager))]
public class GameManager : MonoBehaviour
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
    }

    //TODO add the rest of the game manager as development progresses.
}
