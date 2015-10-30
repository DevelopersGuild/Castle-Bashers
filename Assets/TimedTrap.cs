using UnityEngine;
using System.Collections;

public class TimedTrap : MonoBehaviour
{

    public GameObject Other;
    public float Interval;
    private float time;

    // Use this for initialization
    void Start()
    {
        time = Interval;
    }

    // Update is called once per frame
    void Update()
    {
        if (time < 0)
        {
            time = 0;
            Activate(false);
            Other.GetComponent<TimedTrap>().Activate(true);
        }
        else if(time > 0)
        {
            time -= Time.deltaTime;
        }
    }

    public void Activate(bool choice)
    {
        if(choice)
        time = Interval;

        GetComponent<SpriteRenderer>().enabled = choice;
        if (GetComponent<DealDamageToPlayer>())
            GetComponent<DealDamageToPlayer>().enabled = choice;
    }
}
