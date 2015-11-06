using UnityEngine;
using System.Collections;

public class TestReciever : MonoBehaviour, IListener
{
    // Use this for initialization
    void Start()
    {
        GameManager.Notifications.AddListener(this, MessageTypes.TEST_MESSAGE);
    }

    public void OnReceived(Message message)
    {
        Vector3 vector = (Vector3)message.GetExtra();
        Debug.Log("Test");
    }
}
