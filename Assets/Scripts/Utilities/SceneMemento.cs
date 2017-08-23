using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMemento : MonoBehaviour
{
    public static SceneMemento Instance;
    private string previousScene;
    private string currentScene;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        previousScene = string.Empty;
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void ChangeToPreviousScene()
    {
        SceneManager.LoadScene(previousScene);
    }

    private void OnSceneLoaded()
    {
        previousScene = currentScene;
        currentScene = SceneManager.GetActiveScene().name;
    }
}