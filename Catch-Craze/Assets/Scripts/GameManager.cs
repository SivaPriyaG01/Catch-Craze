using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public int score=0;
    public int lives=3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }
}
