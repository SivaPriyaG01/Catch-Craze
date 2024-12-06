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
    public int score=0;
    public int lives=3;
    public bool gameOver=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
    public void RestartGame()
    {
        //SceneManager.LoadScene(scene)
    }
    public void ExitGame()
    {
        ExitGame();
    }
}
