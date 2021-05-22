using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public CoreGame game;
    public Text scoreText;
    void Update()
    {
        scoreText.text = game.score.ToString();
    }
}
