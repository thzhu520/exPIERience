using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*  This script controls the HUD interactions. It takes in a reference to
 *  the HUD canvas, Journal canvas, and HamburgerCanvas for easy switching
    between them. */
public class HelpMenu : MonoBehaviour
{

    public GameObject helpUI;
    public GameObject hud;

    /* Check if we want the HUD activated or deactivated */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            helpUI.SetActive(!helpUI.activeSelf);
            hud.SetActive(!hud.activeSelf);
            // (GameObject.Find("HelpBackBtn").GetComponent<Button>()).onClick.Invoke();
        }
    }
}
