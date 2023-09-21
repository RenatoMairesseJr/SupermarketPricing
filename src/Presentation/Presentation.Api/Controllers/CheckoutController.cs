//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Dto;
using Infrastructure.Provider.Checkout;
using Infrastructure.Provider.Promotions;
using Infrastructure.Middleware.Exception;

namespace Presentation.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class CheckoutController : ControllerBase
{
    /// <summary>
    /// Mock some price rules
    /// </summary>

    public CheckoutController(ICheckoutProvider checkout) {
        _checkout = checkout;
    }

    private readonly ICheckoutProvider _checkout;


    /// <summary>
    /// Return total based on items already scanned
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    /// <exception cref="BadRequestException"></exception>
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status400BadRequest)]
    [HttpPost("ReturnTotal")]
    public IActionResult ReturnTotal([FromBody] List<string> items)
    {
        if(items.Count == 0)
        {
            throw new BadRequestException("Scanned item list is empty.");
        }

        //Mock-up special prices promotion
        List<IPromotion> activePromotions = new()
        {
            new PromotionSpecialPrice("A", 3, 130),
            new PromotionSpecialPrice("B", 2, 45)
        };

        //Mock-up price rules 
        List<PricingRule> priceRules = new()
        {
            new PricingRule { Id = "A", Price = 50 },
            new PricingRule { Id = "B", Price = 30 },
            new PricingRule { Id = "C", Price = 20 },
            new PricingRule { Id = "D", Price = 15 }
        };

        //Initialize checkout
        _checkout.New(priceRules, activePromotions);

        foreach (var item in items)
        {
            _checkout.Scan(item);
        }

        return Ok(_checkout.Total);
    }
}
