namespace Code;

using System.Text.Json;

public class NestActivityDataParser
{
    public Activities ParseActivityData(string jsonFileName)
    {
        Activities activities = null;
        
        try
        {
            string jsonString = File.ReadAllText(jsonFileName);
            activities = 
                JsonSerializer.Deserialize<Activities>(
                    jsonString, 
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine($"Parsing of {jsonFileName} done.");
        return activities;
    }
}
