using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public CoreGame game;
    public Text scoreText;
    public Text highScore;
    void Update()
    {
        scoreText.text = game.score.ToString();
    }
}
