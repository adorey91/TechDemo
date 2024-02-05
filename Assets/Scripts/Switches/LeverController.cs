using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    public GameObject lever;
    public bool leverRotated;

    public Quaternion leverRotation;

    public GameObject interactText;

    public void Start()
    {
        leverRotation = lever.transform.rotation;
    }

    public void MoveLever(float targetAngle)
    {
        float rotationDuration = 5.0f;
        float elapsedTime = 0f;

        Vector3 startRotation = lever.transform.rotation.eulerAngles;
        Vector3 targetRotation = startRotation + new Vector3(targetAngle, 0, 0);

        while (elapsedTime < rotationDuration)
        {
            if (!leverRotated)
            {
                lever.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRotation, targetRotation, elapsedTime / rotationDuration));
                elapsedTime += Time.deltaTime;
            }
        }
        lever.transform.rotation = Quaternion.Euler(targetRotation);

        leverRotated = true;
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            interactText.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            interactText.SetActive(false);
    }
}