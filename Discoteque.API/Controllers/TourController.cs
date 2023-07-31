using Discoteque.Data.Models;
using Discoteque.Data;
using Discoteque.Business.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TourController : ControllerBase {

    private readonly ITourService _tourService;

    public TourController(ITourService tourService) {
        _tourService = tourService;
    }

        [HttpPost]
    [Route("CreateTour")]
    public async Task<IActionResult> CreateTourAsync(Tour tour) {
        var tourCreated = await _tourService.CreateTour(tour);
        return Ok(tourCreated);
    }

    [HttpDelete]
    [Route("DeleteTourById")]
    public async Task DeleteTourAsync(int id) {
        await _tourService.DeleteTourById(id);
    }

    [HttpPatch]
    [Route("UpdateTour")]
    public async Task<IActionResult> UpdateTourAsync(Tour tour) {
        var tourUpdated = await _tourService.UpdateTour(tour);
        return Ok(tourUpdated);
    }

    [HttpGet]
    [Route("GetTours")]
    public async Task<IActionResult> GetToursAsync(bool areReferencesLoaded = false) {
        var tours = await _tourService.GetToursAsync(areReferencesLoaded);
        return Ok(tours);
    }

    [HttpGet]
    [Route("GetTourById")]
    public async Task<IActionResult> GetTourByIdAsync(int id) {
        var tour = await _tourService.GetTourById(id);
        return Ok(tour);
    }

    [HttpGet]
    [Route("GetToursByName")]
    public async Task<IActionResult> GetToursByNameAsync(string name) {
        var tours = await _tourService.GetToursByName(name);
        return Ok(tours);
    }

    [HttpGet]
    [Route("GetToursByCity")]
    public async Task<IActionResult> GetToursByCityAsync(string city) {
        var tours = await _tourService.GetToursByCity(city);
        return Ok(tours);
    }

    [HttpGet]
    [Route("GetToursByDate")]
    public async Task<IActionResult> GetToursByDateAsync(DateTime date) {
        var tours = await _tourService.GetToursByDate(date);
        return Ok(tours);
    }

    [HttpGet]
    [Route("GetToursByDateRange")]
    public async Task<IActionResult> GetToursByDateRangeAsync(DateTime initDate, DateTime endDate) {
        var tours = await _tourService.GetToursByDateRange(initDate, endDate);
        return Ok(tours);
    }

}