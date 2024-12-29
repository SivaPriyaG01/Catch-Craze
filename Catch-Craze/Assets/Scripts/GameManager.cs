using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject loginRegisterPanel; // Panel for login/register
    public GameObject startGamePanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public TextMeshProUGUI scoreDisplay;
    public ScoreManager scoreManager;

    public int score = 0;
    public int lives = 3;
    public bool gameOver = false;
    public bool gamePaused = false;
    public bool gameStart = false;
    public bool isLoggedIn = false; // Check if the user is logged in

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        // Show login panel if the user is not logged in
        if (!isLoggedIn)
        {
            loginRegisterPanel.SetActive(true);
            startGamePanel.SetActive(false);
        }
        else
        {
            loginRegisterPanel.SetActive(false);
            startGamePanel.SetActive(true);
        }

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void OnLoginSuccess()
    {
        // Called when login is successful
        isLoggedIn = true;
        loginRegisterPanel.SetActive(false);
        startGamePanel.SetActive(true);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateLives(int livesToSubtract)
    {
        lives -= livesToSubtract;
        livesText.text = "Lives: " + lives.ToString();
        if (lives < 1)
        {
            gameOver = true;
            scoreManager.UpdateScoreInDatabase();
            gameOverPanel.SetActive(true);
            scoreDisplay.text = "Your Score: "+ score;

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
        Application.Quit(); // Close the game
    }

    public void PauseGame()
    {
        gamePaused = true;
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        gamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        gameStart = true;
        startGamePanel.SetActive(false);
    }
    
}
