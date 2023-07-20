namespace Discoteque.Data.Models;

public class Artist : BaseEntity<int>
{

    public String Name { get; set; } = "";

    public String Label { get; set; } = "";

    public Boolean IsOnTour { get; set; }

}