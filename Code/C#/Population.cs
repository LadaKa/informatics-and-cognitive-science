namespace Code;

public class Population
{
    private Dictionary<int, Neuron> neurons = new Dictionary<int, Neuron>();
    private int spikeTimesBinIntervalMs = 0;

    public Population(
        Activities activities, 
        int excludedInitialTimeMs,
        int totalTimeMs,
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
            decimal eventTime = activities.events.times[i];
            if (eventTime > excludedInitialTimeMs)
            {
                neurons[activities.events.senders[i]].AddSpikeTime(eventTime);
            }
        }

        spikeTimesBinIntervalMs = binIntervalMs;
        InitNeuronsSpikeTimesBins(excludedInitialTimeMs, totalTimeMs);
    }


    // The ﬁring rate of individual neurons is estimated 
    // as the average spike count 
    // over the full simulation period, 
    // excluding the ﬁrst 500 ms of initial network transients.
    private void InitNeuronsSpikeTimesBins(
        int excludedInitialTimeMs,
        int totalTimeMs)
    {
        int binsCount = (totalTimeMs-excludedInitialTimeMs)/spikeTimesBinIntervalMs;

        foreach (Neuron neuron in neurons.Values)
        {
            // compute number of spikes in bins
            neuron.InitSpikeTimesBins(
                excludedInitialTimeMs, 
                spikeTimesBinIntervalMs, 
                binsCount);
        }
    }

    // Returns dictionary 
    // that contains firing rate counts for population
    // over the given simulation period:
    // 
    // <firing rate, count of neurons with this firing rate>
    public Dictionary<decimal,int> ComputeFiringRateCounts(
        int excludedInitialTimeMs,
        int startTimeMs,
        int endTimeMs)
    {
        Dictionary<decimal,int> firingRateCounts = new Dictionary<decimal,int>();
        foreach (Neuron neuron in neurons.Values)
        {
            // compute individual firing rate
            decimal neuronFiringRate = neuron.ComputeFiringRate(
                excludedInitialTimeMs, startTimeMs, endTimeMs, spikeTimesBinIntervalMs);

            if (!firingRateCounts.ContainsKey(neuronFiringRate))
            {
                firingRateCounts.Add(neuronFiringRate, 1);
            }
            else
            {
                firingRateCounts[neuronFiringRate]++;
            }
        }

        return firingRateCounts;
    }

    public int GetNeuronsCount()
    {
        return neurons.Count;
    }

}

