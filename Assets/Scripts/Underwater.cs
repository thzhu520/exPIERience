/******************************************************************************

Controls underwater visibility and player movement while underwater.

******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Underwater : MonoBehaviour {
    public float waterLevel = 0f;
    public float neutralThreshold = 1.0f;
    public float floatScaler = 0.025f;
    public float floatDecay = 0.0125f;
    public float maxBuoyancy = 1.5f;
    public float minBuoyancy = -1.5f;
    public static bool isUnderwater;
    public static bool headUnderwater;
    public GameObject playerHead;
    public AudioSource audioSource;
    public AudioClip swimmingAudio;

    public float abovewaterFogDensity;
    public Color normalColor;
    public Color underwaterColor;

    private float underwaterGravity;
    private float normalGravity;
    private float buoyancy;

    private bool descend;
    private bool ascend;

    bool firstEnter;

    protected CharacterController Controller = null;
    // Use this for initialization
    void Start() {
        Controller = gameObject.GetComponent<CharacterController>();

        //normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        //underwaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);

        isUnderwater = false;
        headUnderwater = false;
        underwaterGravity = 0.0f;
        buoyancy = 0.0f;
        Physics.gravity = new Vector3(0f, -9.81f, 0f);
        normalGravity = Physics.gravity.y;
        SetNormal();

        firstEnter = false;
    }

    // Update is called once per frame
    void Update() {
        // Physics.gravity = new Vector3(0, normalGravity, 0);
        
        // Turn gravity off and set visibility to normal
        if (playerHead.transform.position.y <= (waterLevel + 1.0f)) {
            
                // playerHead.transform.position = new Vector3(playerHead.transform.position.x, 0, playerHead.transform.position.z);
                SetUnderwater();
            
        }
        

        if (isUnderwater) {
            /* Collect keyboard inputs for ascend and descend */
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL //these are the controls being used in the editor
            descend = Input.GetKey(KeyCode.F) && isUnderwater;
            ascend = Input.GetKey(KeyCode.Space) && isUnderwater;

        //these are touch controls used when running the program on the phone
        //specify the platform and also specify that you are NOT running the program in the Editor
        #elif UNITY_ANDROID && !UNITY_EDITOR 
            ascend = TouchControlAscend.touchAscend;
            descend= TouchControlDescend.touchDescend;
        #endif

            if (descend) //buoyancy decrease
                buoyancy -= floatScaler;

            if (ascend) //buoyancy increase
                buoyancy += floatScaler;

            /* Lock buoyancy to min / max */
            buoyancy = (buoyancy > maxBuoyancy) ? maxBuoyancy : buoyancy;
            buoyancy = (buoyancy < minBuoyancy) ? minBuoyancy : buoyancy;

            /* decay the buoyancy 
               allows player depth to be locked */
            if (buoyancy > 0) {
                buoyancy -= floatDecay;
                buoyancy = (buoyancy > 0) ? buoyancy : 0;
            }
            else if (buoyancy < 0) {
                buoyancy += floatDecay;
                buoyancy = (buoyancy < 0) ? buoyancy : 0;
            }

            /* Move the amount that buoyancy dictates */
            Controller.Move(new Vector3(0, buoyancy, 0) * Time.deltaTime);
        }
    }

    // Sets visibility to normal
    void SetNormal() {
        isUnderwater = false;
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = abovewaterFogDensity;
        Physics.gravity = new Vector3(0, normalGravity, 0);
        headUnderwater = false;
    }

    // Sets visibility to underwater
    void SetUnderwater() {
        if (firstEnter == false)
        {
            audioSource.Play();
            firstEnter = true;
            StartCoroutine(AudioDelay());
        }
        isUnderwater = true;
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.025f;
        Physics.gravity = new Vector3(0, 0, 0);
        //Physics.gravity = new Vector3(0, 9.81f, 0);
        //transform.position = new Vector3(transform.position.x + 10, 0f, transform.position.z);
        headUnderwater = true;
    }

    private IEnumerator AudioDelay()
    {
        yield return new WaitForSeconds(5);
        audioSource.volume = 0.08f;
        audioSource.clip = swimmingAudio;
        audioSource.loop = true;
        audioSource.Play();
    }
}

