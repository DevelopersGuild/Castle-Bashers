using UnityEngine;
using System.Collections;


public class Biome : MonoBehaviour {

   public enum BiomeName {SnowyForest, Desert, Fort, Forest}

    public static string[,] EnemyList = new string[20, 30];
    public static string[,] Backgrounds = new string[20, 3];

    int SnowyForest = 0;
    int Desert = 1;
    int Fort = 2;
    int Forest = 3;

    //Hard coded, assigned enemies

    // Use this for initialization
    void Start () {
        EnemyList[SnowyForest, 0] = "Assets/Prefabs/Enemies/BasicEnemy.prefab";
        EnemyList[SnowyForest, 1] = "Assets/Prefabs/Enemies/Big Guy.prefab";
        EnemyList[Desert, 0] = "Assets/Prefabs/Enemies/SpecialEnemyV1.prefab";
        EnemyList[Desert, 1] = "Assets/Prefabs/Enemies/RangedEnemy.prefab";
        EnemyList[Fort, 0] = "Assets/Prefabs/Enemies/BasicEnemy.prefab";
        EnemyList[Fort, 1] = "Assets/Prefabs/Enemies/RangedEnemy.prefab";

        Backgrounds[SnowyForest, 0] = "Assets/Prefabs/Maps/SnowBackground.prefab";

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
