using UnityEngine;

public class OrientateScreenForScene : MonoBehaviour
{
    private ScreenOrientation baseScreenOrientation;
    [SerializeField] private ScreenOrientation activeScreenOrientation;
    
    private void Awake ()
    {
        baseScreenOrientation = Screen.orientation;
	}

    private void Start()
    {
        Screen.orientation = activeScreenOrientation;
    }
    
    void OnDisable () {
        Screen.orientation = baseScreenOrientation;
	}
}
