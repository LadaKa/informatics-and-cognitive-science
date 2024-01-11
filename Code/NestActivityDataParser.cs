namespace Code;

using System.Text.Json;

public class NestActivityDataParser
{
    public Activities ParseActivityData(string jsonFileName)
    {
        try
        {
            string jsonString = File.ReadAllText(jsonFileName);
            Activities activities = 
                JsonSerializer.Deserialize<Activities>(
                    jsonString, 
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            Console.WriteLine(activities.nodeIds[0].ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine($"Parsing of {jsonFileName} done.");
    }
}
