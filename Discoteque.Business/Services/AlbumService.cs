using System.Net;
using Discoteque.Data;
using Discoteque.Data.Models;
using Discoteque.Business.IServices;

namespace Discoteque.Business.Services;

/// <summary>
/// This is a Album service implementation of <see cref="IAlbumService"/> 
/// </summary>
public class AlbumService : IAlbumService {

    private readonly IUnitOfWork _unitOfWork;

    public AlbumService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Album"/> entity in Database. 
    /// </summary>
    /// <param name="album">A new album entity</param>
    /// <returns>The created album with an Id assigned</returns>
    public async Task<Album> CreateAlbum(Album album) {
        
        var newAlbum = new Album{
            Name = album.Name,
            ArtistId = album.ArtistId,
            Genre = album.Genre,
            Year = album.Year,
            Price = album.Price
        };

        try {
            if (album.Year < 1905 || album.Year > 2023) {
                throw new ArgumentException("Year of album must be between 1905 and 2023");
            }

            if (album.Price < 0) {
                throw new ArgumentException("Price of album must be positive or zero");
            }

            List<string> wordsExcluded = new() { "Revolución", "Poder", "Amor", "Guerra"};
            if (wordsExcluded.Any(album.Name.Contains)) {
                throw new ArgumentException("The name of the album must not contain the next words: " + string.Join(", ", wordsExcluded));
            }
            
            await _unitOfWork.AlbumRepository.AddAsync(newAlbum);
            await _unitOfWork.SaveAsync();

        } catch (Exception e) {
            throw new Exception(e.Message + ". Error in CreateAlbum method.");
        }

        return newAlbum;
    }

    /// <summary>
    /// Finds all albums in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per album if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<IEnumerable<Album>> GetAlbumsAsync(bool areReferencesLoaded) {     
        IEnumerable<Album> albums;
        if(areReferencesLoaded) {
            albums = await _unitOfWork.AlbumRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        } else {
            albums = await _unitOfWork.AlbumRepository.GetAllAsync();
        }
        
        return albums;
    }

    public async Task<IEnumerable<Album>> GetAlbumsByName(string name) {
        var albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Name.ToLower().Equals(name.ToLower()), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    public async Task<IEnumerable<Album>> GetAlbumsByPrice(double price) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Price.Equals(price), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    public async Task<IEnumerable<Album>> GetAlbumsByPriceRange(double minPrice, double maxPrice) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Price >= minPrice && x.Price <= maxPrice, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    /// <summary>
    /// A list of albums released by a <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<IEnumerable<Album>> GetAlbumsByArtist(string artist) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Artist.Name.ToLower().Equals(artist.ToLower()), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    /// <summary>
    /// Returns all albums with the assigned genre
    /// </summary>
    /// <param name="genre">A genre from the <see cref="Genres"/> list</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<IEnumerable<Album>> GetAlbumsByGenre(Genres genre) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Genre.Equals(genre), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    /// <summary>
    /// Returns all albums published in the year.
    /// </summary>
    /// <param name="year">A gregorian year between 1900 and current year</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<IEnumerable<Album>> GetAlbumsByYear(int year) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year == year , x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    /// <summary>
    /// returns all albums released from initial to max year
    /// </summary>
    /// <param name="initialYear">The initial year, min value 1900</param>
    /// <param name="maxYear">the latest year, max value 2025</param>
    /// <returns>A <see cref="List" /> of <see cref="Album"/> </returns>
    public async Task<IEnumerable<Album>> GetAlbumsByYearRange(int initialYear, int maxYear) {
        IEnumerable<Album> albums;        
        albums = await _unitOfWork.AlbumRepository.GetAllAsync(x => x.Year >= initialYear && x.Year <= maxYear , x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return albums;
    }

    /// <summary>
    /// Get an album by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Album"/> </returns>
    public async Task<Album> GetById(int id) {
        var album = await _unitOfWork.AlbumRepository.FindAsync(id);
        return album;
    }

    /// <summary>
    /// Updates the <see cref="Album"/> entity in EF DB
    /// </summary>
    /// <param name="album">The Album entity to update</param>
    /// <returns>The new album with updated fields if successful</returns>
    public async Task<Album> UpdateAlbum(Album album) {
        await _unitOfWork.AlbumRepository.Update(album);
        await _unitOfWork.SaveAsync();
        return album;
    }



}