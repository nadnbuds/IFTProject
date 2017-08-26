/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Collections.Generic;
using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    // Data Access Layer
    // Handles access of IFT words or other word lists
    public abstract class IWordlistRepository : ScriptableObject
    {
        public abstract IEnumerable<string> QueryWords();
    }
}