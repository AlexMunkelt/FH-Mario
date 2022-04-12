using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Playertype playertype;
    public float speed = 1f;
    public float acceleration = 1f;
    public float jumpStrength = 1f;
    public LayerMask layerMask;

    public enum Playertype { Player1, Player2 };
    private enum State { Idle, Running, Jumping};

    private State state = State.Idle;
    private Rigidbody rb;
    private bool isGrounded;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.A))
        //{
        //    rb.AddForce(Vector2.left * acceleration * 100 * Time.deltaTime, ForceMode.Acceleration);
        //} else if (Input.GetKey(KeyCode.D))
        //{
        //    rb.AddForce(Vector2.right * acceleration * 100 * Time.deltaTime, ForceMode.Acceleration);
        //}

        float hor;

        if (playertype == Playertype.Player1)
        {
            hor = Input.GetAxis("Player1");
        } else
        {
            hor = Input.GetAxis("Player2");
        }

        Vector2 move = rb.velocity;
        move.x = hor * speed;

        if (Input.GetKey(KeyCode.W) && isGrounded && playertype == Playertype.Player1)
        {
            move.y = jumpStrength;
        }

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded && playertype == Playertype.Player2)
        {
            move.y = jumpStrength;
        }

        rb.velocity = move;

        //if (Input.GetKey(KeyCode.W) && isGrounded)
        //{
        //    rb.AddForce(Vector2.up * jumpStrength * 10 * Time.deltaTime, ForceMode.VelocityChange);
        //}

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1f, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            isGrounded = true;
            Debug.Log("Grounded");
        } else
        {
            isGrounded = false;
        }

        //if (rb.velocity.x > speed)
        //{
        //    rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        //}
        //else if (rb.velocity.x < -speed)
        //{
        //    rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        //}
    }
}
