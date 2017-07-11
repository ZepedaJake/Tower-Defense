using UnityEngine;
using System.Collections;
/*TODO
finish designing upgrade panel.
adjust upgrade panel's position so tha the tower is in the corder not center or off panel.
add upgradable variable(Damage : Range : Accuracy : Fire Rate <reload> : Projectile Speed)
try to get a preview of the selected tower at the top of the Tower panel UI**
    */
public class basicTurretScript : MonoBehaviour {
     public GameObject projectile;
    public float reload = 1f;

    public float turnSpeed = 5f;
    public float shotDelay = .25f;
    //public GameObject shootEffect;
    private Transform target;
    public Transform bulletSpawn;
    public Transform turretHead;

    public int price = 50;

    private float shotTimer;
    private float moveTimer;
    private Quaternion desiredRotation;
    public float accuracy = 80;
    private float aimError;
    
    private Vector3 aimPoint;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(target)
        {
            if (Time.time >= moveTimer)
            {
                Aim(target.position);
                turretHead.rotation = Quaternion.Lerp(turretHead.rotation, desiredRotation, Time.deltaTime * turnSpeed);
            }

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
            shotTimer = Time.time + (reload * .5f);
            target = other.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform == target)
        {
            target = null;
        }
    }

    void Aim(Vector3 targetPos)
    {
        float targetSpeed = target.GetComponent<basicEnemyScript>().speed;
        aimPoint = new Vector3(targetPos.x - turretHead.position.x + aimError, targetPos.y - turretHead.position.y -3 + aimError, targetPos.z-turretHead.position.z + aimError);
        desiredRotation = Quaternion.LookRotation(aimPoint);
    }

    void CalculateError()
    {
        aimError = (100 - accuracy) / 50;
        aimError = Random.Range( -aimError, aimError);       
    }

    void Shoot()
    {
        //audio.Play
        shotTimer = Time.time + reload;
        moveTimer = Time.time + shotDelay;
        CalculateError();
        Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation);

    }
}
