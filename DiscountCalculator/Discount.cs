using DiscountCalculator;

namespace DiscountCalculator
{
    public class Discount
    {
        private readonly MarketingCampaign marketingCampaign;

        private readonly IMarketingCampaign marketingCampaign1;

        public Discount()
        {
            this.marketingCampaign = new MarketingCampaign();
        }

        public Discount(IMarketingCampaign marketingCampaign1)
        {
            this.marketingCampaign1 = marketingCampaign1;
        }

        public Money DiscountFor(Money netPrice)
        {
            if (marketingCampaign1.IsCrazySalesDay())
            {
                return netPrice.ReduceBy(15);
            }
            if (netPrice.MoreThan(Money.OneThousand))
            {
                return netPrice.ReduceBy(10);
            }
            if (netPrice.MoreThan(Money.OneHundred) && marketingCampaign1.IsActive())
            {
                return netPrice.ReduceBy(5);
            }
            return netPrice;
        }
    }
}
