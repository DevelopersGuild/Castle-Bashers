using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour {

    private int coins;

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

}
