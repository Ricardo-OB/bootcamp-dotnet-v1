using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class ArtistService : IArtistService {

    private readonly Random rnd = new();
    private readonly IUnitOfWork _unitOfWork;

    public ArtistService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Artist> CreateArtist(Artist artist) {
        
        var newArtist = new Artist() {
            Id = rnd.Next(1, 9999),
            Name = artist.Name,
            Label = artist.Label,
            IsOnTour = artist.IsOnTour
        };

        try {

            if (artist.Name.Length > 100) {
                throw new ArgumentException("The name of the artist cannot have more than 100 characters");
            }

            await _unitOfWork.ArtistRepository.AddAsync(newArtist);
            await _unitOfWork.SaveAsync();

        } catch (Exception e) {
            throw new Exception(e.Message + ". Error in CreateArtist method");
        }

        return newArtist;
    }

    public async Task<IEnumerable<Artist>> GetAllArtists() {
        
        var artists = await _unitOfWork.ArtistRepository.GetAllAsync();
        return artists;

    }

    public async Task<Artist> GetById(int id) {
        
        var artists = await _unitOfWork.ArtistRepository.FindAsync(id);
        return artists;

    }

    public async Task<Artist> UpdateArtist(Artist artist) {
        
        await _unitOfWork.ArtistRepository.Update(artist);
        await _unitOfWork.SaveAsync();
        return artist;

    }

}