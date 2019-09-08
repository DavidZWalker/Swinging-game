﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    public bool _isGrounded;
    public float movementSpeed = 2f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is grounded
        _isGrounded = Physics2D.OverlapArea(new Vector2(transform.position.x - 0.64f, transform.position.y - 0.64f),
            new Vector2(transform.position.x + 0.64f, transform.position.y - 0.64f), 
            groundLayer);

        // handle left-right movement
        var h = Input.GetAxisRaw("Horizontal");
        if (h != 0 || _rigidBody.velocity.x != 0)
        {
            var moveSpeed = h * movementSpeed;
            _rigidBody.velocity = new Vector2(moveSpeed, _rigidBody.velocity.y);
        }

        // handle jumping
        var space = Input.GetKey(KeyCode.Space);
        if (_isGrounded && space)
        {
            _isGrounded = false;
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
    }
}