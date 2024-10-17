using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/* Excel Export thanks to https://www.youtube.com/watch?v=sU_Y2g1Nidk&t=7s 
 * The TransectSystem holds species count data for all 27 pilings*/
public class TransectSystem : MonoBehaviour
{
    // Where the excel sheet gets written
    string filename = "";

    // List of pilings objects
    Piling[] pilings = new Piling[27];
    public int current_piling = -1;
    // References to UIController and Player objects
    public GameObject DiveUI;
    public GameObject Player;
    UIController uicontroller;

    

    // Fill the UIcontroller reference 
    private void Start()
    {

        uicontroller = DiveUI.GetComponent<UIController>();
        filename = Application.dataPath + "/test.csv";
        
    }

    // Initialize all 27 pilings
    public TransectSystem()
    {
        for (int i=0; i < 27; i++)
        {
            pilings[i] = new Piling();
        }
    }

    /* Increment the count of a species depth pair*/
    public void AddSpecies(String species, int depth)
    {



        //Debug.Log("Given Species: " + species + " Given Depth: " + depth);
        //int current_piling = uicontroller.getCurrentPiling();
        // Check to see if character is looking at a viable piling
        if (current_piling != -1)
        {
            //Debug.Log("Current piling: " + current_piling);
            int current_count = pilings[current_piling].Get(species, depth);
            //Debug.Log("Count before: " + current_count);
            current_count++;
            {
                if (current_count == 1)
                {
                    pilings[current_piling].dataDict.Add((species, depth), current_count);
                }
                else
                {
                    pilings[current_piling].Set(species, depth, current_count);
                }
            }
            //Debug.Log("Count after: " + pilings[current_piling].Get(species, depth));
        }
    }

    /* Decrement the count of a species depth pair*/
    public void SubtractSpecies(String species, int depth)
    {
        int current_piling = uicontroller.getCurrentPiling();
        int current_count = pilings[current_piling].Get(species, depth);
        if (current_piling != -1)
        {
            if (current_count > 0)
                current_count--;
            pilings[current_piling].dataDict.Add((species, depth), current_count);
        }
    }  

    /* Print all the piling data*/
    public void PrintPilings()
    {
        for (int i = 0; i < 27; i++)
        {
            Piling current_piling = pilings[i];
            Debug.Log("Piling " + i);
            var keys = current_piling.dataDict.Keys;
            foreach ((string,int)key in keys)
            {
                Debug.Log("Species: " + key.Item1 + " Depth: " + key.Item2 + " Count: " + current_piling.Get(key.Item1, key.Item2));
            }
        }
    }

    /*  TODO: Format the CSV file according to how Crow wants it. At the moment it spits out all of the transect data. Change it
        so that it only writes data for the focal species. */
    public void WriteCSV()
    {
        Debug.Log("Write CSV!");
        int write_flag = 0;
        for (int i = 0; i < 27; i++)
        {
            if (pilings[i].dataDict.Count > 0)
            {
                write_flag = 1;
                continue;
            }
        }
        if (write_flag == 1)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine(" , , , Number of Individuals Detected");
            Debug.Log(" , , , Number of Individuals Detected");
            tw.WriteLine("Scientist name, Piling number, Piling Depth [meters], Acorn Barnacle, Ochre Star, Purple Urchin, Starburst Anemone, White-plumed Anemone");
            Debug.Log("Scientist name, Piling number, Piling Depth [meters], Acorn Barnacle, Ochre Star, Purple Urchin, Starburst Anemone, White-plumed Anemone");
            tw.Close();

            tw = new StreamWriter(filename, true);
            for (int i = 0; i < 27; i++)
            {
                Piling current_piling = pilings[i];
                if (current_piling.dataDict.Count <= 0)
                {
                    continue;
                }
                var keys = current_piling.dataDict.Keys;
                for (int j = 0; j < 14; j++)
                {
                    int[] counts = new int[5];
                    foreach ((string, int) key in keys)
                    {
                        // At depth j, get all counts
                        if (key.Item2 == j)
                        {
                            if (key.Item1 == "Acorn Barnacle")
                            {
                                counts[0] = current_piling.Get(key.Item1, key.Item2);
                            }
                            else if (key.Item1 == "Ochre Star")
                            {
                                counts[1] = current_piling.Get(key.Item1, key.Item2);
                            }
                            else if (key.Item1 == "Purple Urchin")
                            {
                                counts[2] = current_piling.Get(key.Item1, key.Item2);
                            }
                            else if (key.Item1 == "Starburst Anemone")
                            {
                                counts[3] = current_piling.Get(key.Item1, key.Item2);
                            }
                            else // White-plumed Anemone
                            {
                                counts[4] = current_piling.Get(key.Item1, key.Item2);
                            }
                        }
                    }
                    
                    // Write counts for depth j
                    tw.WriteLine("Joe, " + i + ", " + j + ", " + counts[0] + ", " + counts[1] + ", " + counts[2] + ", " + counts[3] + ", " + counts[4]);
                    Debug.Log("Joe, " + i + ", " + j + ", " + counts[0] + ", " + counts[1] + ", " + counts[2] + ", " + counts[3] + ", " + counts[4]);
                }
            }
            tw.Close();
        }
    }

    /*  Includes the Keypresses needed to either print the transect in the console or write it to CSV.
        TODO: Make the WriteCSV call be at the end of ExPIERience.*/
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintPilings();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            WriteCSV();
        }
    }
}

/* The Piling class that represents a piling in the scene. Contains a single dictionary
 * that contains key value pairs of (species, depth) -> count. */
public class Piling
{
    // Key = (Species, depth)
    // Value = number of species at that depth
    public IDictionary<(String, int), int> dataDict = new Dictionary<(String, int), int>() {};
    // Add or set the count in the dictionary
    public void Set(string species, int depth, int value)
    {
        if (dataDict.ContainsKey((species, depth)))
        {
            dataDict[(species, depth)] = value;
        }
        else
        {
            dataDict.Add((species, depth), value);
        }
    }

    /* Get the count of species at the given depth, 0 if the key is not in the dictionary*/
    public int Get(string species, int depth)
    {
        if (dataDict.ContainsKey((species, depth)))
        {
            return dataDict[(species, depth)];
        }
        return 0;
    }
}