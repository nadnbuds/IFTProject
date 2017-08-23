/*
 * Author(s): Joshua Beto
 * Company: MindTAPP
 */

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// TODO: update for online information once website goes up
namespace MindTAPP.Unity.IFT
{
    public class IFTWordlist : IWordlistService
    {
        [SerializeField] IWordlistRepository IFTRepo;
        private IEnumerable<string> IFTWords;

        private void Awake()
        {
            IFTWords = IFTRepo.QueryWords();
        }

        public override IEnumerable<string> GetWords()
        {
            return IFTWords.ToArray();
        }
    }
}