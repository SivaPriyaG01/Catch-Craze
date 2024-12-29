using Firebase;
using UnityEngine;

public class FirebaseInitialiser : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            Debug.Log("Firebase is ready!");
        });
    }
}
