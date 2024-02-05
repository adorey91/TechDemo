using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBuildings : LeverController
{
    public GameObject originalMazePrefab; // Reference to the original Maze prefab
    public GameObject originalRangePrefab; // Reference to the original Range prefab

    private GameObject mazeInstance; // Reference to the instantiated Maze object
    private GameObject rangeInstance; // Reference to the instantiated Range object

    private Vector3 originalMazePosition;
    private Vector3 originalRangePosition;

    public GravityControl gravityScript;
    public FallControl fallScript;

    public bool resetRequested;


    void Update()
    {
        if (resetRequested & !leverRotated)
        {
            MoveLever(45);
            ResetOtherLevers();
            gravityScript.ClearLists();
            FindMazeAndRangeInstance();

            if (mazeInstance != null)
                Destroy(mazeInstance);
            if (rangeInstance != null)
                Destroy(rangeInstance);

            mazeInstance = Instantiate(originalMazePrefab, originalMazePosition, transform.rotation);
            rangeInstance = Instantiate(originalRangePrefab, originalRangePosition, transform.rotation);
            gravityScript.AddRigidbodiesFromChildren(mazeInstance, gravityScript.MazeList);
            gravityScript.AddRigidbodiesFromChildren(rangeInstance, gravityScript.RangeList);

            resetRequested = false;  
        }
    }
    void FindMazeAndRangeInstance()
    {
        mazeInstance = GameObject.Find("Maze(Clone)");
        rangeInstance = GameObject.Find("Range(Clone)");

        if (mazeInstance != null)
            originalMazePosition = mazeInstance.transform.position;
        else
            Debug.LogError("Maze GameObject not found in the scene!");

        if (rangeInstance != null)
            originalRangePosition = rangeInstance.transform.position;
        else
            Debug.LogError("Range GameObject not found in scene!");
    }

    void ResetOtherLevers()
    {
        gravityScript.lever.transform.rotation = gravityScript.leverRotation;
        gravityScript.turnOffGravity = false;
        gravityScript.leverRotated = false;
        gravityScript.gravityText.SetActive(false);
        fallScript.lever.transform.rotation = fallScript.leverRotation;
        fallScript.fallApartNow = false;
        fallScript.leverRotated = false;
        fallScript.fallText.SetActive(false);
    }
}