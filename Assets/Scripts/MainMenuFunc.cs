using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*  This script handles the MainMenu scene interactions. */
public class MainMenuFunc : MonoBehaviour
{
    /*  For clicking noise when clicking a button */
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;

    /* Canvas References */
    public GameObject MainCanvas;
    public GameObject JournalCanvas;

    /* Function for the explore button that loads the pier scene */
    public void Explore() {
        audioSource.PlayOneShot(clip, volume);
        SceneManager.LoadScene("IntroScene");
    }
    
    /* Function for accessing the Journal */
    public void Journal()
    {
        MainCanvas.SetActive(false);
        JournalCanvas.SetActive(true);
    }

    /* Function for quitting the game */
    public void Quit()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("Exit");
        Application.Quit();
    }

    /* Function for accessing the ContactUs Scene*/
    public void Contact()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("Contact");
        SceneManager.LoadScene("ContactScene");
    }

    /* Function for accessing the AboutUs Scene */
    public void About()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("About");
        SceneManager.LoadScene("AboutScene");
    }

    /* Function for going back to the main menu from the Journal */
    public void BackJournal()
    {
        JournalCanvas.SetActive(false);
        MainCanvas.SetActive(true);
    }
}
