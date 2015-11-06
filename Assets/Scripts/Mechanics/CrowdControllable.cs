using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Effect
{
    public float value = 0;
    public float duration = 0;
    private float expiration = 0;

    public Effect()
    {

    }
    public Effect(float val, float dura)
    {
        value = val;
        duration = dura;
        expiration = Time.time + dura;
    }

    //Returns the time an effect will expire
    public float getExpiration()
    {
        return expiration;
    }
}


public class CrowdControllable : MonoBehaviour {

    public floatingText floatText;
    public Vector3 effectOffset = new Vector3(0, 2, 0);
    private Vector3 effectPosition;

    public bool canStun = true;
    private bool isStunned = false;
    List<Effect> stuns = new List<Effect>();
    public GameObject stunEffect;

    public bool canSlow = true;
    private bool isSlowed = false;
    List<Effect> slows = new List<Effect>();
    public GameObject slowEffect;

    public bool canSilence = true;
    private bool isSilenced = false;
    List<Effect> silences = new List<Effect>();
    public GameObject silenceEffect;

    public bool canSnare = true;
    private bool isSnared = false;
    List<Effect> snares = new List<Effect>();
    public GameObject snareEffect;

    public bool canPoly = true;
    private bool isPoly = false;
    List<Effect> polymorphs = new List<Effect>();
    public GameObject PolyEffect;


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        effectPosition = gameObject.transform.position + effectOffset;

        //Testing buttons
        if (Input.GetKeyDown("n"))
        {
            addStun(5);
        }
        if (Input.GetKeyDown("m"))
        {
            addSnare(2);
        }
        if (Input.GetKeyDown("u"))
        {
            addSlow(0.50f, 2);
        }
        if (Input.GetKeyDown("k"))
        {
            addSilence(2);
        }

        //update stuns
        for (int i = stuns.Count - 1; i >= 0; i--)
        {
            if (Time.time > stuns[i].getExpiration())
            {
                stuns.RemoveAt(i);
                floatText.GetComponent<TextMesh>().text = "Stun Removed!";
                Instantiate(floatText, effectPosition, Quaternion.identity);
            }
        }

        //update snares
        for (int i = snares.Count - 1; i >= 0; i--)
        {
            if (Time.time > snares[i].getExpiration())
            {
                snares.RemoveAt(i);
                floatText.GetComponent<TextMesh>().text = "Snare Removed!";
                Instantiate(floatText, effectPosition, Quaternion.identity);
            }
        }

        //update slows
        for (int i = slows.Count - 1; i >= 0; i--)
        {
            if (Time.time > slows[i].getExpiration())
            {
                slows.RemoveAt(i);
                floatText.GetComponent<TextMesh>().text = "Slows Removed!";
                Instantiate(floatText, effectPosition, Quaternion.identity);
            }
        }

        //update silences
        for (int i = silences.Count - 1; i >= 0; i--)
        {
            if (Time.time > silences[i].getExpiration())
            {
                silences.RemoveAt(i);
                floatText.GetComponent<TextMesh>().text = "Silence Removed!";
                Instantiate(floatText, effectPosition, Quaternion.identity);
            }
        }

        if (stuns.Count == 0)
            isStunned = false;
        if (snares.Count == 0)
            isSnared = false;
        if (slows.Count == 0)
            isSlowed = false;
        if (silences.Count == 0)
            isSilenced = false;
        
    }

    public void addStun(float duration)
    {
        if (canStun == true)
        {
            Effect newStun = new Effect(0, duration);
            stuns.Add(newStun);
            isStunned = true;
            //The floating text should be replaced by a better graphical representation
            floatText.GetComponent<TextMesh>().text = "Stunned!";
            Instantiate(floatText, effectPosition, Quaternion.identity);
        }

    }

    public void addSnare(float duration)
    {
        if (canSnare == true)
        {
            Effect newSnare = new Effect(1, duration);
            snares.Add(newSnare);
            isSnared = true;

            floatText.GetComponent<TextMesh>().text = "Snared!";
            Instantiate(floatText, effectPosition, Quaternion.identity);
        }
    }

    //Slow value is read intuitively. 0.15 means 15% slow
    public void addSlow(float value, float duration)
    {
        if (canSlow == true)
        {
            Effect newSlow = new Effect(value, duration);
            slows.Add(newSlow);
            isSlowed = true;

            floatText.GetComponent<TextMesh>().text = "Slowed!";
            Instantiate(floatText, effectPosition, Quaternion.identity);
        }
    }

    public void addSilence(float duration)
    {
        if(canSilence == true)
        {
            Effect newSilence = new Effect(2, duration);
            silences.Add(newSilence);
            isSilenced = true;

            floatText.GetComponent<TextMesh>().text = "Silenced!";
            Instantiate(floatText, effectPosition, Quaternion.identity);
        }

    }

    //The slow value returned is the value of the strongest slow
    //judged by value, not time. Return value is the effective speed coefficient.
    //A player with 15% slow on will return 0.85.
    public float getSlow()
    {
        float strongest = 0.0f;
        for (int i = slows.Count; i > 0; i--)
        {
            if (strongest < slows[i-1].value)
            {
                strongest = slows[i-1].value;
            }
        }
        return (1 - strongest);
    }

    //stun will return true if there is at least one active stun
    //Diminishing returns will be added after some basic testing
    public bool getStun()
    {
        return isStunned;
    }

    public bool getSnare()
    {
        return isSnared;
    }

    //silence will return true if there is at least one active silence
    //Diminishing returns will be added after some basic testing
    public bool getSilence()
    {
        return isSilenced;
    }

    //Force premature removal of effects
    public void removeStuns()
    {
        for (int i = stuns.Count; i >= 0; i--)
        {
            stuns.RemoveAt(i);
        }
    }

    public void removeSnares()
    {
        for (int i = snares.Count; i >= 0; i--)
        {
            snares.RemoveAt(i);
        }
    }

    public void removeSlows()
    {
        for (int i = slows.Count; i >= 0; i--)
        {
            slows.RemoveAt(i);
        }
    }

    public void removeSilences()
    {
        for (int i = silences.Count; i >= 0; i--)
        {
            silences.RemoveAt(i);
        }
    }

}
