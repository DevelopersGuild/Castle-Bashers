using UnityEngine;
using System.Collections;

public class TestSender : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            GameManager.Notifications.PostNotification(new Message(this.gameObject, MessageTypes.TEST_MESSAGE, gameObject.transform.position));
        }
    }
}
