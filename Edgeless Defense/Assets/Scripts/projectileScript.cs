using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {
    public float speed = 30.0f;
    public float range = 7.0f;
    public float distance;
    public int damage;
    public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(target!=null)
        {
            transform.LookAt(target);
        }
        
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        distance += Time.deltaTime * speed;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
	}

    

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("erg");
        if (other.gameObject.tag == "enemy")
        {
            Destroy(gameObject);
            other.gameObject.SendMessage("Hit", damage, SendMessageOptions.DontRequireReceiver);
        }
    }
}
