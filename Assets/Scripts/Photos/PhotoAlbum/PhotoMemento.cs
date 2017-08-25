/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using UnityEngine;

namespace MindTAPP.Unity.Gallery
{
    public class PhotoMemento
    {
        public GameObject PhotoGameObject;
        public Sprite PhotoSprite { get; private set; }
        public string FileName { get; private set; }

        public PhotoMemento(GameObject photoGO, Sprite photo, string file)
        {
            this.PhotoGameObject = photoGO;
            this.PhotoSprite = photo;
            this.FileName = file;
        }
    }
}