/*
* Author(s): Joshua Beto
* Company: MindTAPP
*/

using System.Collections.Generic;

using UnityEngine;

namespace MindTAPP.Unity.IFT
{
    [CreateAssetMenu()]
    public class IFTRepository : IWordlistRepository
    {
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