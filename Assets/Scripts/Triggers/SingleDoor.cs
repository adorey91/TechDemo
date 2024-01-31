using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDoor : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private bool openDoor;
    [SerializeField] private bool closeDoor;
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Transform middlePosition;
    [SerializeField] private Transform finalPosition;

    private void Start()
    {
        startPos = door.position;
    }

    private void Update()
    {
        if (openDoor)
        {
            door.localPosition = Vector3.MoveTowards(door.localPosition, middlePosition.localPosition, Time.deltaTime);
           // door.localPosition = Vector3.MoveTowards(door.localPosition, finalPosition.localPosition, Time.deltaTime);
        }
    
            //StartCoroutine(OpenDoors());
       
        else if(closeDoor)
        {
           // door.localPosition = Vector3.MoveTowards(door.localPosition, middlePosition.localPosition, Time.deltaTime);

            door.localPosition = Vector3.MoveTowards(door.localPosition, startPos, Time.deltaTime);

        }
            //StartCoroutine(CloseDoors());  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            openDoor = true;
            closeDoor = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            openDoor = false;
            closeDoor = true;
        }
    }

    IEnumerator OpenDoors()
    {
        door.localPosition = Vector3.MoveTowards(door.localPosition, middlePosition.localPosition, Time.deltaTime);

        yield return new WaitForSeconds(1.2f);

        door.localPosition = Vector3.MoveTowards(door.localPosition, finalPosition.localPosition, Time.deltaTime);
    }

    IEnumerator CloseDoors()
    {
        door.localPosition = Vector3.MoveTowards(door.localPosition, middlePosition.localPosition, Time.deltaTime);

        yield return new WaitForSeconds(1.2f);

        door.localPosition = Vector3.MoveTowards(door.localPosition, startPos, Time.deltaTime);
    }
}
