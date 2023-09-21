namespace Test;

using Domain.Dto;
using Infrastructure.Provider.Checkout;
using Infrastructure.Provider.Promotions;
using System.Collections.Generic;
using Xunit;

public class CheckOutTests
{
    private readonly List<PricingRule> priceRules = new()
    {
        new PricingRule { Id = "A", Price = 50 },
        new PricingRule { Id = "B", Price = 30 },
        new PricingRule { Id = "C", Price = 20 },
        new PricingRule { Id = "D", Price = 15 },
        new PricingRule { Id = "E", Price = 100 }
    };

    private readonly List<IPromotion> activePromotions = new()
    {
        new PromotionSpecialPrice("A", 3, 130),
        new PromotionSpecialPrice("B", 2, 45),
        new PromotionFixPercentageDiscount("E", 5)
    };

    private decimal Price(string goods)
    {
        ICheckoutProvider co = new CheckoutProvider();

        co.New(priceRules, activePromotions);

        goods.ToCharArray().ToList().ForEach(item => co.Scan(item.ToString()));
        return co.Total;
    }

    [Fact]
    public void TestTotalWhenNoItems()
    {
        Assert.Equal(0, Price(""));
    }

    [Fact]
    public void TestTotalAOnly()
    {
        Assert.Equal( 50, Price("A"));
        Assert.Equal(100, Price("AA"));
        Assert.Equal(130, Price("AAA"));
        Assert.Equal(180, Price("AAAA"));
        Assert.Equal(230, Price("AAAAA"));
        Assert.Equal(260, Price("AAAAAA"));
    }

    [Fact]
    public void TestTotalTwoDifferentItems()
    {
        Assert.Equal(80, Price("AB"));
    }

    [Fact]
    public void TestTotalFourDifferentItems()
    {
        Assert.Equal(115, Price("CDBA"));
    }

    [Fact]
    public void TestTotalOneSpacialPriceOneItem()
    {
        Assert.Equal(160, Price("AAAB")); 
    }

    [Fact]
    public void TestTotalTwoSpacialPrices()
    {
        Assert.Equal(175, Price("AAABB"));
    }

    [Fact]
    public void TestTotalTwoSpacialPriceSOneItem()
    {
        Assert.Equal(190, Price("AAABBD"));
    }

    [Fact]
    public void TestTotalTwoSpacialPriceSOneItemOutOfOrder()
    {
        Assert.Equal(190, Price("DABABA"));
    }

    [Fact]
    public void TestTotalIncremental()
    { 
        ICheckoutProvider co = new CheckoutProvider();
        co.New(priceRules, activePromotions);

        co.Scan("A"); 
        Assert.Equal(50, co.Total);
        
        co.Scan("B"); 
        Assert.Equal(80, co.Total);
        
        co.Scan("A"); 
        Assert.Equal(130, co.Total);
        
        co.Scan("A"); 
        Assert.Equal(160, co.Total);
        
        co.Scan("B"); 
        Assert.Equal(175, co.Total);
    }

    [Fact]
    public void TestTotalFixPercentageDiscount()
    {
        ICheckoutProvider co = new CheckoutProvider();
        co.New(priceRules, activePromotions);

        co.Scan("E");
        Assert.Equal(95, co.Total);

        co.Scan("E");
        Assert.Equal(190, co.Total);
    }
}