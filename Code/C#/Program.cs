namespace Code;

//  .NET SDK fix: sudo ln -s /snap/dotnet-sdk/current/dotnet /usr/local/bin/dotnet
class Program
{
    static string STNJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSTK.json";
    static string GPeJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataGPe.json";

    // time values [ms]:
    static int simulationTotalTimeMs = 950;
    static int excludedInitialTimeMs = 500;
    static int binIntervalMs = 10; 

    //  Poisson generator 1 (STN, GPe):    0 - 1000   
    //  Poisson generator 2 (GPe):       650 -  950
    //  Direct current (STN):            800 -  950
   

    //  Excluded Initial Time:          500
    static int[] conditionsStartsMs = new int[]{500, 650, 800};

    static void Main(string[] args)
    {
        NestActivityDataParser parser = new NestActivityDataParser();
        
        // network model
        NetworkModel networkModel = new NetworkModel(
            parser.ParseActivityData(STNJsonFileName),
            parser.ParseActivityData(GPeJsonFileName),
            excludedInitialTimeMs,
            simulationTotalTimeMs,
            binIntervalMs);

        // the distribution of single cell firing rates 
        // separately for each population 
        // in all three conditions
        FiringRatesDistribution firingRatesDistribution 
            = new FiringRatesDistribution(
                conditionsStartsMs, 
                simulationTotalTimeMs);

        Dictionary<string, Dictionary<decimal,decimal>[]> result
            = firingRatesDistribution.GetDistributionsForNetworkModel(
                networkModel,
                excludedInitialTimeMs);

        firingRatesDistribution.WriteDistributionValuesToFile(result);

        Console.WriteLine("OK");
    }
}
