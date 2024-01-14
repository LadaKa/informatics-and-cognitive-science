namespace Code;

public class Population
{
    private Dictionary<int, Neuron> neurons = new Dictionary<int, Neuron>();

    public Population(Activities activities)
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
    }
}

