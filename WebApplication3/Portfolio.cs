using Microsoft.FSharp.Collections;

namespace WebApplication3;
using ClassLibrary1;
public class Portfolio
{
    readonly decimal _startingAmount;

    readonly int _years;

    private int _sims;

    public Portfolio(int yearsInput, int simInput, decimal startingMoney)
    {
        _years = yearsInput;
        _sims = simInput;
        _startingAmount = startingMoney;
    }
    public IEnumerable<Tuple<decimal,decimal,decimal>> GetProjection()
    {
        return PortfolioSims.handlePortfolio(_sims, _startingAmount, _years);
    }
}