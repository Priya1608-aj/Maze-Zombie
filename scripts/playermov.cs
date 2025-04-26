using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private float horizontal_Input;
    private bool isWalk;
    public float speed = 5.0f, turn_Speed = 200.0f;

    private Rigidbody rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        horizontal_Input = Input.GetAxis("Horizontal");

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement = transform.forward * speed * Time.fixedDeltaTime;
            animator.SetBool("Start_walking", true);
        }
        else
        {
            animator.SetBool("Start_walking", false);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            movement = transform.forward * (speed * 1.5f) * Time.fixedDeltaTime;
            animator.SetBool("Start_running", true);
        }
        else
        {
            animator.SetBool("Start_running", false);
        }

        rb.MovePosition(rb.position + movement);

        Quaternion turn = Quaternion.Euler(0f, horizontal_Input * turn_Speed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
    }

}


