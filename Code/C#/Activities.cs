namespace Code;
public class Activities
{
    public Events? events {get; set;}
    public List<int>? nodeIds { get; set;}

}

public class Events
{
    public List<int>? senders { get; set;}
    public List<decimal>? times { get; set;}
}



