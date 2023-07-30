using System.ComponentModel.DataAnnotations.Schema;

namespace Discoteque.Data.Models;

public class Song : BaseEntity<int> {

    public string Name { get; set; } = "";

    public int Duration { get; set; } = 0;

    /// <summary>
    /// The <see cref="Album"/> id this song belongs to
    /// </summary>
    [ForeignKey("Id")]
    public int AlbumId {get; set;}

    /// <summary>
    /// The <see cref="Album"/> Entity this song is refering to
    /// </summary>
    public virtual Album ? Album { get; set;}

}