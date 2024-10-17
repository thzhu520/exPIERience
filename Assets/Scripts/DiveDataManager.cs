using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiveDataManager : MonoBehaviour
{
    public int[,] DiveData = new int[14,6];

    void Start()
    {
        for (int i = 0; i < DiveData.GetLength(0); i++)
        {
            for (int j = 0; j < DiveData.GetLength(1); j++)
            {
                DiveData[i, j] = 0;
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void addData(int depth, int nameID)
    {
        DiveData[depth, nameID - 1] += 1;
    }
}
