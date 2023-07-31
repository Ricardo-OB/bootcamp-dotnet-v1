using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public class Tour : BaseEntity<int> {

    public string Name { get; set; } = "";

    public string City { get; set; } = "";

    private DateTime _date = DateTime.Now;
    public string Date {
        get { return _date.ToString("yyyy-MM-dd"); }
        set { DateTime.TryParse(value, out _date); }
    }

    public bool IsSoldOut { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> id this tour belongs to
    /// </summary>
    [ForeignKey("Id")]
    public int ArtistId { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> Entity this Tour is refering to
    /// </summary> <summary>
    public virtual Artist ? Artist { get; set; } 

}