using UnityEngine;
using UnityEngine.SceneManagement;

public class gameplayManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOver()
    {
        Debug.Log("Game Over");
    }

    public void restartGame()
    {
        SceneManager.LoadScene("level_scene");
    }
}
