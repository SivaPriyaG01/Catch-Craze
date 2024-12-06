using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject startGamePanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public int score=0;
    public int lives=3;
    public bool gameOver=false;
    public bool gamePaused=false;
    public bool gameStart=false;

    void Start()
    {
        if (!gameStart) // Only show startGamePanel if the game is not already started
        {
            startGamePanel.SetActive(true);
        }
        else
        {
            startGamePanel.SetActive(false);
        }
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void UpdateScore(int scoreToAdd)
    {
        score+=scoreToAdd;
        scoreText.text = "Score: "+ score.ToString();
    }
    public void UpdateLives(int livesToSubtract)
    {
        lives-=livesToSubtract;
        livesText.text = "Lives: "+ lives.ToString();
        if(lives<1)
        {
            gameOver=true;
            gameOverPanel.SetActive(true);
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameStart = true;  // Ensure game starts immediately on restart
        gameOver = false;  // Reset gameOver state
        Time.timeScale = 1; // Resume the game if paused
    }
    public void ExitGame()
    {
        ExitGame();
    }
    public void PauseGame()
    {
        gamePaused=true;
        pausePanel.SetActive(true);
        Time.timeScale=0;
    }
    public void ResumeGame()
    {
        gamePaused=false;
        pausePanel.SetActive(false);
        Time.timeScale=1;
    }
    public void GoHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        gameStart=true;
        startGamePanel.SetActive(false);
    }
}
