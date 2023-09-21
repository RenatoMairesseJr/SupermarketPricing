using Domain.Dto;

namespace Infrastructure.Provider.Promotions;

public class PromotionSpecialPrice : IPromotion
{
    public string ItemId { get; }

    private readonly int Quantity;
    private readonly decimal Price;

    public PromotionSpecialPrice(string itemId, int quantity, decimal price)
    {
        ItemId = itemId;
        Quantity = quantity;
        Price = price;
    }

    public decimal ApplyPromotion(string id, int itemCount, PricingRule pricingRules)
    {
        decimal promotionPrice = 0;

        if (id == ItemId && itemCount > 0)
        {
            while (Quantity > 0 && itemCount >= Quantity)
            {
                promotionPrice += Price;
                itemCount -= Quantity;
            }

            promotionPrice += itemCount * pricingRules.Price;
        }

        return promotionPrice;
    }
}