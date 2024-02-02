using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject projectile;


    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (player.hasItem)
            {
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                projectile.GetComponent<Rigidbody>().isKinematic = false;
                projectile.GetComponent<BoxCollider>().enabled = true;
                projectileRb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                projectileRb.AddForce(transform.up * 8f, ForceMode.Impulse);

                projectile.transform.parent = null;
            }
        }
    }
}