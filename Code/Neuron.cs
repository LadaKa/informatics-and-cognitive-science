namespace Code;

public class Neuron 
{
    private List<decimal> spikeTimes = new List<decimal>();

    public void AddSpikeTime(decimal spikeTime)
    {
        spikeTimes.Add(spikeTime);
    }

}