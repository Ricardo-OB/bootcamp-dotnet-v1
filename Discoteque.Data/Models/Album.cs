using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public class Album : BaseEntity<int> {
    public string Name { get; set;} = "";

    public int Year { get; set;}

    public Genres Genre { get; set;} = Genres.Unknown;

    double _price = 50_000;
    public double Price {
        get { return _price; }
        set { _price = Math.Round(value, 2); }
    }

    /// <summary>
    /// The <see cref="Artist"/> id this Album belongs to
    /// </summary>
    [ForeignKey("Id")]
    public int ArtistId { get; set; }

    /// <summary>
    /// The <see cref="Artist"/> Entity this album is refering to
    /// </summary>
    public virtual Artist ? Artist {get; set;}

}

/// <summary>
/// A collection of musical genres
/// </summary>
public enum Genres
{
    Rock,
    Metal,
    Salsa,
    Merengue,
    Urban,
    Folk,
    Indie,
    Techno,
    Vallenato,
    Pop,
    Unknown
}