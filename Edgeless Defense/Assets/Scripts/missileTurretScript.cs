using UnityEngine;
using System.Collections;

public class missileTurretScript : turretBaseScript {

    public GameObject projectile;
    public GameObject rangeSphere;
    
    //public float reload = 2f;


    public Transform target;
    public Transform[] targetArray;
    public Transform bulletSpawn;
    public Transform turretHead;
    public int targetCount = 0;

    private GameObject missile;
    private bool missileOut;

    private float shotTimer;
    
    

    //public float accuracy = 100;

    //public int price = 100;


    // Use this for initialization
    void Start()
    {
        targetArray = new Transform[25];

        //variable from the base script
        range = 15;
        reload = 3f;
        accuracy = 100;
        price = 100;
        damage = 100;
        speed = 15;

        baseRange = 15;
        baseReload = 3f;
        baseAccuracy = 100;
        baseDamage = 100;
        baseSpeed = 15;

        theLevelMaster = GameObject.FindWithTag("LevelMaster").GetComponent<levelMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetComponent<SphereCollider>().radius = range;
        rangeSphere.transform.localScale = new Vector3(range * 2, range * 2, range * 2);

        
        
            rangeSphere.SetActive(theLevelMaster.debug);
        

        if (target)
        {
            if (Time.time >= shotTimer)
            {
                Shoot();
                
            }
        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {

            targetArray[24] = other.gameObject.transform;
            if (!targetArray[0] || targetArray[0] == null)
            { 
                for (int i = 0; i < 25; i++)
                {
                    targetArray[i] = targetArray[i + 1];
                }
            }
            
            
        }
    }
    //while enemies are still in range of tower. if a missile does not have a target, find a new one from the array
    void OnTriggerStay(Collider other)
    {
        if (!target)
        {
            
            for (int i = 0; i < 25; i ++)
            {
                
                if (targetArray[i])
                {
                    target = targetArray[i];
                    if (missile)
                    missile.SendMessage("Retarget", target, SendMessageOptions.DontRequireReceiver);
                    break;
                }
                else
                {
                    targetArray[i] = targetArray[i + 1];
                }
                
            }

            /*if (other.gameObject.tag == "enemy")
            {
                
                shotTimer = Time.time + (reload * .5f);
                target = other.gameObject.transform;
                
            }*/
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == target)
        {
           
            target = null;
        }
    }



    void Shoot()
    {
        //audio.Play
        shotTimer = Time.time + reload;
        missile = (GameObject)Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation);
        missile.GetComponent<missileScript>().target = target;
        missile.GetComponent<missileScript>().parentTurret = gameObject;
        missile.GetComponent<missileScript>().damage = damage;
        missile.GetComponent<missileScript>().speed = speed;
        
        
    }
}
