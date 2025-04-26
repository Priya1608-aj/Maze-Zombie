using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;

    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            if (distance > attackRange)
            {
                // Move toward player
                MoveTowardsPlayer();
                animator.SetBool("IsWalk", true);
                animator.ResetTrigger("Attack");
            }
            else
            {
                // Attack player
                animator.SetBool("IsWalk", false);
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            // Idle
            animator.SetBool("IsWalk", false);
            animator.ResetTrigger("Attack");
        }
    }

    void MoveTowardsPlayer()
    {
        // Smoothly rotate towards player
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // Move forward
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Bullet"))
    {
        Destroy(gameObject); // Instantly destroy the zombie
        Destroy(other.gameObject); // Optionally destroy the bullet too
    }
}

}

