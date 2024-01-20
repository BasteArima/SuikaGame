using System;
using Ads.Interfaces;
using Ads.Types;
using UnityEngine;

namespace Ads.Behaviors
{
    public class RewardAdController : MonoBehaviour
    {
        public event Action EarnedReward;
        public event Action AdClosed;
        public event Action AdHidden;

        private IRewardAd _rewardAd;

        private void Start()
        {
#if UNITY_EDITOR
            _rewardAd = new EditorRewardAd();
#else
            _rewardAd = new EditorRewardAd();
#endif

            _rewardAd.EarnedReward += () => EarnedReward?.Invoke();
            _rewardAd.AdClosed += () => AdClosed?.Invoke();
            _rewardAd.AdHidden += () => AdHidden?.Invoke();
            _rewardAd.InitializeSdk();
            _rewardAd.LoadAds();
        }

        private void OnDestroy()
        {
            _rewardAd.EarnedReward -= () => EarnedReward?.Invoke();
            _rewardAd.AdClosed -= () => AdClosed?.Invoke();
            _rewardAd.AdHidden -= () => AdHidden?.Invoke();
        }

        public bool ShowAd(string adType = "")
        {
            return _rewardAd.ShowAd(adType);
        }
    }
}