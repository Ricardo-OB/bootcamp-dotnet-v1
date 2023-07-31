using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface IArtistService {

    Task<IEnumerable<Artist>> GetAllArtists();

    Task<Artist> GetById(int id);

    Task<Artist> CreateArtist(Artist artist);

    Task<Artist> UpdateArtist(Artist artist);

}