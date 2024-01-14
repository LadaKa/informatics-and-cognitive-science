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
            int binIndex = ((int)spikeTime)/binIntervalMs;
            spikeTimesBins[binIndex]++;
            includedSpikeCount++;
        }
    }

    // The ﬁring rate of individual neurons is estimated 
    // as the average spike count 
    // over the given simulation period
    // (excluding the ﬁrst 500 ms of initial network transients)
    public decimal ComputeFiringRate(
        int startTimeMs, 
        int endTimeMs,
        int binIntervalMs)
    {
        int startBinIndex = startTimeMs/binIntervalMs;
        int endBinIndex = endTimeMs/binIntervalMs;
        int sumFiringRateOfBins = 0;
        for (int binIndex = startBinIndex; binIndex < endBinIndex; binIndex++)
        {
            sumFiringRateOfBins = sumFiringRateOfBins + spikeTimesBins[binIndex];
        }

        decimal avgFiringRateOfBins = decimal.Divide(sumFiringRateOfBins, endBinIndex-startBinIndex);
        decimal firingRate = avgFiringRateOfBins * (decimal.Divide(1000, binIntervalMs));
        
        Console.WriteLine("Firing rate: " + firingRate);
        return firingRate;
    }

}