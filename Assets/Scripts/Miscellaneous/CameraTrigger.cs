using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    private CameraFollow camera;
    private int enemyCount;

    void Start()
    {
        camera = FindObjectOfType<CameraFollow>();
        enemyCount = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            enemyCount++;
            Debug.Log(enemyCount);
            camera.setLock(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            enemyCount--;
            Debug.Log(enemyCount);
        }

        if (enemyCount <= 0)
        {
            camera.setLock(false);
        }
    }
}
