using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArtistsController : ControllerBase {

    private readonly IArtistService _artistService;

    public ArtistsController(IArtistService artistService) {
        _artistService = artistService;
    }

    [HttpGet]
    [Route("GetArtists")]
    public async Task<IActionResult> GetArtistsAsync() {
        var allArtists = await _artistService.GetAllArtists();
        return Ok(allArtists);
    }

    [HttpPost]
    [Route("CreateArtist")]
    public async Task<IActionResult> CreateArtistAsync(Artist artist) {
        var artistCreated = await _artistService.CreateArtist(artist);
        return Ok(artistCreated);
    }

    [HttpGet]
    [Route("GetArtistById")]
    public async Task<IActionResult> GetArtistById(int id) {
        var artist = await _artistService.GetById(id);
        return Ok(artist);
    }

    [HttpPatch]
    [Route("UpdateArtist")]
    public async Task<IActionResult> UpdateArtist(Artist artist) {
        var artistUpdated = await _artistService.UpdateArtist(artist);
        return Ok(artistUpdated);
    }

}
