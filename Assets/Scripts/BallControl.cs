using UnityEngine;

public class BallControl : MonoBehaviour
{
    private float power = 5f;

    private float endPosX = -5.8f;
    private float endPosY = -2.3f;
    private float xPosPerSecond = 0.5f;
    
    public int score = 0;
    
    public bool timeStart = false;
    public float timeRemaining = 3.0f;

    public bool disableKey;

    Vector3 OriginalPos;
    Vector2 DragStartPos;

    Rigidbody2D rigidbody2d;
    LineRenderer lineRenderer;
    HoleControl hole;
    GameOver gameOverScreen;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        hole = GameObject.Find("Hole").GetComponent<HoleControl>();
        gameOverScreen = GameObject.Find("GameOverScreen").GetComponent<GameOver>();
        OriginalPos = gameObject.transform.position;
        disableKey = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && disableKey == false)
        {
            DragStartPos = (Vector2)gameObject.transform.position;
        }

        if (Input.GetButton("Jump") && disableKey == false)
        {
            SetParabola();
        }

        if (Input.GetButtonUp("Jump") && disableKey == false)
        {
            Fire();
        }

        Mishit();

        if(timeStart == true)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    private Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
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

    private void SetParabola()
    {
        Vector2 DragEndPos = new Vector2(endPosX += xPosPerSecond * Time.deltaTime, endPosY);
        if (endPosX > -3.6f)
        {
            Fire();
        }
        else
        {
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;

            Vector2[] trajectory = Plot(rigidbody2d, (Vector2)transform.position, _velocity, 500);

            lineRenderer.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = trajectory[i];
            }

            lineRenderer.SetPositions(positions);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Hole")
        {
            Debug.Log("Trafiony!");
            score += 1;
            ResetTime();
            NextRound();
        }
    }

    private void Mishit()
    {
        if ((gameObject.transform.position.y <= -3.4f && gameObject.transform.position.y >= -3.5f) &&
            (gameObject.transform.position.x >= -5.5f && gameObject.transform.position.x < hole.transform.position.x))
        {
            timeStart = true;
            if(timeRemaining < 0)
            {
                GameOver();
                ResetTime();
            }
        }
        if((gameObject.transform.position.y <= -3.4f && gameObject.transform.position.y >= -3.5f) &&
            (gameObject.transform.position.x > hole.transform.position.x && gameObject.transform.position.x <= 11f))
        {
            if(gameOverScreen.isGameOver==false)
                GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Koniec gry!");
        gameOverScreen.GameOverScreenAppear();
    }

    private void ResetTime()
    {
        timeStart = false;
        timeRemaining = 3.0f;
    }

    private void Fire()
    {
        disableKey = true;
        lineRenderer.positionCount = 0;
        Vector2 DragEndPos = new Vector2(endPosX += xPosPerSecond * Time.deltaTime, endPosY);
        Vector2 _velocity = (DragEndPos - DragStartPos) * power;
        rigidbody2d.velocity = _velocity;
    }
    private void NextRound()
    {
        xPosPerSecond += 0.1f;
        endPosX = -5.8f;
        disableKey = false;
        gameObject.transform.position = OriginalPos;
        rigidbody2d.Sleep();
        hole.bufferRand[1] = hole.bufferRand[0];
        hole.RandomPosition();
    }
}
