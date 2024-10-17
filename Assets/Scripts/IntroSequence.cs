using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Script for the intro down the stairs to diver sequence  */

public class IntroSequence : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] Camera cam2;
    [SerializeField] GameObject hud;
    [SerializeField] GameObject hamburgerCanvas;
    [SerializeField] GameObject icon;
    [SerializeField] GameObject diver;
    
    Animator camAnim;
    // Start is called before the first frame update
    void Awake()
    {
        camAnim = cam2.GetComponent<Animator>();
        StartCoroutine(StartIntro());
    }

    private void Update()
    {

    }

    IEnumerator StartIntro()
    {
        icon.gameObject.SetActive(false);
        hud.gameObject.SetActive(false);
        hamburgerCanvas.SetActive(false);
        mainCam.SetActive(false);
        cam2.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        camAnim.Play("IntroScene");
        yield return new WaitForSeconds(4.7f);
        cam2.gameObject.SetActive(false);
        Destroy(diver);
        mainCam.SetActive(true);
        hud.gameObject.SetActive(true);
        hamburgerCanvas.SetActive(true);
        icon.gameObject.SetActive(true);
    }
}
