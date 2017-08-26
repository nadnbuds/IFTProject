using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MindTAPP.Unity.IFT;

public class PersistIFTData : MonoBehaviour
{
    // Ensures single instance is created, but not singleton.
    private static bool isCreated;

    // Holds references to scriptable objects to ensure objects are not destroyed.
    [SerializeField] private IPhotoService photoService;
    [SerializeField] private IWordlistService wordlist;

	private void Awake()
    {
		if (isCreated)
        {
            Destroy(this);
        }
        else
        {
            isCreated = true;
            DontDestroyOnLoad(this);
        }
	}
}