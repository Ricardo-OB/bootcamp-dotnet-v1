using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discoteque.Business.IServices;
using Discoteque.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [Route("GetAllArtists")]
        public async Task<IActionResult> GetAllArtistsAsync()
        {
            var allArtists = await _artistService.GetAllArtists();
            return Ok(allArtists);
        }

        [HttpPost]
        [Route("CreateArtist")]
        public async Task<IActionResult> CreateArtistAsync(Artist artist)
        {
            var artistCreated = await _artistService.CreateArtist(artist);
            return Ok(artistCreated);
        }

    }
}
