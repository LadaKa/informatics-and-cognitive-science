namespace Code;

public class NetworkModel
{
    private Population stkPopulation;
    private Population gpePopulation;

    public NetworkModel(
        Activities stkActivities,
        Activities gpeActivities)
        {
            stkPopulation = new Population(stkActivities);
            gpePopulation = new Population(gpeActivities);
        }

}