using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Score score;
    public Button Restart;
    public Text GameOverText;
    public Text GameOverScore;
    public Text GameOverBest;
    public bool isGameOver = false;

    public void GameOverScreenDisappear()
    {
        isGameOver = false;
        Restart.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        GameOverScore.gameObject.SetActive(false);
        GameOverBest.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOverScreenAppear()
    {
        GameOverScore.text += score.scoreText.text;
        isGameOver = true;
        Restart.gameObject.SetActive(true);
        GameOverText.gameObject.SetActive(true);
        GameOverScore.gameObject.SetActive(true);
        GameOverBest.gameObject.SetActive(true);
    }
}
