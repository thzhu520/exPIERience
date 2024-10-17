using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepUnlocked : MonoBehaviour
{
    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
