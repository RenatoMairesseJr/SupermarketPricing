using Domain.Dto;

namespace Infrastructure.Provider.Promotions;

public interface IPromotion
{
    string ItemId { get; }
    decimal ApplyPromotion(string id, int itemCount, PricingRule pricingRules);
}
