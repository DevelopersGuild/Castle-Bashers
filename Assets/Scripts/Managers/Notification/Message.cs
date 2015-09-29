using UnityEngine;
using System.Collections;

public class Message
{
    public Message(GameObject sender, MessageTypes messageType, System.Object extraData = null)
    {
        m_sender = sender;
        m_messageType = messageType;
        m_extraData = extraData;
    }
    private GameObject m_sender;
    private MessageTypes m_messageType;
    private System.Object m_extraData;

    public GameObject GetSender()
    {
        return m_sender;
    }

    public MessageTypes GetMessageType()
    {
        return m_messageType;
    }

    public System.Object GetExtra()
    {
        return m_extraData;
    }
}
