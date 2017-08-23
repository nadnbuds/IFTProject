/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System.Collections.Generic;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    // Business Logic Layer
    // Proccesses data from IWordlistRepository (Data Access Layer)
    public abstract class IWordlistService : MonoBehaviour
    {
        public abstract IEnumerable<string> GetWords();
    }
}