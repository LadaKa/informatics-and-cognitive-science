namespace Code;

public class NetworkModel
{
    private Dictionary<string, Population> populations = new Dictionary<string, Population>();
    public readonly string stn = "STN";
    public readonly string gpe = "GPe";


    public NetworkModel(
        Activities stnActivities,
        Activities gpeActivities,
        int excludedInitialTimeMs,
        int totalTimeMs,
        int binIntervalMs)
        {
            populations.Add(
                stn, 
                new Population(stnActivities, excludedInitialTimeMs, totalTimeMs, binIntervalMs));

            populations.Add(
                gpe,
                new Population(gpeActivities, excludedInitialTimeMs, totalTimeMs, binIntervalMs));
        }

    public Population GetPopulationByName(string name)
    {
        return populations[name];
    }
}