using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrapplingHook : MonoBehaviour
{
    private CircleCollider2D circleCollider;
    public Player player;
    private Vector3 hookDirection = Vector3.zero;
    private LineRenderer line;

    public bool isReturning;
    public bool isActive;
    public bool isHooked;
    public float grappleSpeed = 1f;
    public float grappleReturnSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        isActive = true;
        line.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && hookDirection == Vector3.zero)
        {
            hookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isReturning = false;
        }

        if (Input.GetMouseButton(0) && !isReturning && isActive)
            transform.position = Vector2.MoveTowards(transform.position, hookDirection, grappleSpeed);

        if (Input.GetMouseButtonUp(0))
            isReturning = true;

        if (isReturning)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, grappleReturnSpeed);

        if (isActive)
        {
            line.enabled = true;
            line.SetPosition(0, player.transform.position);
            line.SetPosition(1, transform.position);
        }

        if (Vector2.Distance(transform.position, player.transform.position) < 0.5f && isReturning)
        {
            line.enabled = false;
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider is TilemapCollider2D)
        {
            isHooked = true;
            Debug.Log("Hooked!");
        }
    }
}
