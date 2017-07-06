using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour 
{
    
	public void loadByIndex(int sceneIndex)
	{
		SceneManager.LoadScene (sceneIndex);
	}
    //Also load by name, (Better while building the game)
    public void loadByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
