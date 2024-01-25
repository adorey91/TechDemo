using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelBoundaries;

    public void Start()
    {
        GameObject.Instantiate(levelBoundaries);
    }
}
