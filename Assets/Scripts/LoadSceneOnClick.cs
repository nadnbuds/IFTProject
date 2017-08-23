using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public Texture2D fadeOutTexture;
    public float fadeSpeed = 0.1f;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int Direction)
    {
        fadeDir = Direction;
        return fadeSpeed;
    }

    private void OnLevelWasLoaded(int level)
    {
        BeginFade(-1);
    }

    public void loadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    //Also load by name, (Better while building the game)
    public void loadByName(string name)
    {
        StartCoroutine(FadeScene(name));
    }
    IEnumerator FadeScene(string name)
    {
        yield return new WaitForSeconds(BeginFade(1));
        SceneManager.LoadScene(name);
    }
}

