using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public BallControl ball;
    public Text scoreText;
    void Update()
    {
        scoreText.text = ball.score.ToString();
    }
}
