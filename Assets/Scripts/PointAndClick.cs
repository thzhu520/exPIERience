using UnityEngine;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Collections;
using System.Collections;

/* Script that controls all of the click functionality */

/* Script that controls all of the click functionality */
//See if this enum can be moved to its own file
public class PointAndClick : MonoBehaviour
{
    public GameObject TransectManager;  // Reference to the TransectManager which holds the TransectSystem
    public UIController ui;
    public Camera mainCamera;
    TransectSystem transectSystem;
    public Transform FirstPersonController;
    public Material selectedMaterial;

    public DiveDataManager dataMgr;

    public bool isButtonPressed;
    public UIController uiController;
    private void Start()
    {
        isButtonPressed = false;
        transectSystem = TransectManager.GetComponent<TransectSystem>();
    }
    public void pressButton()
    {
        isButtonPressed = true;
        // Debug.Log("Pressed the Count Button");
        var screenPointToRay = mainCamera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        // Use Physics.Raycast instead of PhysicsWorld.CastRay
        RaycastHit[] hits = Physics.RaycastAll(screenPointToRay, 1000);

        foreach (RaycastHit hit in hits) {
            var selectedObject = hit.collider.gameObject;
            // Debug.Log("Selected Object: " + selectedObject.name);

            // Use GetComponent instead of GetComponentData and HasComponent

            int depth = (int)hit.transform.position.y;
            depth = Mathf.Abs(depth);
            depth = Mathf.RoundToInt((depth * 0.3048f) - 0.2f); // 1ft = 0.3048m, 0.2m calibration
            //Debug.Log("Depth calculated to meters: " + depth);

            float distance = Vector3.Distance(FirstPersonController.position, hit.transform.position);

            if (selectedObject.CompareTag("PilingCreature") && uiController.transectActive && distance < 3)
            {

                NameTag nameTag = selectedObject.GetComponent<NameTag>();
                int nameID = nameTag.nameID;
                var species = getSpeciesName(nameID);
                transectSystem.AddSpecies(species, depth);
                Debug.Log("Added " + species + " at depth " + depth);

                dataMgr.addData(depth, nameID);
                MeshRenderer mesh = FindMeshRendererInChildren(selectedObject.gameObject);

                mesh.material = selectedMaterial;
            }
            else
            {
                // Debug.Log("Unrecognized object selected" + selectedObject.name);
            }
        }
        
    }
    public string getSpeciesName(int nameID)
    {
        switch (nameID)
        {
            case 1:
                return "White Plumed Anemone";
            case 2:
                return "Acorn Barnacle";
            case 3:
                return "Ochre Star";
            case 4:
                return "Purple Urchin";
            case 5:
                return "Seaweed";
            case 6:
                return "Starburst Anemone";
            case 7:
                return "Geoduck";
            default:
                Debug.Log("Species out of range!");
                return "Unknown";
        }
    }

    public MeshRenderer FindMeshRendererInChildren(GameObject parent)
    {
        MeshRenderer meshRenderer = parent.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            return meshRenderer;
        }

        foreach (Transform child in parent.transform)
        {
            MeshRenderer childMeshRenderer = FindMeshRendererInChildren(child.gameObject);
            if (childMeshRenderer != null)
            {
                return childMeshRenderer;
            }
        }

        return null;
    }
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            pressButton();
        }
        if (mainCamera != null)
        {
            var screenPointToRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        }
        //var screenPointToRay = main.ScreenPointToRay(Input.mousePosition);
    }
}