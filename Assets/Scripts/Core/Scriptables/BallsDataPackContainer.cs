using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scriptables
{
    [CreateAssetMenu(menuName = "Data/BallsDataPackContainer")]
    public class BallsDataPackContainer : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<BallsDataPack, bool> _ballsDataPacks;

        public Dictionary<BallsDataPack, bool> BallsDataPacks => _ballsDataPacks;

        public void ActivePackWithId(string id)
        {
            foreach (var pack in _ballsDataPacks.Keys)
                _ballsDataPacks[pack] = false;
            
            var activePack = _ballsDataPacks.FirstOrDefault(x => x.Key.Id == id).Key;
            _ballsDataPacks[activePack] = true;
        }

        public BallsDataPack GetActivePack()
        {
            var pack = _ballsDataPacks.FirstOrDefault(pack => pack.Value);
            return pack.Key;
        }
    }
}