using UnityEngine;
using System.Collections;

public class playerBaseScript : MonoBehaviour {
    public levelMaster theLevelMaster;
    public GameObject levelevel;
	// Use this for initialization
	void Start () {
        levelevel = GameObject.FindWithTag("LevelMaster");
        theLevelMaster = GameObject.FindWithTag("LevelMaster").GetComponentInParent<levelMaster>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "enemy")
        {
            Destroy(other.gameObject);
            theLevelMaster.enemyCount--;
            theLevelMaster.health--;
            theLevelMaster.UpdateHUD();
        }
    }

}
