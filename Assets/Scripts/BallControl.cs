using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float power = 5f;

    HoleControl hole;
    
    Vector3 originalPos;

    Rigidbody2D rigidbody2d;

    LineRenderer linerenderer;

    Vector2 DragStartPos;
    
    new Transform transform;


    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        linerenderer = GetComponent<LineRenderer>();
        transform = GetComponent<Transform>();
        originalPos = gameObject.transform.position;
        hole = GameObject.Find("Hole").GetComponent<HoleControl>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DragStartPos = (Vector2)gameObject.transform.position;
        }

        if (Input.GetButton("Jump"))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;

            Vector2[] trajectory = Plot(rigidbody2d, (Vector2)transform.position, _velocity, 500);
            
            linerenderer.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for(int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }

            linerenderer.SetPositions(positions);
        }

        if (Input.GetButtonUp("Jump"))
        {
            linerenderer.positionCount = 0;
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;
            rigidbody2d.velocity = _velocity;
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Mouse position: " + Input.mousePosition);
        GUILayout.EndArea();
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for(int i = 0; i < steps; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Hole")
        {
            Debug.Log("Kolizja!");
            gameObject.transform.position = originalPos;
            rigidbody2d.Sleep();
            hole.RandomPosition();
        }
    }
}
