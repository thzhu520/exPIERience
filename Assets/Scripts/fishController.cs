using UnityEngine;
public class fishController : MonoBehaviour
{
    public float movementSpeed;
    public float avoidanceRange = 2f;
    public bool flippedAnimation;


    private Rigidbody rb;
    float rotateSpeed = 40f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //InvokeRepeating("randomRotation", Random.Range(1f, 2f), Random.Range(0.5f, 2f));
    }

    void randomRotation()
    {
        transform.Rotate(Vector3.up, Random.Range(-1f, 1f));
        transform.Rotate(new Vector3(1, 0, 0), Random.Range(-0.2f, 0.2f));
        //Debug.Log("Rotation" + transform.rotation);
    }

    void FixedUpdate()
    {
        Vector3 direction;
        if (flippedAnimation)
        {
            direction = (transform.forward) * -1;
        }
        else
        {
            direction = (transform.forward);
        }
        //direction.y = 0;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, avoidanceRange))
        {
            //Quaternion targetRandomRotation = Quaternion.Euler(0f, Random.Range(130f, 270f), 0f);

            transform.Rotate(Vector3.up, Random.Range(135f, 225f));

            // Calculate the rotation needed to reach the target rotation
            //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRandomRotation, rotateSpeed * Time.deltaTime);

            // Apply the new rotation to the game object
            //transform.rotation = newRotation;
            // Apply the new rotation to the game object
            //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotateSpeed);

            if (flippedAnimation)
            {
                direction = (transform.forward) * -1;
            }
            else
            {
                direction = (transform.forward);
            }

        }

        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
    }
}