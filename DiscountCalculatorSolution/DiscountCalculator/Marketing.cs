﻿using System;

namespace DiscountCalculator
{
    public interface IMarketingCampaign
    {
        public bool IsActive() 
        {
            return true;
        }
        public bool IsCrazySalesDay()
        {
            return true;
        }
    }

    public class MarketingCampaign : IMarketingCampaign
    {
        public bool IsActive()
        {
            return (long) DateTime.Now.TimeOfDay.TotalMilliseconds % 2 == 0;
        }

        public bool IsCrazySalesDay()
        {
            return DateTime.Now.DayOfWeek.Equals(DayOfWeek.Friday);
        }
    }
}
