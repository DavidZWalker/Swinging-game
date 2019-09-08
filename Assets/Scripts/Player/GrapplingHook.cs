using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GrapplingHook : MonoBehaviour
{
    public Player player;
    private Vector3 hookDirection = Vector3.zero;
    private LineRenderer line;
    private float grappleTimeHelper = 0;
    private Rigidbody2D rigidBody;

    public bool isReturning;
    public bool isActive;
    public bool isHooked;
    public float grappleForce = 1f;
    public float grappleSpeed = 1f;
    public float grappleReturnSpeed = 10f;
    public float maxGrappleTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        line = GetComponent<LineRenderer>();
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

        grappleTimeHelper += Time.deltaTime;
        if (grappleTimeHelper > maxGrappleTime && !isHooked)
        {
            isReturning = true;
            grappleTimeHelper = 0;
        }

        if (Input.GetMouseButton(0) && !isReturning && isActive)
            transform.position = Vector2.MoveTowards(transform.position, hookDirection, grappleSpeed);

        if (Input.GetMouseButtonUp(0))
        {
            isHooked = false;
            isReturning = true;
            player.ReleaseHook();
        }

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

    void OnCollisionEnter2D(Collision2D col)
    {
        isHooked = true;
        player.JoinToHookedTargetAt(transform.position);
    }
}
