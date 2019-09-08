using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private DistanceJoint2D joint;
    public LineRenderer lineRenderer;
    public LayerMask layerMask;
    public float maxDistance = 10f;


    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var target = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(transform.position, target, maxDistance, layerMask);
            if (hit.collider != null)
            {
                joint.enabled = true;
                joint.connectedAnchor = hit.point;
                joint.distance = Vector2.Distance(transform.position, joint.connectedAnchor);

                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, joint.connectedAnchor);

                Debug.Log(hit.collider);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            lineRenderer.enabled = false;
        }



    }
}
