namespace Code;

//  .NET SDK fix: sudo ln -s /snap/dotnet-sdk/current/dotnet /usr/local/bin/dotnet
class Program
{
    static string STNJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSTN.json";
    static string GPeJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataGPe.json";

    // time values [ms]:
    static int simulationTotalTimeMs = 1000;
    static int excludedInitialTimeMs = 500;
    static int binIntervalMs = 10; 

    //  TODO: What are correct start times? Should be DC start after 500 ms?

    //  Poisson generator 1 (STN, GPe):   0 - 1000   
    //  Direct current (STN):           600 - 1000
    //  Poisson generator 2 (GPe):      800 - 1000

    //  Excluded Initial Time:          500
    static int[] conditionsStartsMs = new int[]{500, 600, 800};

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

        Dictionary<string, Dictionary<decimal,decimal>[]> firingRatesDistributions 
            = firingRatesDistribution.GetDistributionsForNetworkModel(
                networkModel,
                excludedInitialTimeMs);

        Console.WriteLine("OK");
    }
}
