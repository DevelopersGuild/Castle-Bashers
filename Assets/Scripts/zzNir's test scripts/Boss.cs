using UnityEngine;
using System.Collections;
using System;

public class Boss : Enemy
{
    private bool blink, blinkColorSwitch;
    private float blinkTime;
    private Color origColor, blinkColor;

    // Use this for initialization
    public void Start()
    {
        //later on make it only target living players, priority on tanks
        base.Start();
        blinkTime = 1;
        blink = true;
        origColor = sprRend.color;
        blinkColor = new Color(origColor.r / 2, origColor.b / 2, origColor.g / 2, .7f);
        blinkColorSwitch = true;


    }

    // Update is called once per frame
    public void Update()
    {
        Blink();

    }

    public void Gravity()
    {
        if (!moveController.collisions.above && !moveController.collisions.below)
        {
            gravity.y += -0.1f;
            Move(gravity, Math.Abs(gravity.y));
            //Debug.Log(gravity.y);
            freeFall = true;
        }
        else
        {
            gravity.y = -1;
            freeFall = false;
        }
    }

    
    public void setInvTime(float t)
    {
        base.setInvTime(t);
        blinkTime = t;
    }

    public void Blink()
    {
        if (blink)
        {
            if (blinkTime > 0)
            {
                if (blinkColorSwitch)
                {
                    sprRend.color = blinkColor;
                    blinkColorSwitch = false;
                }
                else
                {
                    sprRend.color = origColor;
                    blinkColorSwitch = true;
                }
                blinkTime -= Time.deltaTime;
            }
            else
            {
                blink = false;
                blinkColorSwitch = true;
                sprRend.color = origColor;
            }
        }
    }

    public void TurnOnBlink()
    {
        blink = true;
    }

}

