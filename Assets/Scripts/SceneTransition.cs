using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
