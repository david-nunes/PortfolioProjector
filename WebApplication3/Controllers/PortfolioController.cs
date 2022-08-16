using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers;

[ApiController]
[Route("[controller]")]
public class PortfolioController : ControllerBase
{
    
    [HttpGet(Name = "GetPort")]
    public IEnumerable<Tuple<decimal,decimal,decimal>> Get(int years, int sims, decimal startingAmount)
    {
        Portfolio results = new Portfolio(years, sims, startingAmount);
        return results.GetProjection();
    }
}