﻿using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

    private int coins;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            coins = 10000;
        else
            coins = 0;
    }

    //Coin methods
    public void addCoins(int add)
    {
        coins += add;
    }

    public void subtractCoins(int subtract)
    {
        coins -= subtract;
    }

    public void reset()
    {
        coins = 0;
    }

    public void setCoins(int coinsSet)
    {
        coins = coinsSet;
    }

    public int getCoins()
    {
        return coins;
    }

}
