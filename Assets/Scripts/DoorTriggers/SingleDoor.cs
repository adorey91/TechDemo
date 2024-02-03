using System.Collections;
using UnityEngine;

public class SingleDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float openRotation;
    [SerializeField] private float closeRotation;
    [SerializeField] private float speed;
    bool opening;
    
    Vector3 currentRot;

    private void Update()
    {
        currentRot = door.transform.localEulerAngles;

        if (opening)
            door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRotation, currentRot.z), speed * Time.deltaTime);
        else
            door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRotation, currentRot.z), speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            opening = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            opening = false;
    }
}