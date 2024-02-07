using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupGem : MonoBehaviour
{
    public GameObject gemUIPrefab;
    public GameObject gemPrefab;
    [SerializeField] private GameObject UIparent;

    public float spawnAreaWidth = 15f;
    public float spawnAreaLength = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            AddGemUI();
            SpawnObject();
            Destroy(this.gameObject);
        }
    }

    void AddGemUI()
    {
        GameObject NewUIGem = Instantiate(gemUIPrefab);
        NewUIGem.transform.parent = UIparent.transform;
    }

    void SpawnObject()
    {
        // Generate random position within the specified bounds
        float randomX = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
        float randomZ = Random.Range(-spawnAreaLength / 2, spawnAreaLength / 2);

        Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ);

        while(!IsPositionClear(spawnPosition))
        {
            if (IsPositionClear(spawnPosition))
                Instantiate(gemPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsPositionClear(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, 1f);

        return colliders.Length == 0;
    }
}