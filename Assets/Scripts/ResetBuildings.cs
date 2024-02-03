using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBuildings : MonoBehaviour
{
    public GameObject originalMazePrefab; // Reference to the original Maze prefab
    public GameObject originalRangePrefab; // Reference to the original Range prefab

    public Rigidbody originalMaze;
    public Rigidbody originalRange;

    private GameObject mazeInstance; // Reference to the instantiated Maze object
    private GameObject rangeInstance; // Reference to the instantiated Range object

    [SerializeField] private Vector3 originalMazePosition;
    [SerializeField] private Vector3 originalRangePosition;

    public NoGravity noGravityScript;

    public bool Reset;


    public void Update()
    {
        if (Reset)
        {
            // Check if the objects are already instantiated
            if (mazeInstance != null)
                Destroy(mazeInstance); // Destroy the existing Maze object
            if (rangeInstance != null)
                Destroy(rangeInstance); // Destroy the existing Range object

            // Instantiate new objects from their original prefabs at the stored positions
            mazeInstance = Instantiate(originalMazePrefab, originalMazePosition, transform.rotation);
            rangeInstance = Instantiate(originalRangePrefab, originalRangePosition, transform.rotation);
            noGravityScript.AddRigidbodiesFromChildren(mazeInstance, noGravityScript.MazeList);
            noGravityScript.AddRigidbodiesFromChildren(rangeInstance, noGravityScript.RangeList);

            Reset = false; // Reset the flag after performing the reset
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            Reset = true;
    }
}