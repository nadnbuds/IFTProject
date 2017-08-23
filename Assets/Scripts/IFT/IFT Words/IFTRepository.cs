/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System.Collections.Generic;

using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    public class IFTRepository : IWordlistRepository
    {
        // Checks to see if there is only one instance created
        private static bool isInstanceCreated = false;

        public IFTRepository()
        {
            if (isInstanceCreated)
            {
                Debug.LogWarning("There should only be a single instance of IFTWordRepository");
                return;
            }
            isInstanceCreated = true;
        }

        public override IEnumerable<string> QueryWords()
        {
            string[] IFTWords =
            {
                "Goes Above and Beyond", "Hardworking", "Productive",
                "Excited", "Outgoing", "Happy", "Loyal", "Reliable",
                "Team play", "Industrious", "Enthusiasm", "Good Citizen",
                "Gregarious", "Thrilled", "Prompt", "Faithful", "Playful",
                "Conscientious", "Brave", "Creative", "Assertive", "Educated", "Organized",
                "Efficient"
            };
            return IFTWords;
        }
    }
}