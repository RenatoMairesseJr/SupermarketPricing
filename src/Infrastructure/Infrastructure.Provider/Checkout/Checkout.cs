using Domain.Dto;
using Infrastructure.Provider.Promotions;

namespace Infrastructure.Provider.Checkout;

public class CheckoutProvider : ICheckoutProvider
{
    private readonly List<PricingRule> PricingRules;
    private readonly List<string> ScannedItems;
    private readonly List<IPromotion> ActivePromotions;

    /// <summary>
    /// Contructor; initialize Price rules and active promotions
    /// </summary>
    public CheckoutProvider()
    {
        PricingRules = new List<PricingRule>();
        ScannedItems = new List<string>();
        ActivePromotions = new List<IPromotion>();
    }

    /// <summary>
    /// Add item to scanned item list
    /// </summary>
    /// <param name="item"></param>
    public void Scan(string item)
    {
        ScannedItems.Add(item);
    }

    /// <summary>
    /// Update pricing rules and active promotions
    /// </summary>
    /// <param name="pricingRule"></param>
    /// <param name="activePromotion"></param>
    public void New(List<PricingRule> pricingRule, List<IPromotion> activePromotion)
    {
        PricingRules.AddRange(pricingRule);
        ActivePromotions.AddRange(activePromotion);
    }

    /// <summary>
    /// calculate total price of the purchase
    /// </summary>
    /// <returns></returns>
    public decimal Total
    {
        get
        {
            decimal total = 0;

            foreach (var rule in PricingRules)
            {
                var itemCount = ScannedItems.Count(item => item == rule.Id);

                if (itemCount > 0)
                {
                    decimal promotionPrice = 0;

                    var promotion = ActivePromotions.FirstOrDefault(a => a.ItemId == rule.Id);

                    if (promotion != null)
                    {
                        promotionPrice = promotion.ApplyPromotion(rule.Id, itemCount, rule);
                        total += promotionPrice;
                    }
                    else
                        total += itemCount * rule.Price;
                }
            }

            return total;
        }
    }
}
