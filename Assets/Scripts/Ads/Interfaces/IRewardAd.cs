using System;

namespace Ads.Interfaces
{
    public interface IRewardAd
    {
        public event Action EarnedReward;
        public event Action AdClosed;
        public event Action AdHidden;
        void InitializeSdk();
        void LoadAds();
        bool ShowAd(string adType);
    }
}