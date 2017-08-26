using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class InitialPhoto : ScriptableObject
{
    public Sprite StartingPhoto { get; set; }

    private void OnEnable()
    {
        StartingPhoto = null;
    }
}