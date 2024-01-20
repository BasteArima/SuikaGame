using System;
using Ads.Interfaces;
using UnityEngine;

namespace Ads.Types
{
    public class EditorRewardAd : IRewardAd
    {
        public event Action EarnedReward;
        public event Action AdClosed;
        public event Action AdHidden;

        public void InitializeSdk()
        {
        }

        public void LoadAds()
        {
        }

        public bool ShowAd(string adType)
        {
            EarnedReward?.Invoke();
            AdClosed?.Invoke();
            Debug.Log($"[EditorRewardAd] Ad Watched in Editor");
            return true;
        }
    }
}