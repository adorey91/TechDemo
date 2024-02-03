using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravity : MonoBehaviour
{
    public List<Rigidbody> MazeList; // Reference to the original Maze prefab
    public List<Rigidbody> RangeList; // Reference to the original Range prefab

    public bool turnOffGravity;


    public void Update()
    {
        if (turnOffGravity)
        {
            for(int i = 0; i < RangeList.Count; i++)
            {
                RangeList[i].useGravity = false;
                RangeList[i].isKinematic = false;
            }
            for(int i = 0; i< MazeList.Count; i++)
            {
                MazeList[i].useGravity = false;
                MazeList[i].isKinematic = false;
            }
            MazeList.Clear();
            RangeList.Clear();
            turnOffGravity = false;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            turnOffGravity = true;
    }

    public void AddRigidbodiesFromChildren(GameObject obj, List<Rigidbody> rigidbodyList)
    {
        Rigidbody[] rigidbodies = obj.GetComponentsInChildren<Rigidbody>();
        rigidbodyList.AddRange(rigidbodies);
    }
}