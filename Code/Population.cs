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
        int binsCount = totalTimeMs/spikeTimesBinIntervalMs;

        foreach (Neuron neuron in neurons.Values)
        {
            // compute number of spikes in bins
            neuron.InitSpikeTimesBins(
                excludedInitialTimeMs, spikeTimesBinIntervalMs, binsCount);
        }
    }

    // Returns dictionary 
    // that contains firing rate counts for population
    // over the given simulation period:
    // 
    // <firing rate, count of neurons with this firing rate>
    public Dictionary<decimal,int> ComputeFiringRateCounts(
        int startTimeMs,
        int endTimeMs)
    {
        Dictionary<decimal,int> firingRateCounts = new Dictionary<decimal,int>();
        foreach (Neuron neuron in neurons.Values)
        {
            // compute individual firing rate
            decimal neuronFiringRate = neuron.ComputeFiringRate(
                startTimeMs, endTimeMs, spikeTimesBinIntervalMs);

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

