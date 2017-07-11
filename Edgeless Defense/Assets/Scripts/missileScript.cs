using UnityEngine;
using System.Collections;

public class missileScript : MonoBehaviour {

    public Transform target;
    public GameObject parentTurret;
    public float speed = 15.0f;
    public float range = 200.0f;
    public float distance;
    public float damage;

    public float timer = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > 2.5f)
        {
            
            if (target)
            {
                transform.LookAt(target);
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else
            {
                Retarget();
                //Explode();
            }

        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed * .5f);
        }

        distance += Time.deltaTime * speed;
        if (distance >= range)
        {
           Explode();
        }
        
    }

  void OnTriggerEnter(Collider other)
    {
        //Debug.Log("erg");
        if(other.gameObject.tag == "enemy")
        {
            Explode();
            other.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void Explode()
    {
        //instantiate an explosion here
        Destroy(gameObject);
    }

    void Retarget()
    {
        target = parentTurret.GetComponent<missileTurretScript>().target;
    }
}
