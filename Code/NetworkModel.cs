namespace Code;

public class NetworkModel
{
    private Population stkPopulation;
    private Population gpePopulation;

    public NetworkModel(
        Activities stkActivities,
        Activities gpeActivities,
        int totalTimeMs,
        int excludedInitialTimeMs,
        int binIntervalMs)
        {
            stkPopulation = new Population(
                stkActivities, totalTimeMs, excludedInitialTimeMs, binIntervalMs);

            gpePopulation = new Population(
                gpeActivities, totalTimeMs, excludedInitialTimeMs, binIntervalMs);

        }

}