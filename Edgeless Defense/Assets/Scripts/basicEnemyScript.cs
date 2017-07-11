using UnityEngine;
using System.Collections;

public class basicEnemyScript : MonoBehaviour {
    public float height = 1;
    public float speed = 15;
    float health = 100;
    float maxHealth = 100;
    float cashValue = 10;

    //link to level master
    //GameObject levelMaster;
    levelMaster theLevelMaster;
	// Use this for initialization
	void Start () {

        theLevelMaster = GameObject.FindWithTag("LevelMaster").GetComponent<levelMaster>();
        if(theLevelMaster)
        {
            Debug.Log("level master set" + theLevelMaster.difficulty);
        }
        maxHealth = health;
        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        speed *= theLevelMaster.difficulty;
        health *= theLevelMaster.difficulty;
        maxHealth *= theLevelMaster.difficulty;
        cashValue *= theLevelMaster.difficulty;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));

	}

    public void Hit(float damageAmount)
    {
        health -= damageAmount;
        float healthPercent = health / maxHealth;
        if (health<=0)
        {
            Death();
            return;
        }
    }

    void Death()
    {
        theLevelMaster.enemyCount--;
        theLevelMaster.money += (int)cashValue;
        theLevelMaster.score += (int)((maxHealth + speed) * theLevelMaster.difficulty);
        theLevelMaster.UpdateHUD();

        //want an explosion? put it here!
        Destroy(gameObject);
    }
}
