namespace Code;

class Program
{
    static string NestJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSample.json";
    static string STKJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataSTK.json";
    static string GPeJsonFileName = @"/home/lada/Desktop/CognitiveScience/Project/Code/ActivityData/DataGPe.json";
    static void Main(string[] args)
    {
        NestActivityDataParser parser = new NestActivityDataParser();

        Activities activitiesSTK = parser.ParseActivityData(NestJsonFileName);
        Activities activitiesGPe = parser.ParseActivityData(NestJsonFileName);
    }
}
