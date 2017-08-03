using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{

	// Use this for initialization
	public void PlaySound(string soundName)
    {
        AudioManager.Instance.Play(soundName);
    }
}
