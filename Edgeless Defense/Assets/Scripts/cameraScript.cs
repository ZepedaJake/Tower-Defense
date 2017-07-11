using UnityEngine;
using System.Collections;

//fix camera pivot so that it is on the ground level. not way beneath it after zooming
public class cameraScript : MonoBehaviour {
    public GameObject mainCamera;
    public GameObject viewCamera;
    public GameObject parentCamera;
    public GameObject lookHere;
    public GameObject moveToHere;
  
    public LayerMask floorLayer;
    
    
   

	// Use this for initialization
	void Start () {
        mainCamera = GameObject.Find("Main Camera");
        viewCamera = GameObject.Find("Main Camera1");
        moveToHere = GameObject.Find("Move To Here");
        parentCamera = GameObject.Find("Parent Camera");
        parentCamera.transform.position = mainCamera.transform.position;
        lookHere = GameObject.Find("Look Here");
        mainCamera.transform.LookAt(lookHere.transform.position);
        moveToHere.transform.position = new Vector3(lookHere.transform.position.x, mainCamera.transform.position.y + 30, lookHere.transform.position.z);
        
         
    }
	
	// Update is called once per frame
	void Update () {
        moveToHere.transform.LookAt(new Vector3(mainCamera.transform.position.x,moveToHere.transform.position.y,mainCamera.transform.position.z));
        moveToHere.transform.position = new Vector3(lookHere.transform.position.x, mainCamera.transform.position.y + 30, lookHere.transform.position.z);

        if (GameObject.Find("Turrets Panel").GetComponent<turretButtonsScript>().moveCamera == false)
        {
            //Debug.Log("false");

            RaycastHit hit;
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
           
            if (Physics.Raycast(ray, out hit, 5000, floorLayer))
            {
                //Instantiate(sourceObject, hit.point, Quaternion.identity);

                lookHere.transform.position = hit.point;
                //Debug.Log(hit.transform.name);
                Debug.DrawLine(mainCamera.transform.position, hit.point, Color.green);
            }


            //move camera around
            if (Input.GetKey(KeyCode.W))
            {
                parentCamera.transform.Translate(Vector3.forward);
                mainCamera.transform.position = parentCamera.transform.position;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                parentCamera.transform.Translate(Vector3.back);
                mainCamera.transform.position = parentCamera.transform.position;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                parentCamera.transform.Translate(Vector3.right);
                mainCamera.transform.position = parentCamera.transform.position;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                parentCamera.transform.Translate(Vector3.left);
                mainCamera.transform.position = parentCamera.transform.position;
            }

            //zoom camera
            viewCamera.transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 20);

            parentCamera.transform.position = mainCamera.transform.position;

            if (Input.GetMouseButton(1))
            {

                mainCamera.transform.RotateAround(lookHere.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 5);
                parentCamera.transform.eulerAngles = new Vector3(0, mainCamera.transform.eulerAngles.y, 0);
                parentCamera.transform.position = mainCamera.transform.position;
            }
        }
    }
}
