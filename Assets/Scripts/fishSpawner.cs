using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public int spawnCount;
    public Vector2 heightBounds;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-75, 75), Random.Range(heightBounds.x, heightBounds.y), Random.Range(-75, 75));
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            if (fishPrefab.CompareTag("School")) {

                for (int j = 0; j < 5; j ++)
                {
                    GameObject schoolInstance = Instantiate(fishPrefab, randomPosition + new Vector3(j, 0, j), Quaternion.identity);
                    schoolInstance.transform.rotation = randomRotation;
                    schoolInstance.name = schoolInstance.name + i + j;
                }
                i += 4;
            }
            GameObject fishInstance = Instantiate(fishPrefab, randomPosition, Quaternion.identity);
            fishInstance.transform.rotation = randomRotation;
            fishInstance.name = fishInstance.name + i;
        }
    }

}
