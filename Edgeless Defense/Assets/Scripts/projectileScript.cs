using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour {
    public float speed = 30.0f;
    public float range = 7.0f;
    public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        distance += Time.deltaTime * speed;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
	}
}
