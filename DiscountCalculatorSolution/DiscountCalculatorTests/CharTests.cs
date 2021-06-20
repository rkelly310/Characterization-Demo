using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DiscountCalculator;
using Moq;
using Pose;

namespace DiscountCalculatorTests
{
    [TestClass]
    public class DiscountTest
    {
        [TestMethod]
        public void TestValueGreaterThanOneThousandThenDiscountIsTenPercent()
        {
            var discount = new Discount();
            var net = new Money(1002);
            var total = discount.DiscountFor(net);

            Assert.AreEqual(new Money(901.8m), total);
        }
        [TestMethod]
        public void TestValueLessThanOneThousandOnCrazySalesDayDiscountIsFifteenPercent()
        {
            var stubcampaign = new Mock<IMarketingCampaign>();
            stubcampaign.Setup(stubcampaign => stubcampaign.IsCrazySalesDay()).Returns(true);
            var discount = new Discount(stubcampaign.Object);
            Money net = new Money(740);;
            Money total = discount.DiscountFor(net);

            Assert.AreEqual(new Money(629), total);
        }
    }
}
