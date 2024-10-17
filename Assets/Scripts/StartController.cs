using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    /*  For clicking noise when clicking a button */
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    private string scientistName;

    /* Function for the initial start button that starts the simulation */
    public void Begin() {
        audioSource.PlayOneShot(clip, volume);
        PlayerPrefs.SetString("ScientistName", scientistName);
        SceneManager.LoadScene("IntroScene");
    }

    /* Function for quitting the game */
    public void Quit()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("Exit");
        Application.Quit();
    }

    /* Function for reading scientist name */
    public void ReadInput(string s)
    {
        scientistName = s;
        Debug.Log(scientistName);
    }
}
