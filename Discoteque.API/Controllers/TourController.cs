using System.Data.SqlTypes;
using Discoteque.Data.Models;
using Discoteque.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TourController : ControllerBase
{
    private readonly ITourService _tourService;

    public TourController(ITourService tourService)
    {
        _tourService = tourService;
    }

    [HttpGet]
    [Route("GetTours")]
    public async Task<IActionResult> GetTours(bool areReferencesLoaded = false)
    {
        var tours = await _tourService.GetToursAsync(areReferencesLoaded);
        return Ok(tours);
    }

    [HttpGet]
    [Route("GetTourById")]
    public async Task<IActionResult> GetById(int id)
    {
        var tour = await _tourService.GetById(id);
        return Ok(tour);
    }

    [HttpGet]
    [Route("GetToursByCity")]
    public async Task<IActionResult> GetToursByCity(string city)
    {
        var tours = await _tourService.GetToursByCity(city);
        return tours.Any() ? Ok(tours) : StatusCode(StatusCodes.Status404NotFound,  "There was no Tours found in this City");
    }

    [HttpGet]
    [Route("GetToursByDate")]
    public async Task<IActionResult> GetToursByDate(string date)
    {
        var tours = await _tourService.GetToursByDate(date);
        return tours.Any() ? Ok(tours) : StatusCode(StatusCodes.Status404NotFound,  "There was no Tours found in this Date");
    }

    [HttpGet]
    [Route("GetToursBySolds")]
    public async Task<IActionResult> GetToursBySolds(bool isSold)
    {
        var tours = await _tourService.GetToursBySolds(isSold);
        return tours.Any() ? Ok(tours) : StatusCode(StatusCodes.Status404NotFound,  "There was no Tours found");
    }
    [HttpGet]
    [Route("GetToursByArtist")]
    public async Task<IActionResult> GetToursByArtist(string artist)
    {
        var tours = await _tourService.GetToursByArtist(artist);
        return tours.Any() ? Ok(tours) : StatusCode(StatusCodes.Status404NotFound,  "There was no Tours by this artist");
    }

    [HttpPost]
    [Route("CreateTour")]
    public async Task<IActionResult> CreateToursAsync(Tour Tour)
    {
        var result = await _tourService.CreateTour(Tour);
        return Ok(result); 
    }
}