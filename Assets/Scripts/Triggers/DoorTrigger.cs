using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public float doorSpeed;
    [SerializeField] private GameObject doorHolder;
    [SerializeField] private GameObject leftSideDoor;
    [SerializeField] private GameObject rightSideDoor;
    [SerializeField] private bool openDoor;
    [SerializeField] private bool closeDoor;
    [SerializeField] private bool stayOpen;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private Vector3 leftDoorTarget;
    [SerializeField] private Vector3 rightDoorTarget;
    private Vector3 doorStartPos;
    private Vector3 rightStartPos;
    private Vector3 leftStartPos;

    private void Start()
    {
        doorStartPos = doorHolder.transform.position;
        rightStartPos = rightSideDoor.transform.localPosition;
        leftStartPos = leftSideDoor.transform.localPosition;
    }

    private void Update()
    {
        if(openDoor)
        {
            StartCoroutine(OpenDoors());
        }
        else if(stayOpen)
        {

        }
        else if(closeDoor)
        {
            StartCoroutine(CloseDoors());  
        }
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
        doorHolder.transform.localPosition = Vector3.MoveTowards(doorHolder.transform.localPosition, targetPosition, doorSpeed * Time.deltaTime);

        yield return new WaitUntil(() => doorHolder.transform.position == targetPosition);


        leftSideDoor.transform.localPosition = Vector3.MoveTowards(leftSideDoor.transform.localPosition, leftDoorTarget, doorSpeed * Time.deltaTime);
        rightSideDoor.transform.localPosition = Vector3.MoveTowards(rightSideDoor.transform.localPosition, rightDoorTarget, doorSpeed * Time.deltaTime);
    }

    IEnumerator CloseDoors()
    {
        leftSideDoor.transform.localPosition = Vector3.MoveTowards(leftSideDoor.transform.localPosition, leftStartPos, doorSpeed * Time.deltaTime);
        rightSideDoor.transform.localPosition = Vector3.MoveTowards(rightSideDoor.transform.localPosition, rightStartPos, doorSpeed * Time.deltaTime);

        yield return new WaitUntil(() => leftSideDoor.transform.localPosition == leftStartPos && rightSideDoor.transform.localPosition == rightStartPos);

        doorHolder.transform.position = Vector3.MoveTowards(doorHolder.transform.localPosition, doorStartPos, doorSpeed * Time.deltaTime);
        yield return new WaitUntil(() => doorHolder.transform.position == doorStartPos);
    }
}
