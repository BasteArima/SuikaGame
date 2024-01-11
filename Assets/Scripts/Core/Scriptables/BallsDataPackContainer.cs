using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scriptables
{
    [CreateAssetMenu(menuName = "Data/BallsDataPackContainer")]
    public class BallsDataPackContainer : SerializedScriptableObject
    {
        [SerializeField] private BallsDataPack[] _ballsDataPacks;

        public BallsDataPack[] BallsDataPacks => _ballsDataPacks;

        public void ActivePackWithId(string id)
        {
            var pack = _ballsDataPacks.FirstOrDefault(pack => pack.Id == id);
            if (null == pack)
                return;
            pack.ActivePack = true;
        }

        public BallsDataPack GetActivePack()
        {
            var pack = _ballsDataPacks.FirstOrDefault(pack => pack.ActivePack);
            return pack;
        }
    }
}