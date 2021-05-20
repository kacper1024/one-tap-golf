using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
