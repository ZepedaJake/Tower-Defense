using UnityEngine;
using System.Collections;

public class turretBaseScript : MonoBehaviour {
    /*ToDo
    make turrets an extended class of this one.
    send signal for damage to the projectiles
    create an upgrade panel of some sort,allowing you to upgrade some of these variables
    placementplaneScript.isopen needs to be linked in the turretbuttonsscript instead of using tags now


        */

    public int price;

    public int damage;
    public int baseDamage;

    //fire rate
    public float reload;
    public float baseReload;

    //how far the tower can see
    public float range;
    public float baseRange;

    //how accurate the tower is
    public float accuracy;
    public float baseAccuracy;

    //how fast the towers projectile travels
    public float speed;
    public float baseSpeed;

    public int damageUpgrades;
    public int reloadUpgrades;
    public int rangeUpgrades;
    public int accuracyUpgrades;
    public int speedUpgrades;

    public levelMaster theLevelMaster;

    public GameObject rangeSphere;

    private void Start()
    {
        
    }
    void Update()
    {

        //gameObject.transform.GetComponent<SphereCollider>().radius = range;
        //rangeSphere.transform.localScale = new Vector3(range * 2, range * 2, range * 2);

        //rangeSphere.SetActive(theLevelMaster.debug);
    }



}
