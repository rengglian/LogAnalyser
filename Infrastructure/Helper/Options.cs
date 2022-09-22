namespace Infrastructure.Helper;

public class Options
{
    public double Value { get; set; }

    public string Description { get; set; }

    public Options(double value, string description)
    {
        Value = value;
        Description = description;
    }
}
