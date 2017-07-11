using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class turretButtonsScript : MonoBehaviour {

    //Controls turret placement and camera move when panel is open
    /* TODO
    placement planes disappear when turret menu closed
    red color on already used spots?
    add buttons and their functions
    buttons colors or whatever
    make a button where the tower is to close the upgrade menu or something. or an x somewhere
    */
    public GameObject turretsPanel;
    public bool turretsOpen;

    public GameObject upgradePanel;
    public bool upgradesOpen;

    //camera stuff
    public bool moveCamera;
    public GameObject mainCamera;
    public Vector3 overheadPosition;
    public Quaternion overheadAngle;
    public Vector3 positionChange = new Vector3(0,30,50);
    public float cameraMoveTimer = 0.0f;

    //stuff for placement
    public Transform placementPlanesRoot;
    public Material hoverMaterial;
    public LayerMask placementLayer;
    private Material defaultMaterial;
    private GameObject lastObjectHit;
    public GameObject allPlacementPlanes;
    placementPlaneScript focusedPlane;

    //button stuff
    public GameObject[] towersArray;
    public GameObject setTower;
    private int towersIndex = -1;
    public int cashCost;
    levelMaster theLevelMaster;
    public GameObject button;
    public GameObject lastButton;

    //upgrade stuff 
    turretBaseScript focusedTower;  
     
    public Text damageText;
    public Text damageUpgradeCostText;
    public Text damageAdditionText;
    public Text damageFinalText;
    public int damage = 0;
    public int damageUpgradeCost;
    public int damageAddition;
    public int damageFinal;

    public Text reloadText;
    public Text reloadUpgradeCostText;
    public Text reloadAdditionText;
    public Text reloadFinalText;
    public float reload = 0;
    public int reloadUpgradeCost;
    public float reloadAddition;
    public float reloadFinal;

    public Text rangeText;
    public Text rangeUpgradeCostText;
    public Text rangeAdditionText;
    public Text rangeFinalText;
    public float range = 0;
    public int rangeUpgradeCost;
    public float rangeAddition;
    public float rangeFinal;

    public Text accuracyText;
    public Text accuracyUpgradeCostText;
    public Text accuracyAdditionText;
    public Text accuracyFinalText;
    public float accuracy = 0;
    public int accuracyUpgradeCost;
    public float accuracyAddition;
    public float accuracyFinal;

    public Text speedText;
    public Text speedUpgradeCostText;
    public Text speedAdditionText;
    public Text speedFinalText;
    public float speed = 0;
    public int speedUpgradeCost;
    public float speedAddition;
    public float speedFinal;

    


	// Use this for initialization
	void Start () {
        theLevelMaster = GameObject.FindWithTag("LevelMaster").GetComponent<levelMaster>();
        mainCamera = GameObject.Find("Main Camera1");
    }
	
	// Update is called once per frame
	void Update () {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "TestScene")
        {
            Debug.Log(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }

        upgradePanel.transform.eulerAngles = new Vector3( 90, 0, -mainCamera.transform.eulerAngles.y);
        if (moveCamera == true)
        {
                OverheadCamera();
            cameraMoveTimer += Time.deltaTime;
            if (cameraMoveTimer >=3)
            {
                moveCamera = false;
                cameraMoveTimer = 0;
            }
        }

        if(turretsOpen)
        {
            allPlacementPlanes.SetActive(true);

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 1000, placementLayer))
                {
                    Debug.DrawLine(mainCamera.transform.position, hit.point, Color.red);
                    if (lastObjectHit)
                    {
                        lastObjectHit.GetComponent<Renderer>().material = defaultMaterial;
                    }
                    lastObjectHit = hit.collider.gameObject;
                    defaultMaterial = lastObjectHit.GetComponent<Renderer>().material;

                    //if spot is available make it green whn hovering
                    if (lastObjectHit.tag == "openPlace")
                    {
                        lastObjectHit.GetComponent<Renderer>().material = hoverMaterial;
                    }
                }
                else if (lastObjectHit)
                {
                    lastObjectHit.GetComponent<Renderer>().material = defaultMaterial;
                    lastObjectHit = null;
                }

                if (Input.GetMouseButtonDown(0) && lastObjectHit)
                {
                    focusedPlane = lastObjectHit.GetComponent<placementPlaneScript>();
                    if (focusedPlane.isOpen && theLevelMaster.money >= cashCost)
                    {
                        focusedPlane.isOpen = false;
                        GameObject newStructure = (GameObject)Instantiate(towersArray[towersIndex], lastObjectHit.transform.position, lastObjectHit.transform.rotation);
                        focusedPlane.currentTower = newStructure;
                        theLevelMaster.money -= cashCost;
                        theLevelMaster.UpdateHUD();
                        lastObjectHit.tag = "takenPlace";
                    }
                    else if (!focusedPlane.isOpen)
                    {
                        if (upgradesOpen == false)
                        {
                           
                            Debug.Log("show an upgrade menu");
                            upgradesOpen = !upgradesOpen;
                            updateUpgradeUI();
                            RectTransform rt = upgradePanel.GetComponent<RectTransform>();
                            rt.position = new Vector3(focusedPlane.transform.position.x, rt.position.y, focusedPlane.transform.position.z);
                            upgradePanel.SetActive(upgradesOpen);
                        }
                        else if(upgradesOpen == true)
                        {
                            Debug.Log("close upgrades");
                            upgradesOpen = !upgradesOpen;
                            upgradePanel.SetActive(upgradesOpen);

                            
                            Debug.Log("show an upgrade menu");
                            upgradesOpen = !upgradesOpen;
                            updateUpgradeUI();
                            RectTransform rt = upgradePanel.GetComponent<RectTransform>();
                            rt.position = new Vector3(focusedPlane.transform.position.x, rt.position.y, focusedPlane.transform.position.z);
                            upgradePanel.SetActive(upgradesOpen);
                        }
                            
                    }
                    
                }
            }
            else if(lastObjectHit)
            {
                lastObjectHit.GetComponent<Renderer>().material = defaultMaterial;
                lastObjectHit = null;
            }
        }
        else
        {
            allPlacementPlanes.SetActive(false);
        }                          
    }

    //determines wether to show the turret pane or close it
   public void ShowUI ()
    {       
        //open panel
        if (turretsOpen == false && moveCamera == false)
        {
            turretsOpen = !turretsOpen;
            overheadPosition = GameObject.Find("Move To Here").transform.position;
            overheadAngle.eulerAngles = new Vector3(90,mainCamera.transform.eulerAngles.y,0);
            Debug.Log("make dat whole panel move into view boi");
            moveCamera = true;
            RectTransform rt = turretsPanel.GetComponent<RectTransform>();         
            rt.localPosition = new Vector3(rt.localPosition.x - 200, rt.localPosition.y, rt.localPosition.z);

        }
        //close panel
        else if (turretsOpen == true && moveCamera == false)
        {
            turretsOpen = !turretsOpen;       
            overheadPosition = GameObject.Find("Return To Here").transform.position;                
            overheadAngle.eulerAngles = new Vector3(30, mainCamera.transform.eulerAngles.y, 0);          
            moveCamera = true;
            RectTransform rt = turretsPanel.GetComponent<RectTransform>();    
            rt.localPosition = new Vector3(rt.localPosition.x + 200, rt.localPosition.y, rt.localPosition.z);
            if (lastButton)
            {
                lastButton.GetComponent<Button>().image.color = Color.white;
            }
        }
    }

    //sets variables for camera position
    void OverheadCamera()
    {
        
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, overheadPosition, Time.deltaTime);

        Quaternion cameraAngle = Quaternion.Euler(mainCamera.transform.eulerAngles);
        mainCamera.transform.rotation = Quaternion.Lerp(cameraAngle, overheadAngle, Time.deltaTime);
        
    }

    public void basicTurretButton()
    {
        button = GameObject.Find("basicTurretButton");
        towersIndex = 0;
        swapButtonColor();
        setTower = towersArray[towersIndex];
        cashCost = setTower.GetComponent<basicTurretScript>().price;
    }

    public void homingTurretButton()
    {
        button = GameObject.Find("homingTurretButton");
        towersIndex = 1;
        swapButtonColor();
        //set tower is the tower now ready to be placed.
        //this takes the price variable and applies it to cost. that way you subtract the correct amount of money
        setTower = towersArray[towersIndex];
        cashCost = setTower.GetComponent<missileTurretScript>().price;
    }

    public void swapButtonColor()
    {
        if (lastButton)
        {
            lastButton.GetComponent<Button>().image.color = Color.white;
        }
        button.GetComponent<Button>().image.color = Color.cyan;
        lastButton = button;
    }

    public void showUpgrades()
    {
        if (!upgradesOpen)
        {
            upgradesOpen = !upgradesOpen;
        }
    }

    public void updateUpgradeUI()
    {
        focusedTower = focusedPlane.currentTower.GetComponent<turretBaseScript>();
        damage = focusedTower.damage; //focusedPlane.currentTower.GetComponent<turretBaseScript>().damage;
        damageUpgradeCost = focusedTower.price * (int)(Mathf.Pow(focusedTower.damageUpgrades, 1.5f ) + 1);
        damageAddition = (int)((focusedTower.damageUpgrades + 1) * focusedTower.baseDamage * 1.2f);
        damageFinal = damage + damageAddition;

        damageText.text = damage.ToString();
        damageUpgradeCostText.text = "$" + damageUpgradeCost.ToString();
        damageAdditionText.text = damageAddition.ToString();
        damageFinalText.text = damageFinal.ToString();


        reload = focusedTower.reload;
        reloadUpgradeCost = focusedTower.price * (int)(Mathf.Pow(focusedTower.reloadUpgrades, 1.5f) + 1);
        reloadAddition = (focusedTower.baseReload/2 *.1f);
        reloadFinal = reload - reloadAddition;

        reloadText.text = reload.ToString("F1");
        reloadUpgradeCostText.text = "$" + reloadUpgradeCost.ToString();
        reloadAdditionText.text = reloadAddition.ToString("F1");
        reloadFinalText.text = reloadFinal.ToString("F1");



        range = focusedTower.range;
        rangeUpgradeCost = focusedTower.price * (int)(Mathf.Pow(focusedTower.rangeUpgrades, 1.5f) + 1);
        rangeAddition = (focusedTower.range * .1f);
        rangeFinal = range + rangeAddition;

        rangeText.text = range.ToString("F1");
        rangeUpgradeCostText.text = "$" + rangeUpgradeCost.ToString();
        rangeAdditionText.text = rangeAddition.ToString("F1");
        rangeFinalText.text = rangeFinal.ToString("F1");



        accuracy = focusedTower.accuracy;
        accuracyUpgradeCost = focusedTower.price * (int)(Mathf.Pow(focusedTower.rangeUpgrades, 1.5f) + 1);
        if (100 - accuracy > 100 - focusedTower.baseAccuracy)
        {
            accuracyAddition = (100 - focusedTower.baseAccuracy) * .1f;
        }
        else
        {
            accuracyAddition = (100 - accuracy);
        }

        accuracyFinal = accuracy + accuracyAddition;

        accuracyText.text = accuracy.ToString("f1");
        if (accuracy < 100)
        {
            accuracyUpgradeCostText.text = accuracyUpgradeCost.ToString();
            accuracyAdditionText.text = accuracyAddition.ToString("f1");
            accuracyFinalText.text = accuracyFinal.ToString("f1");
        }
        else
        {
            accuracyUpgradeCostText.text = "MAX";
            accuracyAdditionText.text = "MAX";
            accuracyFinalText.text = "MAX";
        }



        speed = focusedTower.speed;
        speedUpgradeCost = focusedTower.price * (int)(Mathf.Pow(focusedTower.rangeUpgrades, 1.5f) + 1);
        speedAddition = focusedTower.speed / 2 * .1f;
        speedFinal = speed + speedAddition;

        speedText.text = speed.ToString("f1");
        speedUpgradeCostText.text = "$" + speedUpgradeCost;
        speedAdditionText.text = speedAddition.ToString("f1");
        speedFinalText.text = speedFinal.ToString("f1");


    }

    public void upgradeDamage()
    {
        if (theLevelMaster.money >= damageUpgradeCost)
        {
            theLevelMaster.money -= damageUpgradeCost;
            theLevelMaster.UpdateHUD();

            focusedTower.damage += damageAddition;
            focusedTower.damageUpgrades += 1;
            updateUpgradeUI();
        }
    }

    public void upgradeReload()
    {
        if (theLevelMaster.money >= reloadUpgradeCost)
        {
            theLevelMaster.money -= reloadUpgradeCost;
            theLevelMaster.UpdateHUD();

            focusedTower.reload -= reloadAddition;
            focusedTower.reloadUpgrades += 1;
            updateUpgradeUI();
        }
    }

    public void upgradeRange()
    {
        if(theLevelMaster.money >= rangeUpgradeCost)
        {
            theLevelMaster.money -= rangeUpgradeCost;
            theLevelMaster.UpdateHUD();

            focusedTower.range += rangeAddition;
            focusedTower.rangeUpgrades += 1;
            updateUpgradeUI();
        }
    }

    public void upgradeAccuracy()
    {
        if(theLevelMaster.money >= accuracyUpgradeCost && focusedTower.accuracy < 100)
        {
            theLevelMaster.money -= accuracyUpgradeCost;
            theLevelMaster.UpdateHUD();

            focusedTower.accuracy += accuracyAddition;
            focusedTower.accuracyUpgrades += 1;
            updateUpgradeUI();
        }
    }

    public void upgradeSpeed()
    {
        if(theLevelMaster.money >= speedUpgradeCost)
        {
            theLevelMaster.money -= speedUpgradeCost;
            theLevelMaster.UpdateHUD();

            focusedTower.speed += speedAddition;
            focusedTower.speedUpgrades += 1;
            updateUpgradeUI();
        }
    }

    public void closeUpgrades()
    {
        Debug.Log("close upgrades");
        upgradesOpen = !upgradesOpen;
        upgradePanel.SetActive(upgradesOpen);
    }

}
