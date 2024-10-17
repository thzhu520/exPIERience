using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*  This script takes care of the hamburger menu interaction in the pier scene.
 *  It uses references to the HUD canvas and the Hamburger menu canvas
 *  for easy deactivtation of either*/
public class HamburgerMenu : MonoBehaviour
{
    public GameObject HUDCanvasRef;
    public GameObject HamburgerCanvasRef;

    /* Function for back button */
    public void Back() 
    {
        Debug.Log("Back");
        var controller = HUDCanvasRef.GetComponent<HUDController>();
        controller.isHUDActive = true;
        controller.DisableGroup(HamburgerCanvasRef.GetComponent<CanvasGroup>());
        HUDCanvasRef.SetActive(true);    
    }

    /* Function for FAQ button */
    public void FAQ() 
    {
        //TODO: Implement a FAQ/tutorial page
        Debug.Log("Implement FAQ!");
    }

    /* Function for exit to main menu */
    public void ExitToEndScene() 
    {
        SceneManager.LoadScene("EndScene");
    }

    /* Function for exiting the game */
    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

}
