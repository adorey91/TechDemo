using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 1f;

    bool playerOn;

    void Update()
    {
        if(playerOn)
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
            {
                currentWaypointIndex++;

                if(currentWaypointIndex >= waypoints.Length)
                    currentWaypointIndex = 0;
            }
        }


        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOn = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOn = false;
    }
}
