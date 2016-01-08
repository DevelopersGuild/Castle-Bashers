using UnityEngine;
using System.Collections;

public class PlayerHolder : MonoBehaviour
{


    //Get Components
    public static PlayerHolder Instance
    {
        get
        {
            if (playerHolder == null)
            {
                playerHolder = new GameObject("PlayerHolder").AddComponent<PlayerHolder>();
            }
            return playerHolder;
        }
    }

    private static PlayerHolder playerHolder;

    void Awake()
    {
        if ((playerHolder) && (playerHolder.GetInstanceID() != GetInstanceID()))
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            playerHolder = this;

            DontDestroyOnLoad(gameObject);
        }
    }

    void resetPositions()
    {
        foreach (Transform child in transform)
        {
            child.position = new Vector3(20, 10, 0);
        }
    }

    public void OnLevelWasLoaded(int level)
    {
        resetPositions();
    }

}
