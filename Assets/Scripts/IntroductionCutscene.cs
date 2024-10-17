using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/*  This script handles the scene transition of the introduction cutscene to main menu  */
public class IntroductionCutscene : MonoBehaviour
{
    public VideoPlayer Introduction; // Attach Video Player component found in hierarchy
    
    public VideoPlayer Opening;
    


    /* Scene Macro */
    const int BUILD_INDEX_MENU = 0;
    const int BUILD_INDEX_EXPLORE = 1;
    /* Used to know when the scene is being called, before or after menu */
    public static int introFlag = 0;
    // Start is called before the first frame update
    void Start()
    {
        Introduction.url = System.IO.Path.Combine(Application.streamingAssetsPath, "ExPIERience Startup.mp4");
        Opening.url = System.IO.Path.Combine(Application.streamingAssetsPath, "ExPIERience Intro Cutscene 2.mp4");
        if (introFlag == 0)
        {
            //play introduction and load scene once finished
            Introduction.Play();
            Introduction.loopPointReached += LoadScene;
        }
        else
        {
            //play oppening and load scene once finished
            Opening.Play();
            Opening.loopPointReached += LoadScene;
        }
    }

    void LoadScene(VideoPlayer vp)
    {
        if(introFlag == 0)
        {
          introFlag++;
          SceneManager.LoadScene("Menu");
        }
        else
          SceneManager.LoadScene("PierScene");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (introFlag == 0)
            {
                introFlag++;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene("PierScene");
            }
        }
    }
}
