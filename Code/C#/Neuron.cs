namespace Code;

public class Neuron 
{
    private List<decimal> spikeTimes = new List<decimal>();
    private int[] spikeTimesBins;

    public void AddSpikeTime(decimal spikeTime)
    {
        spikeTimes.Add(spikeTime);
    }

    public void InitSpikeTimesBins(
        int excludedInitialTimeMs,
        int binIntervalMs,
        int binsCount)
    {
        spikeTimesBins = new int[binsCount];
        int includedSpikeCount = 0;
        foreach (decimal spikeTime in spikeTimes)
        {
            int binIndex = ((int)(spikeTime - 1 - excludedInitialTimeMs))/binIntervalMs;

            //  spike time < total time 
            if (binIndex < binsCount)
            {
                spikeTimesBins[binIndex]++;
                includedSpikeCount++;
            }
           
        }
    }

    // The ﬁring rate of individual neurons is estimated 
    // as the average spike count 
    // over the given simulation period
    public decimal ComputeFiringRate(
        int excludedInitialTimeMs,
        int startTimeMs, 
        int endTimeMs,
        int binIntervalMs)
    {
        int startBinIndex = (startTimeMs-excludedInitialTimeMs)/binIntervalMs;
        int endBinIndex = (endTimeMs-excludedInitialTimeMs)/binIntervalMs;
        int sumFiringRateOfBins = 0;
        for (int binIndex = startBinIndex; binIndex < endBinIndex; binIndex++)
        {
            sumFiringRateOfBins = sumFiringRateOfBins + spikeTimesBins[binIndex];
        }
        
        decimal firingRate = sumFiringRateOfBins * (decimal.Divide(1000, endTimeMs - startTimeMs));
        return firingRate;
    }

}