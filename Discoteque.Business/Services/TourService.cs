using System.Security.Cryptography.X509Certificates;
using Discoteque.Data;
using Discoteque.Data.Models;
using Discoteque.Data.Services;

namespace Discoteque.Business.Services;

/// <summary>
/// This is a Tour service implementation of <see cref="ITourService"/> 
/// </summary>
public class TourService : ITourService
{
    private IUnitOfWork _unitOfWork;

    public TourService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new <see cref="Tour"/> entity in Database. 
    /// </summary>
    /// <param name="tour">A new Tour entity</param>
    /// <returns>The created Tour with an Id assigned</returns>
    public async Task<Tour> CreateTour(Tour tour)
    {
        var newTour = new Tour{
            Name = tour.Name,
            ArtistId = tour.ArtistId,
            Date = tour.Date,
            IsSold = tour.IsSold
        };
        
        await _unitOfWork.TourRepository.AddAsync(newTour);
        await _unitOfWork.SaveAsync();
        return newTour;
    }

    /// <summary>
    /// Finds all Tours in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artists per Tour if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    public async Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded)
    {
        IEnumerable<Tour> tours;
        if(areReferencesLoaded)
        {
            tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        }
        else
        {
            tours = await _unitOfWork.TourRepository.GetAllAsync();
        }
        
        return tours;
    }

    /// <summary>
    /// A list of Tours released by a <see cref="Artist.Name"/>
    /// </summary>
    /// <param name="artist">The name of the artist</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    public async Task<IEnumerable<Tour>> GetToursByArtist(string artist)
    {
        IEnumerable<Tour> tours;        
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Artist.Name.Equals(artist), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    /// <summary>
    /// Returns all Tours published at the city.
    /// </summary>
    /// <param name="city">A name of a city</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    public async Task<IEnumerable<Tour>>  GetToursByCity(string city)
    {
        IEnumerable<Tour> tours;        
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.City.Equals(city), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    /// <summary>
    /// Returns all Tours published at the date.
    /// </summary>
    /// <param name="date">A date in this format YYYY-MM-DD</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    public async Task<IEnumerable<Tour>> GetToursByDate(string date)
    {
        IEnumerable<Tour> tours;        
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Equals(date) , x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }
    /// <summary>
    /// returns all Tours released from initial to max year
    /// </summary>
    /// <param name="isSold">This parameter specify if the tour is sold or not</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    public async Task<IEnumerable<Tour>> GetToursBySolds(bool isSold)
    {
        IEnumerable<Tour> tours;        
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.IsSold.Equals(isSold), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    /// <summary>
    /// Get an Tour by its EF DB Identity
    /// </summary>
    /// <param name="id">The unique ID of the element</param>
    /// <returns>A <see cref="Tour"/> </returns>
    public async Task<Tour> GetById(int id)
    {
        var tour = await _unitOfWork.TourRepository.FindAsync(id);
        return tour;
    }

    /// <summary>
    /// Updates the <see cref="Tour"/> entity in EF DB
    /// </summary>
    /// <param name="tour">The Tour entity to update</param>
    /// <returns>The new Tour with updated fields if successful</returns>
    public async Task<Tour> UpdateTour(Tour tour)
    {
        await _unitOfWork.TourRepository.Update(tour);
        await _unitOfWork.SaveAsync();
        return tour;
    }
}