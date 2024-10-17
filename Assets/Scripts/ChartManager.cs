using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChartManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Canvas canvas;

    public float xPadding;
    public float yPadding;

    public TMP_Text ScientistName;

    // Start is called before the first frame update
    void Start()
    {
        string name = PlayerPrefs.GetString("ScientistName");
        if (name.Length > 1)
        {
            ScientistName.text = name + "'s Dive Data";
        }
        else
        {
            ScientistName.text = "Your Dive Data";
        }

        DiveDataManager dataMgr = GameObject.FindObjectOfType<DiveDataManager>();

        int[,] data = dataMgr.DiveData;
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                    // instantiate the cell prefab
                    GameObject instance = Instantiate(cellPrefab);
                    instance.transform.SetParent(canvas.transform, false);
                    //instance.transform.parent = canvas.transform;
                    // move the cell to the corresponding location for the data
                    RectTransform rect = instance.GetComponent<RectTransform>();
                    rect.anchoredPosition += new Vector2(i * rect.rect.width + xPadding + 2, j * rect.rect.height + yPadding + 2);

                    //assign the data value to the text object
                    TMP_Text txt = instance.GetComponentInChildren<TMP_Text>();
                    txt.text = data[i, j].ToString();
             
            }
        }
    }

}
