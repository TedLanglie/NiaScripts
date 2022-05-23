using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Move Stats")]
    [SerializeField] public float _moveSpeed;
    [SerializeField] private float _dampSpeed = .2f;

    public Rigidbody2D rb;
    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    void FixedUpdate()
    {
        move();
    }

    void ProcessInputs()
    {
    }

    void move()
    {
        // horiz movement
        float fHorizontalVelocity = rb.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal") * _moveSpeed;
        fHorizontalVelocity *= Mathf.Pow(1f - _dampSpeed, Time.deltaTime * 10f);
        // vert movement
        float fVerticalVelocity = rb.velocity.y;
        fVerticalVelocity += Input.GetAxisRaw("Vertical") * _moveSpeed;
        fVerticalVelocity *= Mathf.Pow(1f - _dampSpeed, Time.deltaTime * 10f);

        // move char
        rb.velocity = new Vector2(fHorizontalVelocity, fVerticalVelocity);
    }
}
