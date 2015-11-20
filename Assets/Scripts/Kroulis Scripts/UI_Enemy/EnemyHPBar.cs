using UnityEngine;
using System.Collections;

public class EnemyHPBar : MonoBehaviour {
    private Transform bar;
    private Health enemy_health;
    float health;
    float maxhealth;
    float lasthealth;
	// Use this for initialization
	void Start () {
        bar = GetComponentInChildren<Transform>();
        enemy_health = GetComponentInParent<Health>();
        if(!enemy_health || !bar)
        {
            Destroy(this.gameObject);
        }
        maxhealth = enemy_health.GetMaxHP();
        health = enemy_health.GetCurrentHealth();
        lasthealth = health;
	}
	
	// Update is called once per frame
	void Update () {
        if(enemy_health && bar)
        {
            health = enemy_health.GetCurrentHealth();
            if(health!=lasthealth)
            {
                lasthealth = health;
                bar.localScale = new Vector3(health / maxhealth, 1, 1);
                bar.position=new Vector3(-(bar.localScale.x/2),bar.position.y,bar.position.z);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
	
	}
}
