using Discoteque.Business.IServices;
using Discoteque.Data;
using Discoteque.Data.Models;

namespace Discoteque.Business.Services;

public class SongService : ISongService {

    private readonly IUnitOfWork _unitOfWork;
    public SongService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Song> CreateSong(Song song) {

        var newSong = new Song {
            Name = song.Name,
            Duration = song.Duration,
            AlbumId = song.AlbumId
        };

        try {
            var valAlbumId = await _unitOfWork.AlbumRepository.FindAsync(song.AlbumId);
            
            if (valAlbumId == null) {
                throw new ArgumentException("AlbumId does not exist");
            }
            
            await _unitOfWork.SongRepository.AddAsync(newSong);
            await _unitOfWork.SaveAsync();

        } catch (Exception e) {
            throw new Exception(e.Message + ". Error in CreateSong method");
        }

        return newSong;
    }

    public async Task<List<Song>> CreateSongs(List<Song> songs) {

        int _idSongProblem = 0;
        try {
            foreach (var song in songs) {
                var newSong = new Song {
                    Name = song.Name,
                    Duration = song.Duration,
                    AlbumId = song.AlbumId
                };
                _idSongProblem = newSong.Id;
                await _unitOfWork.SongRepository.AddAsync(newSong);
            }
            await _unitOfWork.SaveAsync();

        } catch (Exception e) {
            throw new Exception(e.Message + ". Error in CreateSongs method. Problem with Song, check song with id " + _idSongProblem.ToString());
        }

        return songs;
    }

    public async Task DeleteSongById(int id) {
        await _unitOfWork.SongRepository.Delete(id);
        await _unitOfWork.SaveAsync();
        // Debe retornar algo?
    }

    public async Task<Song> GetSongById(int id) {
        var song =  await _unitOfWork.SongRepository.FindAsync(id);
        return song;
    }

    public async Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded) {
        IEnumerable<Song> songs;
        if (areReferencesLoaded) {
            songs = await _unitOfWork.SongRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        } else {
            songs = await _unitOfWork.SongRepository.GetAllAsync();
        }
        return songs;
    }

    public async Task<IEnumerable<Song>> GetSongsByDuration(double duration) {
        IEnumerable<Song> songs;
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration.Equals(duration), x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    public async Task<IEnumerable<Song>> GetSongsByDurationGreaterThanOrEqualTo(double duration) {
        IEnumerable<Song> songs;
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration >= duration, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    public async Task<IEnumerable<Song>> GetSongsByDurationLessThanOrEqualTo(double duration) {  
        IEnumerable<Song> songs;
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration <= duration, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    public async Task<IEnumerable<Song>> GetSongsByDurationRange(double minDuration, double maxDuration) {
        IEnumerable<Song> songs;
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration >= minDuration && x.Duration <= maxDuration, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    public async Task<IEnumerable<Song>> GetSongsByName(string name) { 
        IEnumerable<Song> songs;
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Name.ToLower().Equals(name.ToLower()), x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    public async Task<Song> UpdateSong(Song song) {
        await _unitOfWork.SongRepository.Update(song);
        await _unitOfWork.SaveAsync();
        return song;
    }

}