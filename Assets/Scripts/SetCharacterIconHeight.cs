using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCharacterIconHeight : MonoBehaviour
{
    // Updates the height of the minimap icon for the user

    //object that I want to follow in this case: FPSController
    public Transform target;
 
    // Update is called once per frame
    void Update()
    {

        if (Underwater.isUnderwater)
        {
            transform.position = new Vector3(target.position.x, -25f, target.position.z);
            //transform.position.y = 13f;
        }
        else {
            transform.position = new Vector3(target.position.x, 13f, target.position.z);
        }
        
    }
}
