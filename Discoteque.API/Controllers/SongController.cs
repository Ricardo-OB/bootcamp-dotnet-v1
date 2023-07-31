using Discoteque.Data.Models;
using Discoteque.Data;
using Discoteque.Business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase {

    private readonly ISongService _songService;

    public SongController(ISongService songService) {
        _songService = songService;
    }

    [HttpPost]
    [Route("CreateSong")]
    public async Task<IActionResult> CreateSong(Song song) {
        var songCreated = await _songService.CreateSong(song);
        return Ok(songCreated);
    }

    [HttpPost]
    [Route("CreateSongs")]
    public async Task<IActionResult> CreateSongs(List<Song> songs) {
        var songsCreated = await _songService.CreateSongs(songs);
        return Ok(songsCreated);
    }

    [HttpDelete]
    [Route("DeleteSongById")]
    public async Task DeleteSongById(int id) {
        await _songService.DeleteSongById(id);
    }

    [HttpPatch]
    [Route("UpdateSong")]
    public async Task<IActionResult> UpdateSong(Song song) {
        var songUpdted = await _songService.UpdateSong(song);
        return Ok(songUpdted);
    }

    [HttpGet]
    [Route("GetSongs")]
    public async Task<IActionResult> GetSongsAsync(bool areReferencesLoaded = false) {
        var artists = await _songService.GetSongsAsync(areReferencesLoaded);
        return Ok(artists);
    }

    [HttpGet]
    [Route("GetSongById")]
    public async Task<IActionResult> GetSongById(int id) {
        var song = await _songService.GetSongById(id);
        return Ok(song);
    }

    [HttpGet]
    [Route("GetSongsByName")]
    public async Task<IActionResult> GetSongsByName(string name) {
        var songs = await _songService.GetSongsByName(name);
        return Ok(songs);
    }

    [HttpGet]
    [Route("GetSongsByDuration")]
    public async Task<IActionResult> GetSongsByDuration(double duration) {
        var songs = await _songService.GetSongsByDuration(duration);
        return Ok(songs);
    }

    [HttpGet]
    [Route("GetSongsByDurationRange")]
    public async Task<IActionResult> GetSongsByDurationRange(double minDuration, double maxDuration) {
        var songs = await _songService.GetSongsByDurationRange(minDuration, maxDuration);
        return Ok(songs);
    }

    [HttpGet]
    [Route("GetSongsByDurationGreaterEqual")]
    public async Task<IActionResult> GetSongsByDurationGreaterThanOrEqualTo(double duration) {
        var songs = await _songService.GetSongsByDurationGreaterThanOrEqualTo(duration);
        return Ok(songs);
    }

    [HttpGet]
    [Route("GetSongsByDurationLessEqual")]
    public async Task<IActionResult> GetSongsByDurationLessThanOrEqualTo(double duration) {
        var songs = await _songService.GetSongsByDurationLessThanOrEqualTo(duration);
        return Ok(songs);
    }

}