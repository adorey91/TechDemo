using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelBoundaries;

    public GameObject originalMazePrefab; // Reference to the original Maze prefab
    public GameObject originalRangePrefab; // Reference to the original Range prefab

    public GameObject mazeInstance; // Reference to the instantiated Maze object
    public GameObject rangeInstance; // Reference to the instantiated Range object

    public Vector3 originalMazePosition;
    public Vector3 originalRangePosition;

    public GravityControl noGravityScript;

    public GameObject gemPrefab;
    private GameObject gemInstanceClone;
    public Vector3 gemPos1;
    public Vector3 gemPos2;

    [SerializeField] private int count = 0;
    private int gemHolderCount;

    public void Start()
    {
        gemHolderCount = 0;
        GameObject.Instantiate(levelBoundaries);
        BuildBuildings();
    }

    public void Update()
    {
        CreateGem();
    }

    void CreateGem()
    {
        gemInstanceClone = GameObject.Find("GemPickup(Clone)");
        
        if (gemInstanceClone == null && gemHolderCount <10)
        {
            InstantiateGem();
            gemHolderCount++;
        }
    }

    void InstantiateGem()
    {
        gemPos1 = new Vector3(-7.82f, 1.059f, 11.72f);
        gemPos2 = gemPos1 + new Vector3(10, 0, 10);

        if(count == 0)
        {
            gemInstanceClone = Instantiate(gemPrefab, gemPos2, Quaternion.identity);
            count++;
        }
        else if (count == 1)
        {
            gemInstanceClone = Instantiate(gemPrefab, gemPos1, Quaternion.identity);
            count--;
        }
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