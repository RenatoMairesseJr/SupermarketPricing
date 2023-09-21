using Domain.Dto;
using Infrastructure.Provider.Promotions;

namespace Infrastructure.Provider.Checkout;

public interface ICheckoutProvider
{
    void New(List<PricingRule> pricingRule, List<IPromotion> activePromotion);
    void Scan(string item);
    decimal Total { get; }
}
