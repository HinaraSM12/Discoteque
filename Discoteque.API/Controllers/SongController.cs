using System.Data.SqlTypes;
using Discoteque.Data.Models;
using Discoteque.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Discoteque.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SongController : ControllerBase
{
    private readonly ISongService _songService;

    public SongController(ISongService songService)
    {
        _songService = songService;
    }

    [HttpGet]
    [Route("GetSongs")]
    public async Task<IActionResult> GetSongs(bool areReferencesLoaded = false)
    {
        var Songs = await _songService.GetSongsAsync(areReferencesLoaded);
        return Ok(Songs);
    }

    [HttpGet]
    [Route("GetSongById")]
    public async Task<IActionResult> GetById(int id)
    {
        var Song = await _songService.GetById(id);
        return Ok(Song);
    }

    [HttpGet]
    [Route("GetSongsByDuration")]
    public async Task<IActionResult> GetSongsByDuration(float duration)
    {
        var songs = await _songService.GetSongsByDuration(duration);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There was no Songs found in this Duration");
    }

    [HttpGet]
    [Route("GetSongsByDurationRange")]
    public async Task<IActionResult> GetSongsByDurationRange(float initialDuration, float durationRange)
    {
        var songs = await _songService.GetSongsByDurationRange(initialDuration, durationRange);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There was no Songs found in this Duration range");
    }

    [HttpGet]
    [Route("GetSongsByAlbum")]
    public async Task<IActionResult> GetSongsByAlbum(string album)
    {
        var songs = await _songService.GetSongsByAlbum(album);
        return songs.Any() ? Ok(songs) : StatusCode(StatusCodes.Status404NotFound,  "There was no Songs by this album");
    }

    [HttpPost]
    [Route("CreateSong")]
    public async Task<IActionResult> CreateSongsAsync(Song song)
    {
        var result = await _songService.CreateSong(song);
        return Ok(result);
    }
}