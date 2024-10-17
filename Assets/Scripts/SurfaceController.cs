using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfaceController : MonoBehaviour
{
    private bool hasMoved = false;

    private IEnumerator MoveAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        transform.position += new Vector3(0, 4, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        BoxCollider surface = this.GetComponent<BoxCollider>();
        surface.isTrigger = false;


        Debug.Log("CollisionDetected");
        if (!hasMoved && other.GetComponent<CharacterController>())
        {
            StartCoroutine(MoveAfterDelay());
            hasMoved = true;

        }
    }
}
