namespace Code;

class Program
{
    // static string NestJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSample.json";
    static string STKJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSTK.json";
    static string GPeJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataGPe.json";

    // time values [ms]:
    static int simulationTotalTimeMs = 1000;
    static int excludedInitialTimeMs = 100;//500;
    static int binIntervalMs = 10; 

    //  TODO:
    // 0, 400, 700, 1000

    static void Main(string[] args)
    {
        NestActivityDataParser parser = new NestActivityDataParser();
        
        NetworkModel networkModel = new NetworkModel(
            parser.ParseActivityData(STKJsonFileName),
            parser.ParseActivityData(GPeJsonFileName),
            simulationTotalTimeMs,
            excludedInitialTimeMs,
            binIntervalMs);

        

        // TODO: distribution
        // the distribution of single cell firing rates separately for each population in all three
        // conditions

        FiringRatesDistribution firingRatesDistribution 
            = new FiringRatesDistribution(new int[]{0, 400, 700}, simulationTotalTimeMs);

        Console.WriteLine("OK");
    }
}
