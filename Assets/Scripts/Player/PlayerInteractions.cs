using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Building Settings")]
    public ResetBuildings resetBuildings;
    public bool reset;
    public bool fall;
    public bool gravity;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reset"))
        {
            reset = true;
            gravity = false;
            fall = false;
        }
        if (other.gameObject.CompareTag("Gravity"))
        {
            gravity = true;
            reset = false;
            fall = false;
        }
        if (other.gameObject.CompareTag("Fall"))
        {
            fall = true;
            reset = false;
            gravity = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reset"))
        {
            resetBuildings.lever.transform.rotation = resetBuildings.leverRotation;
            resetBuildings.leverRotated = false;
        }
    }
}
