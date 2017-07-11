using UnityEngine;
using System.Collections;

public class turretBaseScript : MonoBehaviour {
    /*ToDo
    make turrets an extended class of this one.
    send signal for damage to the projectiles
    create an upgrade panel of some sort,allowing you to upgrade some of these variables
    fix camera zoom while turret panel is open.
    placementplaneScript.isopen needs to be linked in the turretbuttonsscript instead of using tags now


        */

    public int price;

    public int damage;
    public int baseDamage;

    public float reload;
    public float baseReload;

    public float range;
    public float baseRange;

    public float accuracy;
    public float baseAccuracy;

    public float speed;
    public float baseSpeed;

    public int damageUpgrades;
    public int reloadUpgrades;
    public int rangeUpgrades;
    public int accuracyUpgrades;
    public int speedUpgrades;

    public levelMaster theLevelMaster;
    
   
    
}
