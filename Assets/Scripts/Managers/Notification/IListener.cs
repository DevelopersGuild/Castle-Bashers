using UnityEngine;
using System.Collections;

public interface IListener
{
    void OnReceived(Message message);
}
