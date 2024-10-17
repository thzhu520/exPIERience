using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script controls the EndMenu scene movements. */

public class EndMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToScene(string scene) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
