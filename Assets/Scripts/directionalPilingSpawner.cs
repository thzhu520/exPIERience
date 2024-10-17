using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This is essentially a last minute rushed attempt to clamp certain species to the pilings in accordance with
 their rotation. (rotating sprites changes it's position behavior as well, so need to account for that.)
If you want to add a sprite that requires the 'back' to be flush against the piling, you can use this and
asjust the padding values to your liking.*/

public class directionalPilingSpawner : MonoBehaviour
{
    public GameObject prefab;

    public Vector3 padding0;
    public Vector3 padding45;
    public Vector3 padding90;
    public Vector3 padding135;
    public Vector3 padding180;
    public Vector3 padding270;

    public Vector2 heightBounds;
    public float spawnCount;

    public string Tag;
    
    private void Awake()
    {
        Vector3 position = Vector3.zero;
        Quaternion rotation = Quaternion.identity;

        for (int i = 0; i < spawnCount; i++)
        {
            (position, rotation) = clamp();

            // instantiate the object
            GameObject instance = Instantiate(prefab, position, rotation);
            //instance.transform.Rotate(rotation.eulerAngles, Space.Self);

            // readability
            instance.name = instance.name + i;

            // adding tag, is there better way?
            instance.tag = Tag;
        }
    }

    private (Vector3, Quaternion) clamp()
    {
        // init zero
        Vector3 position = Vector3.zero; 
        Quaternion rotation = Quaternion.identity;

        int randomIndex = Random.Range(0, 6);

        GameObject[] pilings = GameObject.FindGameObjectsWithTag("Piling");

        if (pilings.Length > 0)
        {
            GameObject randomPiling = pilings[Random.Range(0, pilings.Length)];

            switch (randomIndex)
            {
                // no rotation
                case 0:
                    position = randomPiling.transform.position + padding0;
                    rotation = Quaternion.AngleAxis(0, Vector3.up);
                    break;
                // 90 degree rotation
                case 1:
                    position = randomPiling.transform.position + padding90;
                    rotation = Quaternion.AngleAxis(90, Vector3.up);
                    break;
                // 180 
                case 2:
                    position = randomPiling.transform.position + padding180;
                    rotation = Quaternion.AngleAxis(180, Vector3.up);
                    break;
                // 270
                case 3:
                    position = randomPiling.transform.position + padding270;
                    rotation = Quaternion.AngleAxis(270, Vector3.up);
                    break;
                // 45
                case 4:
                    position = randomPiling.transform.position + padding45;
                    rotation = Quaternion.AngleAxis(45, Vector3.up);
                    break;
                // 225
                case 5:
                    position = randomPiling.transform.position + padding135;
                    rotation = Quaternion.AngleAxis(225, Vector3.up);
                    break;
            }
        }
        position.y = Random.Range(heightBounds.x, heightBounds.y);
        return (position, rotation);
    }
    
}
