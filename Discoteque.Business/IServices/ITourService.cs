using Discoteque.Data.Models;
namespace Discoteque.Data.Services;

public interface ITourService
{
    /// <summary>
    /// Finds all Tours in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per Tour if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded);
    
    /// <summary>
    /// Returns all Tours published at the city.
    /// </summary>
    /// <param name="city">A name of a city</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursByCity(string city);
    
    /// <summary>
    /// Returns all Tours published at the date.
    /// </summary>
    /// <param name="date">A date in this format YYYY-MM-DD</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursByDate(string date);


    /// <summary>
    /// returns all Tours released from initial to max year
    /// </summary>
    /// <param name="isSold">This parameter specify if the tour is sold or not</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursBySolds(bool isSold);
    
    /// <summary>
    /// A list of Tours released by a <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursByArtist(string artist);
    
    
    /// <summary>
    /// Get an Tour by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Tour"/> </returns>
    Task<Tour> GetById(int id);
    
    /// <summary>
    /// Creates a new <see cref="Tour"/> entity in Database. 
    /// </summary>
    /// <param name=tour">A new Tour entity</param>
    /// <returns>The created Tour with an Id assigned</returns>
    Task<Tour> CreateTour(Tour tour);
    
    /// <summary>
    /// Updates the <see cref="Tour"/> entity in EF DB
    /// </summary>
    /// <param name="tour">The Tour entity to update</param>
    /// <returns>The new Tour with updated fields if successful</returns>
    Task<Tour> UpdateTour(Tour tour);
}