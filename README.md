# PortfolioProjector
A portfolio projector API. The API takes a three parameters:
- sims: The number of simulations you want to run.
- years: The amount of years to calculate for.
- startingAmount: The initial investment amount.
and returns a sequence of tuples correlating to the 10th, 45th, and 95th percentiles [(low, medium, high),..] . 
The Monte Carlo simulation is written in F#. A C# backend handles the requests.

