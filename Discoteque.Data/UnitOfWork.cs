using Microsoft.EntityFrameworkCore;
using Discoteque.Data.Models;
using Discoteque.Data.Repository;
using Discoteque.Data.IRepositories;

namespace Discoteque.Data;

public class UnitOfWork : IUnitOfWork, IDisposable {

    private readonly DiscotequeContext _context;
    private bool _disposed = false;
    private IRepository<int, Artist> _artistRepository;
    private IRepository<int, Album> _albumRepository;
    private IRepository<int, Song> _songRepository;
    private IRepository<int, Tour> _tourRepository;

    public UnitOfWork(DiscotequeContext context) {
        _context = context;        
    }

    public IRepository<int, Artist> ArtistRepository {
        get {
            if (_artistRepository is null) {
                _artistRepository = new Repository<int, Artist>(_context);
            }
            return _artistRepository;
        }
    }

    public IRepository<int, Album> AlbumRepository {
        get {
            if (_albumRepository is null) {
                _albumRepository = new Repository<int, Album>(_context);
            }
            return _albumRepository;
        }
    }

    public IRepository<int, Song> SongRepository {
        get {
            if (_songRepository is null) {
                _songRepository = new Repository<int, Song>(_context);
            }
            return _songRepository;
        }
    }

    public IRepository<int, Tour> TourRepository {
        get {
            if (_tourRepository is null) {
                _tourRepository = new Repository<int, Tour>(_context);
            }
            return _tourRepository;
        }
    }

    public async Task SaveAsync() {
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException e) {
            e.Entries.Single().Reload();
        }
    }

    # region IDisposable
    public void Dispose() {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing) {
        if (!_disposed) {
            if (disposing) {
                _context.DisposeAsync();
            }
        }
        _disposed = true;
    }
    #endregion

}