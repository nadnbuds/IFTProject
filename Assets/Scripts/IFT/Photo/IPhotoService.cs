/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System.Collections.Generic;

using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    public abstract class IPhotoService : ScriptableObject
    {
        public abstract IEnumerable<Sprite> GetPhotos();
        public abstract void AddPhoto(Sprite photo, string fileName);
        public abstract string DeletePhoto(Sprite photo);
    }
}