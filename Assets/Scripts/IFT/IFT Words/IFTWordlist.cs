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
    [CreateAssetMenu()]
    public class IFTWordlist : IWordlistService
    {
        [SerializeField] private IWordlistRepository IFTRepo;
        private IEnumerable<string> IFTWords;

        private void OnEnable()
        {
            this.hideFlags = HideFlags.HideAndDontSave;
            IFTWords = IFTRepo.QueryWords();
        }

        public override IEnumerable<string> GetWords()
        {
            return IFTWords.ToArray();
        }
    }
}