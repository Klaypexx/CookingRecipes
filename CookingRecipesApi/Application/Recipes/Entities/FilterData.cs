namespace Application.Recipes.Entities;

public class FilterData
{
    public string Time { get; set; } = string.Empty;
    public string Portion { get; set; } = string.Empty;

    public FilterData( string time, string portion )
    {
        Time = time;
        Portion = portion;
    }
}
