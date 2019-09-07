using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool _isGrounded;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;

    public float movementSpeed = 2f;
    public float jumpForce = 10000f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // handle left-right movement
        var pos = _rigidBody.position;
        var h = Input.GetAxisRaw("Horizontal");

        pos.x += h * movementSpeed * Time.deltaTime;
        _rigidBody.MovePosition(pos);

        // handle jumping
        var space = Input.GetKey(KeyCode.Space);
        if (_isGrounded && space)
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // check if player is grounded
        var bottomCenter = new Vector2(_collider.bounds.center.x,
            _collider.bounds.center.y - _collider.bounds.extents.y);
        _isGrounded = Physics.Raycast(bottomCenter, Vector2.down, 0.1f, 8);
        Debug.Log("IsGrounded = " + _isGrounded);
    }

    void FixedUpdate()
    {
    }
}
