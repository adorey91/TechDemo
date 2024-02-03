using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DynamiteController : MonoBehaviour
{
    public GameObject weapon;
    public GameObject weaponStart;
    
    public GameObject explosionEffect;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    private bool hasExploded = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded && collision.gameObject.CompareTag("Range"))
        {
            Explode();
        }
    }

    void Explode()
    {
        hasExploded = true;

        // Instantiate explosion effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Apply explosion force to nearby rigidbodies
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in colliders)
        {
            Rigidbody rb = hitCollider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // Destroy the dynamite GameObject
        Destroy(gameObject);

        Invoke("ResetDynamite", 0.1f);
    }

    void ResetDynamite()
    {
        Vector3 weaponStartPosition = weaponStart.transform.position;

        if (hasExploded)
            Instantiate(weapon, weaponStartPosition, Quaternion.identity);
    }
}
