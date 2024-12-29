// using Firebase.Auth;
// using UnityEngine;
// using UnityEngine.UI;

// public class LoginManager : MonoBehaviour
// {
//     public InputField emailField;
//     public InputField passwordField;
//     public Text statusText;

//     private FirebaseAuth auth;

//     void Start()
//     {
//         auth = FirebaseAuth.DefaultInstance;
//     }

//     public void Login()
//     {
//         string email = emailField.text;
//         string password = passwordField.text;

//         auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
//         {
//             if (task.IsCompletedSuccessfully)
//             {
//                 Debug.Log("Login successful!");
//                 statusText.text = "Login Successful!";
//             }
//             else
//             {
//                 Debug.LogError(task.Exception);
//                 statusText.text = "Login Failed: " + task.Exception?.Message;
//             }
//         });
//     }

//     public void Register()
//     {
//         string email = emailField.text;
//         string password = passwordField.text;

//         auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
//         {
//             if (task.IsCompletedSuccessfully)
//             {
//                 Debug.Log("Registration successful!");
//                 statusText.text = "Registration Successful!";
//             }
//             else
//             {
//                 Debug.LogError(task.Exception);
//                 statusText.text = "Registration Failed: " + task.Exception?.Message;
//             }
//         });
//     }
// }

using UnityEngine;
using TMPro;
using Firebase.Auth;
using Firebase;
using Firebase.Extensions; // For async handling of dependencies


public class LoginManager : MonoBehaviour
{
    public TMP_InputField emailInputField;
    public TMP_InputField passwordInputField;
    public TextMeshProUGUI messageText; // To show error/success messages
    private GameManager gameManager;
    private FirebaseAuth auth;

    void Start()
{
        gameManager= GameObject.Find("GameManager").GetComponent<GameManager>();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            if (task.Result == DependencyStatus.Available)
                {
                    // Initialize Firebase
                    FirebaseApp app = FirebaseApp.DefaultInstance;

                    // Set the Database URL
                    app.Options.DatabaseUrl = new System.Uri("https://catch-craze-default-rtdb.asia-southeast1.firebasedatabase.app");

                    // Initialize FirebaseAuth
                    auth = FirebaseAuth.DefaultInstance;

                    Debug.Log("Firebase initialized successfully.");
                }
            else
            {
            Debug.LogError("Could not resolve all Firebase dependencies: " + task.Result);
            }
        });
}


    public void OnLoginButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            messageText.text = "Please fill in all fields.";
            return;
        }

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        if (task.IsCanceled) 
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            return;
        }
        if (task.IsFaulted) 
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }

        Firebase.Auth.AuthResult result = task.Result;
        Debug.LogFormat("User signed in successfully: {0} ({1})",
        result.User.DisplayName, result.User.UserId);
        });

        gameManager.OnLoginSuccess();
    }

    public void OnRegisterButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            messageText.text = "Please fill in all fields.";
            return;
        }
        
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
        if (task.IsCanceled) 
        {
        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
        return;
        }
        
        if (task.IsFaulted) 
        {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }

        // Firebase user has been created.
        Firebase.Auth.AuthResult result = task.Result;
        Debug.LogFormat("Firebase user created successfully: {0} ({1})",
        result.User.DisplayName, result.User.UserId);
        });
    }

    
    
    
    
    
    
    
    private void SimulateLogin(string email, string password)
    {
        // Simulated login check
        if (email == "test@example.com" && password == "password123")
        {
            messageText.text = "Login successful!";
            if (gameManager != null)
            {
                gameManager.OnLoginSuccess(); // Notify the GameManager
            }
        }
        else
        {
            messageText.text = "Invalid email or password.";
        }
    }
}

