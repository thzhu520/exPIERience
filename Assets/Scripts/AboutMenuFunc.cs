using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* This script handles the About scene interactions. */
public class AboutMenuFunc : MonoBehaviour
{
    /* For clicking noise when clicking a button */
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    /* Function for going back to the Menu Scene */
    public void BackMenu()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("MainMenu");
        SceneManager.LoadScene("Menu");
    }

}
