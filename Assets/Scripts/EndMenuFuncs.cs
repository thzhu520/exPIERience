using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

/* This script handles the EndMenu scene interactions. */

public class EndMenuFuncs : MonoBehaviour
{
    /* For clicking noise when clicking a button */
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    public void Menu()
    {
        audioSource.PlayOneShot(clip, volume);
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }

        public void Download()
    {
        SceneManager.LoadScene("DataScene");
    }
}
