using System.Security.Cryptography.X509Certificates;
using Discoteque.Data;
using Discoteque.Data.Models;
using Discoteque.Data.Services;

namespace Discoteque.Business.Services;

/// <summary>
/// This is a Song service implementation of <see cref="ISongService"/> 
/// </summary>
public class SongService : ISongService
{
    private IUnitOfWork _unitOfWork;

    public SongService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Song"/> entity in Database. 
    /// </summary>
    /// <param name="song">A new Song entity</param>
    /// <returns>The created Song with an Id assigned</returns>
    public async Task<Song> CreateSong(Song song)
    {
        var newSong = new Song{
            Name = song.Name,
            AlbumId = song.AlbumId,
            Duration = song.Duration,
        };
        
        await _unitOfWork.SongRepository.AddAsync(newSong);
        await _unitOfWork.SaveAsync();
        return newSong;
    }

    /// <summary>
    /// Finds all Songs in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per Song if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded)
    {
        IEnumerable<Song> songs;
        if(areReferencesLoaded)
        {
            songs = await _unitOfWork.SongRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        }
        else
        {
            songs = await _unitOfWork.SongRepository.GetAllAsync();
        }
        
        return songs;
    }

    /// <summary>
    /// A list of songs released by a <see cref="Album.Name"/>
    /// </summary>
    /// <param name="album">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsByAlbum(string album)
    {
        IEnumerable<Song> songs;        
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Album.Name.Equals(album), x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    /// <summary>
    /// Returns all Songs published in the duration.
    /// </summary>
    /// <param name="duration">A duration between 0 and 7</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsByDuration(float duration)
    {
        IEnumerable<Song> songs;        
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration == duration , x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    /// <summary>
    /// returns all Songs released from initial to max duration
    /// </summary>
    /// <param name="initialYear">The initial duration, min value 0</param>
    /// <param name="maxYear">the latest duration, max value 7</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    public async Task<IEnumerable<Song>> GetSongsByDurationRange(float initialDuration, float maxDuration)
    {
        IEnumerable<Song> songs;        
        songs = await _unitOfWork.SongRepository.GetAllAsync(x => x.Duration >= initialDuration && x.Duration <= maxDuration , x => x.OrderBy(x => x.Id), new Album().GetType().Name);
        return songs;
    }

    /// <summary>
    /// Get an Song by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Song"/> </returns>
    public async Task<Song> GetById(int id)
    {
        var song = await _unitOfWork.SongRepository.FindAsync(id);
        return song;
    }

    /// <summary>
    /// Updates the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new Song with updated fields if successful</returns>
    public async Task<Song> UpdateSong(Song song)
    {
        await _unitOfWork.SongRepository.Update(song);
        await _unitOfWork.SaveAsync();
        return song;
    }
}