namespace Code;

public class Population
{
    private Dictionary<int, Neuron> neurons = new Dictionary<int, Neuron>();
    private int spikeTimesBinIntervalMs = 0;

    public Population(
        Activities activities, 
        int totalTimeMs,
        int excludedInitialTimeMs,
        int binIntervalMs)
    {
        // create all neurons
        foreach (int id in activities.nodeIds)
        {
            neurons.Add(id, new Neuron());
        };

        // asign spike events to neurons
        for (int i = 0; i < activities.events.senders.Count; i++)
        {
            neurons[activities.events.senders[i]].AddSpikeTime(activities.events.times[i]);
        }

        spikeTimesBinIntervalMs = binIntervalMs;
        InitNeuronsSpikeTimesBins(totalTimeMs, excludedInitialTimeMs);
    }


    // The ﬁring rate of individual neurons is estimated 
    // as the average spike count 
    // over the full simulation period, 
    // excluding the ﬁrst 500 ms of initial network transients.
    private void InitNeuronsSpikeTimesBins(
        int totalTimeMs,
        int excludedInitialTimeMs)
    {
        int intervalPerSecond = 1000/spikeTimesBinIntervalMs;
        int binsCount = totalTimeMs/spikeTimesBinIntervalMs;

        foreach (Neuron neuron in neurons.Values)
        {
            // compute number of spikes in bins
            neuron.InitSpikeTimesBins(
                excludedInitialTimeMs, spikeTimesBinIntervalMs, binsCount);
        }
    }

    // The distribution of single cell firing rates 
    public void ComputeFiringRateDistribution(
        int startTimeMs,
        int endTimeMs)
    {
        foreach (Neuron neuron in neurons.Values)
        {
            // compute individual firing rates 
            neuron.ComputeFiringRate(
                startTimeMs, endTimeMs, spikeTimesBinIntervalMs);
        }

        // TODO: distribution
    }

}

