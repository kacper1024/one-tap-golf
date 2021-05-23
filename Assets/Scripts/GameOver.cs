using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public CoreGame game;
    public Score score;
    public Button Restart;
    public Text GameOverText;
    public Text GameOverScore;
    public Text GameOverBest;
    public bool isGameOver = false;

    public void GameOverScreenAppear()
    {
        isGameOver = true;
        GameOverScore.text += score.scoreText.text;
        GameOverBest.text += game.bestScore.ToString();
        Restart.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(true);
        GameOverScore.gameObject.SetActive(true);
        GameOverBest.gameObject.SetActive(true);
    }

    public void GameOverScreenDisappear()
    {
        isGameOver = false;
        GameOverScore.text = "Score: ";
        GameOverBest.text = "Best: ";
        Restart.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        GameOverScore.gameObject.SetActive(false);
        GameOverBest.gameObject.SetActive(false);
        game.ResetGame();
    }
}
