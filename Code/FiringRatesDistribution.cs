namespace Code;

//  The distribution of single cell firing rates 
//  in all three conditions

public class FiringRatesDistribution
{
    private int[] conditionsStartsMs;
    private int totalTimeMs;
    public FiringRatesDistribution(
        int[] conditionsStartsMs,
        int totalTimeMs)
    {
        // start times of conditions [ms]
        this.conditionsStartsMs = conditionsStartsMs;
        this.totalTimeMs = totalTimeMs;
    }

    public Dictionary<decimal,decimal>[] GetDistributionsForPopulation(
        Population population)
    {
        int conditionsCount = conditionsStartsMs.Count();
        Dictionary<decimal, int>[] firingRateCounts = GetFiringRateCountsForPopulation(population);

        int neuronsCount = population.GetNeuronsCount();
        Dictionary<decimal, decimal>[] distributions = new Dictionary<decimal, decimal>[conditionsCount];
        for (int i = 0; i < conditionsCount; i++)
        {
            Dictionary<decimal, decimal> distribution = distributions[i];
            foreach (decimal key in firingRateCounts[i].Keys)
            {
                distribution.Add(key, decimal.Divide(firingRateCounts[i][key], neuronsCount));
            }
        }

        return distributions;
    }

    private Dictionary<decimal,int>[] GetFiringRateCountsForPopulation(
        Population population)
    {
        int conditionsCount = conditionsStartsMs.Count();
        Dictionary<decimal, int>[] firingRateCounts = new Dictionary<decimal, int>[conditionsCount];

        for (int i = 0; i < (conditionsCount - 1); i++)
        {
            int startMs = conditionsStartsMs[i];
            int endMs = conditionsStartsMs[i+1];
            firingRateCounts[i] = population.ComputeFiringRateCounts(startMs, endMs);
        }

        firingRateCounts[conditionsCount-1] 
            = population.ComputeFiringRateCounts(conditionsStartsMs[conditionsCount-1], totalTimeMs);

        return firingRateCounts;
    }



    
}