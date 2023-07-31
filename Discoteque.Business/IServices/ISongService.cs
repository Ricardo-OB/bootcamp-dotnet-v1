using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface ISongService {

    /// <summary>
    /// Finds all songs in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artist(s) per song if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded);

    Task<Song> GetSongById(int id);
    
    Task<IEnumerable<Song>> GetSongsByName(string name);
    
    Task<IEnumerable<Song>> GetSongsByDuration(double duration);

    Task<IEnumerable<Song>> GetSongsByDurationRange(double minDuration, double maxDuration);
    
    Task<IEnumerable<Song>> GetSongsByDurationLessThanOrEqualTo(double duration);

    Task<IEnumerable<Song>> GetSongsByDurationGreaterThanOrEqualTo(double duration);

    /// <summary>
    /// Creates a new <see cref="Song"/> entity in Database. 
    /// </summary>
    /// <param name="song">A new song entity</param>
    /// <returns>The created song with an Id assigned</returns>
    Task<Song> CreateSong(Song song);

    Task<List<Song>> CreateSongs(List<Song> songs);

    /// <summary>
    /// Updates the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new song with updated fields if successful</returns>
    Task<Song> UpdateSong(Song song);

    /// <summary>
    /// Delete the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="id">The song id to delete</param>
    /// <returns>Succesful response if song id exists</returns>
    Task DeleteSongById(int id);

}