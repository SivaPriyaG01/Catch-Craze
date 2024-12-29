using UnityEngine;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    private DatabaseReference mDatabaseRef;
    private GameManager gameManager;
    private LoginManager loginManager;
    private string userEmail;

    void Start()
    {
        // Get references to GameManager and LoginManager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        loginManager = GameObject.Find("LoginManager").GetComponent<LoginManager>();

        // Initialize Firebase Database
        mDatabaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        Debug.Log("ScoreManager initialized.");
    }

    // Call this function to update the user's score in the database
    public void UpdateScoreInDatabase()
    {
        // Get the user's email from LoginManager
        if (loginManager == null)
        {
            Debug.LogError("LoginManager reference is missing.");
            return;
        }

        userEmail = loginManager.emailInputField.text; // Assuming emailInputField is public in LoginManager
        if (string.IsNullOrEmpty(userEmail))
        {
            Debug.LogError("User email is empty. Ensure the user is logged in.");
            return;
        }

        // Get the user's current score from GameManager
        if (gameManager == null)
        {
            Debug.LogError("GameManager reference is missing.");
            return;
        }

        int currentScore = gameManager.score;

        // Convert email to a Firebase-friendly key
        string sanitizedEmail = userEmail.Replace(".", "_").Replace("@", "_");

        // Create a unique key for the score entry
        string key = mDatabaseRef.Child("scores").Push().Key;

        // Create the score entry
        LeaderboardEntry entry = new LeaderboardEntry(sanitizedEmail, currentScore);
        Dictionary<string, object> entryValues = entry.ToDictionary();

        // Save the score to the database
        Dictionary<string, object> updates = new Dictionary<string, object>
        {
            { $"/scores/{key}", entryValues }, // Global leaderboard
            { $"/user-scores/{sanitizedEmail}/{key}", entryValues } // User-specific leaderboard
        };

        mDatabaseRef.UpdateChildrenAsync(updates).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Score updated in the database successfully.");
            }
            else
            {
                Debug.LogError("Failed to update score in the database: " + task.Exception);
            }
        });
    }
}

// LeaderboardEntry Class to structure the score data
public class LeaderboardEntry
{
    public string uid; // User's email or ID
    public int score;  // User's score

    public LeaderboardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, object> ToDictionary()
    {
        return new Dictionary<string, object>
        {
            { "uid", uid },
            { "score", score }
        };
    }
}
