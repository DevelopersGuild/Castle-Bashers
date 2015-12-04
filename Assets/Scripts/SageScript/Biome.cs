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
        EnemyList[SnowyForest, 0] = "Enemies/GoonEnemy";
        EnemyList[SnowyForest, 1] = "Enemies/Bear";
        EnemyList[Desert, 0] = "Enemies/SpecialEnemyV1";
        EnemyList[Desert, 1] = "Enemies/RangedEnemy";
        EnemyList[Fort, 0] = "Enemies/GoonEnemy";
        EnemyList[Fort, 1] = "Enemies/RangedEnemy";

        Backgrounds[SnowyForest, 0] = "Maps/BackgroundContainer";
        Backgrounds[SnowyForest, 1] = "Assets/SpritesAndAnimations/Objects/object_torch.png";

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
