using System.Text;

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

    public Dictionary<string, Dictionary<decimal,decimal>[]> GetDistributionsForNetworkModel(
        NetworkModel model, int excludedInitialTimeMs)
    {
        Dictionary<string, Dictionary<decimal,decimal>[]> distributions 
            = new Dictionary<string, Dictionary<decimal,decimal>[]>();
        
        distributions.Add(
            model.stn, 
            GetDistributionsForPopulation(
                model.GetPopulationByName(model.stn), excludedInitialTimeMs));

        distributions.Add(
            model.gpe, 
            GetDistributionsForPopulation(
                model.GetPopulationByName(model.gpe), excludedInitialTimeMs));

        return distributions;
    }

    private Dictionary<decimal,decimal>[] GetDistributionsForPopulation(
        Population population, int excludedInitialTimeMs)
    {
        int conditionsCount = conditionsStartsMs.Count();
        Dictionary<decimal, int>[] firingRateCounts 
            = GetFiringRateCountsForPopulation(population, excludedInitialTimeMs);

        int neuronsCount = population.GetNeuronsCount();
        Dictionary<decimal, decimal>[] distributions 
            = new Dictionary<decimal, decimal>[conditionsCount];

        for (int i = 0; i < conditionsCount; i++)
        {
            distributions[i] = new Dictionary<decimal, decimal>();
            foreach (decimal key in firingRateCounts[i].Keys)
            {
                distributions[i].Add(
                    key, 
                    decimal.Divide(firingRateCounts[i][key], neuronsCount));
            }
        }

        return distributions;
    }

    private Dictionary<decimal,int>[] GetFiringRateCountsForPopulation(
        Population population, int excludedInitialTimeMs)
    {
        int conditionsCount = conditionsStartsMs.Count();
        Dictionary<decimal, int>[] firingRateCounts = new Dictionary<decimal, int>[conditionsCount];

        for (int i = 0; i < (conditionsCount - 1); i++)
        {
            int startMs = conditionsStartsMs[i];
            int endMs = conditionsStartsMs[i+1];
            firingRateCounts[i] = population.ComputeFiringRateCounts(
                excludedInitialTimeMs, startMs, endMs);
        }

        firingRateCounts[conditionsCount-1] 
            = population.ComputeFiringRateCounts(
                excludedInitialTimeMs, conditionsStartsMs[conditionsCount-1], totalTimeMs);

        return firingRateCounts;
    }

    public void WriteDistributionValuesToFile(
        Dictionary<string, Dictionary<decimal,decimal>[]> distributions)
    {
        string txt = ".txt";
        
        foreach (KeyValuePair<string, Dictionary<decimal,decimal>[]> populationDistributions in distributions)
        {
            // "STK.txt", "GPE.txt"
            StreamWriter w = new StreamWriter(populationDistributions.Key + txt);
    
            string xValuesLine = String.Empty;
            string yValuesLine = String.Empty;

            foreach (var distr in populationDistributions.Value)
            {
                Console.WriteLine(populationDistributions.Key);
                xValuesLine = String.Join(",", distr.Keys);
                yValuesLine = String.Join(",", distr.Values);

                w.WriteLine(xValuesLine);
                w.WriteLine(yValuesLine);
            }

            w.Close();
        }
        
    }
    
}