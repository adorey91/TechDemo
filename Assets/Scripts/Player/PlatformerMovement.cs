using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [SerializeField] private Transform rbody;
    [SerializeField] private bool isOnPlatform;
    [SerializeField] private Transform platformRb;
    [SerializeField] private Vector3 lastPlatformPosition;

    public void Awake()
    {
        rbody = GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if (isOnPlatform)
        {
            Vector3 deltaPosition = platformRb.position - lastPlatformPosition;
            rbody.position = rbody.position + deltaPosition;
            lastPlatformPosition = platformRb.position;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Platform"))
        {
            platformRb = collision.gameObject.GetComponent<Transform>();
            lastPlatformPosition = platformRb.position;
            isOnPlatform = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isOnPlatform = false;
            platformRb = null;
        }
    }
}
