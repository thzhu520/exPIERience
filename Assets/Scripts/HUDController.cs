using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  This script controls the HUD interactions. It takes in a reference to
 *  the HUD canvas, Journal canvas, and HamburgerCanvas for easy switching
    between them. */
public class HUDController : MonoBehaviour
{
    public GameObject FPS;
    public GameObject HUDCanvas;
    public GameObject JournalCanvas;
    public GameObject HelpCanvas;
    public CanvasGroup HamburgerCanvas;
    public bool isHUDActive;
    public bool isJournalActive;
    public bool isHelpActive;

    /* The HUD is active at start and the rest are deactivated */
    private void Start()
    {
        isHUDActive = true;
        isJournalActive = false;
        isHelpActive = false;
        HUDCanvas.SetActive(true);
        JournalCanvas.SetActive(false);
        //HelpCanvas.SetActive(false);
        //DisableGroup(HelpCanvas);
        DisableGroup(HamburgerCanvas);
    }

    /* Check if we want the HUD activated or deactivated */
    private void Update()
    {
        if (isHUDActive)
        {
            HUDCanvas.SetActive(true);
        }
        else
        {
            HUDCanvas.SetActive(false);
        }
        /*
        if (Input.GetKeyDown(KeyCode.H)) {
            if (!isHelpActive) {
                PressHelp();
            }
            else {
                Debug.Log("press HELP BACK");
                isHelpActive = false;
                GoBackHelp();
            }
        }*/

        //Looks for keypress to activate popUpJournal
        if (Input.GetKeyDown(KeyCode.J)){
            if (!isJournalActive){
                Cursor.lockState = CursorLockMode.None;
                PressJournal();
            }
            else{
                Debug.Log("press JOURNAL BACK");
                isJournalActive = false;
                FPS.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                GoBackJournal();
           }
        }
    }

    public void DisableGroup(CanvasGroup group)
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
    }

    public void EnableGroup(CanvasGroup group)
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
    }

    /* Function for hamburger menu button */
    public void PressMenu()
    {
        isHUDActive = false;
        EnableGroup(HamburgerCanvas);
        Debug.Log("Pressed the pause");
    }

    /* Function for journal button */
    public void PressJournal()
    {
        isHUDActive = false;
        JournalCanvas.SetActive(true);  
        isJournalActive = true;
        Debug.Log("Pressed the Journal");
    }

    /* Function for going back to the game from the Journal */
    public void GoBackJournal()
    {
        isHUDActive = true;
        JournalCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        isJournalActive = false;
    }

    public void PressHelp()
    {
        isHUDActive = false;
        HelpCanvas.SetActive(true);
        isHelpActive = true;
        Debug.Log("Pressed the Help");
    }

    /* Function for going back to the game from the help menu */

    public void GoBackHelp()
    {
        isHUDActive = true;
        //HelpCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        isHelpActive = false;
    }
}
