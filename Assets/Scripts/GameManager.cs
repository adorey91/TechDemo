using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelBoundaries;

    public GameObject originalMazePrefab; // Reference to the original Maze prefab
    public GameObject originalRangePrefab; // Reference to the original Range prefab

    private GameObject mazeInstance; // Reference to the instantiated Maze object
    private GameObject rangeInstance; // Reference to the instantiated Range object

    private Vector3 originalMazePosition;
    private Vector3 originalRangePosition;

    public NoGravity noGravityScript;

    public void Start()
    {
        GameObject.Instantiate(levelBoundaries);
        BuildBuildings();
    }

    void BuildBuildings()
    {
        originalMazePosition = new Vector3(31.4f, 0f, -31.19f);
        originalRangePosition = new Vector3(-10f, 0, -35.61f);

        mazeInstance = Instantiate(originalMazePrefab, originalMazePosition, transform.rotation);
        rangeInstance = Instantiate(originalRangePrefab, originalRangePosition, transform.rotation);

        noGravityScript.AddRigidbodiesFromChildren(mazeInstance, noGravityScript.MazeList);
        noGravityScript.AddRigidbodiesFromChildren(rangeInstance, noGravityScript.RangeList);
    }
}