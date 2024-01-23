namespace Code;

public class NetworkModel
{
    private Dictionary<string, Population> populations = new Dictionary<string, Population>();
    public readonly string stk = "STK";
    public readonly string gpe = "GPe";


    public NetworkModel(
        Activities stkActivities,
        Activities gpeActivities,
        int totalTimeMs,
        int excludedInitialTimeMs,
        int binIntervalMs)
        {
            populations.Add(
                stk, 
                new Population(stkActivities, totalTimeMs, excludedInitialTimeMs, binIntervalMs));

            populations.Add(
                gpe,
                new Population(gpeActivities, totalTimeMs, excludedInitialTimeMs, binIntervalMs));
        }

    public Population GetPopulationByName(string name)
    {
        return populations[name];
    }
}