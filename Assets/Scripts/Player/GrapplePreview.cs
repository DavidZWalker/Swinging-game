using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePreview : MonoBehaviour
{
    public GameObject player;
    public float distance;
    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        line.SetPosition(0, player.transform.position);
        line.SetPosition(1, mousePos);
        distance = Vector2.Distance(player.transform.position, mousePos);
    }
}
