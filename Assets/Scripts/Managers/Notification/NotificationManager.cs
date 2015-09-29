using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour
{
    //TODO Use queue to disttribute message calls over several frames
    private Queue<Message> m_messagesToBeSent;
    private Dictionary<MessageTypes,List<IListener>> m_listeners = new Dictionary<MessageTypes,List<IListener>>();

    public void AddListener(IListener listener, MessageTypes messageType)
    {
        if(m_listeners.ContainsKey(messageType))
        {
            m_listeners[messageType].Add(listener);
        }
        else
        {
            m_listeners.Add(messageType, new List<IListener>());
            m_listeners[messageType].Add(listener);
        }
    }

    public void PostNotification(Message message)
    {
        if(!m_listeners.ContainsKey(message.GetMessageType()))
        {
            return;
        }
        foreach(IListener listener in m_listeners[message.GetMessageType()])
        {
            listener.OnReceived(message);
        }
    }

    //TODO Add function to remove null objects from the lists
}


