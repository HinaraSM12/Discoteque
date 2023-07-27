using Discoteque.Data.Models;
namespace Discoteque.Data.Services;

public interface ISongService
{
    /// <summary>
    /// Finds all Songs in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per Song if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsAsync(bool areReferencesLoaded);
    
    /// <summary>
    /// Returns all Songs published in the year.
    /// </summary>
    /// <param name="duration">A gregorian year between 1900 and current year</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByDuration(float duration);
    
    /// <summary>
    /// returns all Songs released from initial to max year
    /// </summary>
    /// <param name="initialDuration">The initial Duration, min value 0</param>
    /// <param name="maxDuration">the latest duration, max value 7</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByDurationRange(float initialDuration, float maxDuration);
    
    /// <summary>
    /// A list of Songs released by a <see cref="Album.Name"/>
    /// </summary>
    /// <param name="album">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Song"/> </returns>
    Task<IEnumerable<Song>> GetSongsByAlbum(string album);
    
    
    /// <summary>
    /// Get an Song by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Song"/> </returns>
    Task<Song> GetById(int id);
    
    /// <summary>
    /// Creates a new <see cref="Song"/> entity in Database. 
    /// </summary>
    /// <param name=song">A new Song entity</param>
    /// <returns>The created Song with an Id assigned</returns>
    Task<Song> CreateSong(Song song);
    
    /// <summary>
    /// Updates the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="song">The Song entity to update</param>
    /// <returns>The new Song with updated fields if successful</returns>
    Task<Song> UpdateSong(Song song);
}