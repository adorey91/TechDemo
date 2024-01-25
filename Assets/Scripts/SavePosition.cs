using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePosition : MonoBehaviour
{
    public Vector3 playerPosition;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boundary"))
            playerPosition = this.gameObject.transform.position;
    }
}
