using Domain.Dto;

namespace Infrastructure.Provider.Promotions;

public class PromotionFixPercentageDiscount : IPromotion
{
    public string ItemId { get; }

    private readonly decimal DiscountPercentage;

    public PromotionFixPercentageDiscount(string itemId, decimal discountPercentage)
    {
        ItemId = itemId;
        DiscountPercentage = discountPercentage;
    }

    public decimal ApplyPromotion(string id, int itemCount, PricingRule pricingRules)
    {
        if (id == ItemId && itemCount > 0)
        {
            var total = pricingRules.Price * itemCount;
            var discount = total * DiscountPercentage / 100M;
            return total - discount;
        }
        else
            return pricingRules.Price;
    }
}
