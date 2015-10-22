using UnityEngine;
using System.Collections;

public class Mission_Timer : MonoBehaviour {

    public int current_time=0;
    
    public void Start_Timer()
    {
        InvokeRepeating("Timer_Clicking", 0,1);
    }
    void Timer_Clicking()
    {
        current_time = current_time + 1;
    }
    public void Stop_Timer()
    {
        CancelInvoke();
    }
    public void Clear_Timer()
    {
        Stop_Timer();
        current_time = 0;
    }
}
