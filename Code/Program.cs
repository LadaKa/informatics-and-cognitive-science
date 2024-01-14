namespace Code;

class Program
{
    // static string NestJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSample.json";
    static string STKJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSTK.json";
    static string GPeJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataGPe.json";

    static int simulationTotalTime = 1000; // [ms]

    static void Main(string[] args)
    {
        NestActivityDataParser parser = new NestActivityDataParser();
        
        NetworkModel networkModel = new NetworkModel(
            parser.ParseActivityData(STKJsonFileName),
            parser.ParseActivityData(GPeJsonFileName));

        Console.WriteLine("OK");
    }
}
