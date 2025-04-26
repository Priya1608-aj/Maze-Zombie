using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 5f;

    private void Start()
    {
        // Destroy the bullet after a few seconds to avoid clutter
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            Destroy(collision.gameObject); // kill the zombie
        }

        // Destroy the bullet on any collision (wall, zombie, anything)
        Destroy(gameObject);
    }
}

