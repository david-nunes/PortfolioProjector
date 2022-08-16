namespace ClassLibrary1

module PortfolioSims =
    //Returns a CE of one Monte Carlo simulation
    let rec monteCalc (money,years) =
        async {
            // Accepts rand and accumulator arguments. Handles first element by set element == head * money. Base case of [].
            let rec recurCalc (rand: decimal list) (acc: decimal list) =
                match rand with
                | [] -> acc
                | head :: tail when acc.Length = 0 -> recurCalc tail ((head * money) :: acc)
                | head :: tail -> recurCalc tail ((head * acc.Head) :: acc)

            //Call recurCalc function. Produces a list of n random doubles. Converts to decimals for precision.
            return
                recurCalc [ for i in 0..(years - 1) ->
                                ((System.Random().NextDouble() * 0.5) + 0.9)
                                |> decimal ] []
                |> List.rev
        }
    
    //Finds the index for the percentile on a sorted list, then returns item.
    let findPercentile (percentile: float) (inputList: 'a list)=
        let p = ((inputList.Length - 1 |> float) * percentile) |> int
        inputList.[p]
        
    // Sorts a list of lists
    let sortLists (listOfLists: 'a list list) =
        seq{ 0..listOfLists.[0].Length - 1 }
        |> Seq.map (fun c ->
            ([ for i in 0 .. (listOfLists.Length - 1) -> listOfLists.[i][c] ])
            |> List.sort)
       
    //Creates a sequence of CEs to run in parallel.
    let createParallelSeq f amount args =
        let compExpList = seq{for i in 0..amount -> f args}
        async { return! (compExpList |> Async.Parallel) }
        
    //Handler function to calculate portfolio returns.
    let handlePortfolio sims startingMoney years =
            createParallelSeq monteCalc sims (startingMoney,years)
            |> Async.RunSynchronously
            |> Array.toList
            |> sortLists
            |> Seq.map (fun x -> (findPercentile 0.10 x,findPercentile 0.45 x,findPercentile 0.95 x))

//    For testing
//    let answer = handlePortfolio 10 100.0m 50 |> Seq.toList
//    answer
    
    


