using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/* Controls everything in User Interface */

public class UIController : MonoBehaviour {

    public GameObject player;         // Reference to player 
    public TMP_Text instructionText;  // Greeting/instruction message

    // AIR VARIABLES
    public Text airText;
    public GameObject airNeedle;
    public const float endAngle = 140f;
    private const float startAngle = -90f;
    private float airNeedleRotationZ = startAngle;         // Air Needle rotation data

    // TRANSECT VARIABLES
    public GameObject transectPrefab;
    public Text transectText;        // Reference to deploy/retract transect tape
    private bool transectBtnPressed = false;
    private GameObject transect;     // reference to transect tape
    private float maxDistance = 4f;
    private bool inTransect = false;
    public bool transectActive;


    // DEPTH VARIABLES
    // -- Current Display: Digital
    public Text depthText;          // Reference to player's depth in ocean
    private float playerDepth = 0;  // Holds the players depth in game units
    private float playerDepthMeter = 0;   // Holds player depth in meters
    //private float playerDepthFeet = 0;  // Holds player depth in feet, unused;

    // -- Variables for Analog Display (not implemented)
    // public GameObject depthNeedle;
    // public static float depthStartAngle = -90f;
    // public static float depthScale = 7.5f;
    // private float depthNeedleRotationZ = depthStartAngle;   // Depth Needle rotation data
  
    // TIME VARIABLES
    public Text timeText;           // Reference to time left in dive
    public float timeValue = 600;   // Total Dive Time = 10 mins = 600 sec
    private float totalTime = 600;
    public TransectSystem transectSystem;

     // COMPASS VARIABLES
    public Transform playerTransform;   // Position and rotation of FPSController
    Vector3 dir;
    public GameObject compass;          // Used for change the transform of the compass panel
    
    // Use this for initialization
    void Start() {

        // Initalize Depth and Timer Gauges
        depthText.text = string.Format("{0:00.0} m", playerDepthMeter);
        timeText.text = string.Format("{0:00}:{1:00}", 10, 0);

        // Initialize Text Objects
        instructionText.text = ("You have 10 minutes to complete the survey!");
        airText.gameObject.SetActive(false);
        transectText.gameObject.SetActive(false);
        
        // Initialize depth needle (unused)
        // depthNeedle.transform.localRotation = Quaternion.Euler(0, 0, depthNeedleRotationZ);
 
        // Initialize air needle
        
        airNeedle.transform.localRotation = Quaternion.Euler(0, 0, airNeedleRotationZ);

        transectActive = true;
    }

    // Update is called once per frame 
    void FixedUpdate() {
        playerDepth = player.transform.position.y;

        // Change compass position
        dir.z = playerTransform.eulerAngles.y;
        compass.transform.localEulerAngles = dir;

        if (Underwater.headUnderwater) {
            
            // Update the player depth
            playerDepth = Mathf.Abs(playerDepth);
            playerDepthMeter = (playerDepth * 0.3048f) - 0.2f;    // 1ft = 0.3048m, 0.2m calibration
            // playerDepthFeet = playerDepth * (40f / 31f);

            // Update the angle for the depth needle (unused)
            // depthNeedleRotationZ = depthStartAngle + (playerDepth * depthScale);

            // Update the players time
            if (timeValue > 0)
            {
                timeValue -= Time.deltaTime;
            }
            else
            {
                timeValue = 0;
            }

            // Remove beginning message
            if (timeValue <= 599 && timeValue > 0f) {
                instructionText.gameObject.SetActive(false);
            }

            // Update the player's air
            if (timeValue <= 10f && timeValue > 0f) {
                airText.gameObject.SetActive(true);
                airText.text = string.Format("{0}s of air remaining", (int)timeValue);
            }

            // Air needle math: EndAngle - StartAngle = 140 - -91 = 231
            //                  TotalTime / TotalDeg = 600 sec / 231 deg = 2.6 sec/deg
            //                  CurAngle = ((TotalTime - CurTime) / AngleRate) + StartAngle

            float angle = ((totalTime - timeValue) / (totalTime / 231)) + startAngle;
            airNeedleRotationZ = angle;
        }

        // NOT UNDERWATER
        else {
            // Make sure depth is zero out of water
            playerDepth = 0;
            playerDepthMeter = 0;
            // playerDepthFeet = 0;

            // Don't change needle when out of water
            airNeedleRotationZ = airNeedle.transform.localRotation.eulerAngles.z;
            // depthNeedleRotationZ = depthNeedle.transform.localRotation.eulerAngles.z;
        }

        // use this for testing purposes
        /*if (transect == null) {
            spawnTransect(Test);
        }*/

        // TRANSECT TAPE HANDLING
        RaycastHit hit;

        // if raycast hits, it checks if it hit an object with the tag Player
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Piling") && !inTransect) {
            string newText = string.Format("Press X to begin transect on Piling {0}", int.Parse(hit.collider.gameObject.name));
            transectText.text = newText;
            transectText.gameObject.SetActive(true);

            // if X button pressed
            // Change to regular camera
            if (Input.GetKeyDown(KeyCode.X) || transectBtnPressed) {
                transectActive = true;
                transectBtnPressed = false;
                spawnTransect(hit.transform);
                // Debug.Log(hit.)
                // Debug.Log(hit.ToString());
                //Debug.Log("Spawn the transect here!");
            }
        }
        else if (inTransect && Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Piling")) {
            transectText.text = "Press B to remove transect";
            transectText.gameObject.SetActive(true);
            // if B button pressed
            // Change to regular camera
            /*
            if (OVRInput.Get(OVRInput.Button.Two)) {
                destroyTransect(hit.transform);
            }*/

            // destroys transect if player too far
            float distance = Vector3.Distance(new Vector3(transect.transform.position.x, 0f, transect.transform.position.z), 
                                                              new Vector3(player.transform.position.x, 0f, player.transform.position.z));
            if (Input.GetKeyDown(KeyCode.B) || transectBtnPressed || distance > maxDistance + 1){
                transectBtnPressed = false;
                destroyTransect(hit.transform);
                // Debug.Log("Destroy transect!");
                transectActive = false;
            }
        }
        else {
            transectText.gameObject.SetActive(false);
        }
        

        // Update the text object for depth
        depthText.text = string.Format("{0:00.0} m", playerDepthMeter);

        // Update the text object for time
        DisplayTime(timeValue);
    
        // Update analog depth gauge (unused)
        // depthNeedle.transform.localRotation = Quaternion.Euler(0, 180, depthNeedleRotationZ);

        // Update air gauge
        // Debug.Log("Time Fraction: " + airNeedleRotationZ.ToString());
        airNeedle.transform.localRotation = Quaternion.Euler(0, 0, airNeedleRotationZ);
    }

    // Converts Time to Displayable Units
    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeToDisplay == 0)
        {
            EndGame();
        }
    }

    // Moves to the end scene once timer finishes
    void EndGame()
    {
        var controller = player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        controller.UnlockCursor();

        Debug.Log("Change Scene");
        timeValue = 600;
        DontDestroyOnLoad(transectSystem);
        SceneManager.LoadScene("EndScene");
    }

    public void spawnTransect(Transform piling) {
        Dictionary<string, int> transObjs = new Dictionary<string, int>();
        inTransect = true;
        Quaternion rotation = player.transform.rotation * Quaternion.AngleAxis(180, Vector3.up);
        transect = Instantiate(transectPrefab, piling.position + new Vector3(0.0f, -16, 0.0f), rotation);

        // Do transect
        Vector3 top = piling.position + new Vector3(0, 100, 0);
        Vector3 bottom = piling.position + new Vector3(0, -100, 0);
        Collider[] hitColliders = Physics.OverlapCapsule(top, bottom, 10);
        //Debug.Log("dict length: " + hitColliders.Length);
        foreach (Collider c in hitColliders) {
            if (transObjs.ContainsKey(c.tag)) {
                transObjs[c.tag] = transObjs[c.tag] + 1;
            }
            else {
                transObjs.Add(c.tag, 1);
            }
        }
        
        transectSystem.current_piling = getCurrentPiling();
    }

    public void destroyTransect(Transform piling) {
        inTransect = false;
        Destroy(transect.gameObject);
        transectSystem.current_piling = -1;
    }

    /*  This function returns the Piling number that the player is currently looking at.
        Returns -1 if there no piling.*/
    public int getCurrentPiling()
    {
        RaycastHit hit;
        // if raycast hits, it checks if it hit an object with the tag Player
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, maxDistance) && hit.collider.gameObject.CompareTag("Piling"))
        {
            //Debug.Log(hit.collider.gameObject.name);
            int ret = int.Parse(hit.collider.gameObject.name);
            return ret;
        }
        return 0;
    }

    // Function selector for the transect button on the screen
    public void transectButton(){
        transectBtnPressed = true;
        Debug.Log("transectBtnPressed is true");
    }
}