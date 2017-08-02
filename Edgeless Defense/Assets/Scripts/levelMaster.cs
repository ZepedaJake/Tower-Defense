using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class levelMaster : MonoBehaviour {

    //set a spawn amount for enemies. wave time is dumb. 


    public bool debug = false;

    //states
    public bool waveActive = false;
    public bool spawnEnemies = false;

    //player
    public int health = 100;
    public int score = 0;
    public int money = 100;

    //text
    public Text scoreText;
    public Text healthText;
    public Text waveText;
    public Text moneyText;

    //wave stuff
    private float waveLevel = 0.0f;
    public float difficulty = 1.0f;
    public float waveTime = 30.0f;
    private float waveDelay = 2.0f;
    private float waveEndTime = 0;

    //enemy stuff
    public GameObject[] enemyPrefabs;
    public Transform enemySpawns;
    public GameObject cube;
    public Transform[] enemySpawnPoints;
    
    private float nextEnemySpawnTime = 0;
    private float respawnMinBase = 3.0f;
    private float respawnMaxBase = 10.0f;
    private float respawnMin;
    private float respawnMax;

    float enemyInterval = 3.0f;
    public int enemyCount = 0;
    private float lastSpawnTime = 0.0f;

    //new tower menu
    public Button circleButton;
    public bool showMenu = false;


    //new wave system
    public int numberOfWaves;
    /* i need...
    an aray for enemies
    a list of what waves an enemy appears on, or, and array for which you enter the number and each entry corresponds to a wave ( 3 wave, enemy spawns 5 on 1, 0 on 2, 10 on 3 = [5,0,10]
    a timer or something, so that i dont spawn ALL of them at once. set waves int he wave maybe, or spawn them in groups!!! instead of individually
    some sort of adjustable spawning functions, like, and ambush, that DOES, spawn a bunch at once, or a kinda sputter, like a sprinkler
    or... make an entire control menu from within game, but you would need to save and read from a  file to maintain wave data
    */
    // Use this for initialization
    void Start () {
        enemySpawnPoints = new Transform[enemySpawns.childCount];
        int i = 0;
        foreach(Transform spawnPoint in enemySpawns)
        {
            enemySpawnPoints[i] = spawnPoint;
            i++;
        }
        UpdateHUD();
        SetNextWave();
        StartNewWave();
	}
    private void FixedUpdate()
    {
        RotateButton();
    }
    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            debug = !debug;
            Debug.Log("debug is" + debug);
        }
	    /*if(Time.time>=nextEnemySpawnTime)
            {
                SpawnCube();
            }*/

        if (waveActive)
        {
            if (Time.time >= waveEndTime)
            {
                //stop spawning
                spawnEnemies = false;
                if (enemyCount == 0)
                {
                    FinishWave();
                }
            }
            
            if(spawnEnemies)
            {
                if(Time.time > (lastSpawnTime + enemyInterval)) //wave still going. spawn enemies
                {
                    SpawnNewEnemy();
                }
            }
        }
	}

    void SpawnNewEnemy()
    {
        //choose random enemy
        int enemyChoice = Random.Range(0, enemyPrefabs.Length);

        //seperate air and ground spawns
        int spawnChoice;

        if(enemyPrefabs[enemyChoice].tag == "AirEnemy")
        {
            //some air spawn locations here
        }
        else
        {
            spawnChoice = Random.Range(0, enemySpawnPoints.Length);
           /* GameObject newEnemy = (GameObject)*/Instantiate(enemyPrefabs[enemyChoice], enemySpawnPoints[spawnChoice].position, enemySpawnPoints[spawnChoice].rotation);
        }

        enemyCount++;

        lastSpawnTime = Time.time;

        enemyInterval = Random.Range(respawnMin, respawnMax);
        //nextEnemySpawnTime += enemyInterval;
        //int i = Random.Range(0, enemySpawnPoints.Length);
        //GameObject newCube = (GameObject)Instantiate(cube, enemySpawnPoints[i].position, enemySpawnPoints[i].rotation);
    }

    void SetNextWave()
    {
        Debug.Log("Set Next Wave");
        waveLevel++;
        difficulty = ((Mathf.Pow(waveLevel, 1.7f)) * 0.1f) + 1; //exponentially increase diff
        respawnMin = respawnMinBase * (1 / difficulty);
        respawnMax = respawnMaxBase * (1 / difficulty);
    }

    void StartNewWave()
    {
        Debug.Log("Start Next Wave");
        UpdateHUD();

        SpawnNewEnemy();

        waveEndTime = Time.time + waveTime;

        waveActive = true;
        spawnEnemies = true;
    }

    public void UpdateHUD()
    {
        waveText.text = "Wave: " + waveLevel;
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + health;
        moneyText.text = "$" + money;
    }

    void FinishWave()
    {
        Debug.Log("Finish wave");
        waveActive = false;

        //like yield. but much easier. do "" in 'n' seconds
        Invoke("SetNextWave", 1f);

        Invoke("StartNewWave", 1f);
    }

    public void ShowMenu()
    {
        showMenu = !showMenu;
        

    }
    public void RotateButton()
    {
        //rotate button to show options
        //if showmenu, rotate till its at 180 degrees. else, rotate till 0
        if(showMenu)
        {
            circleButton.transform.rotation = Quaternion.Lerp((circleButton.transform.rotation), Quaternion.Euler(0, 0, 180), Time.deltaTime * 3.5f);
        }
        else if(!showMenu)
        {
            circleButton.transform.rotation = Quaternion.Lerp((circleButton.transform.rotation), Quaternion.Euler(0, 0, 0), Time.deltaTime * 3.5f);
        }
       
        
    }
}
