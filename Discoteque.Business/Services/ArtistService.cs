namespace Discoteque.Business.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;

public class ArtistService : IArtistService
{
    private readonly Random rnd = new Random();

    public async Task<Artist> CreateArtist(Artist artist)
    {
        List<Artist> artists = new List<Artist>();
        
        artists.Add(
                new Artist() {
                    Id = rnd.Next(1, 9999),
                    Name = artist.Name,
                    Label = artist.Label,
                    IsOnTour = artist.IsOnTour
                });

        List<Artist> artistsList = await Task.FromResult(artists);
        return artistsList[0];
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Artist>> GetAllArtists()
    {
        List<Artist> artists2 = new List<Artist>();
        
        artists2.Add(new Artist() {Id = rnd.Next(1, 9999), Name = "Charlotte de Witte", Label = "KNTXT", IsOnTour = true});
        artists2.Add(new Artist() {Id = rnd.Next(1, 9999), Name = "Amelie Lens", Label = "Exhale", IsOnTour = true});
        artists2.Add(new Artist() {Id = rnd.Next(1, 9999), Name = "Adam Beyer", Label = "Drumcode", IsOnTour = true});
        artists2.Add(new Artist() {Id = rnd.Next(1, 9999), Name = "Arman John", Label = "nn", IsOnTour = false});
        artists2.Add(new Artist() {Id = rnd.Next(1, 9999), Name = "KI/KI", Label = "Slash", IsOnTour = true});

        IEnumerable<Artist> artistsList = await Task.FromResult(artists2);
        return artistsList;
        throw new NotImplementedException();
    }

    public Task<Artist> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Artist> UpdateArtist(Artist artist)
    {
        throw new NotImplementedException();
    }

}