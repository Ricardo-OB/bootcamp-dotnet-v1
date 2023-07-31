using Discoteque.Data.Models;

namespace Discoteque.Business.IServices;

public interface ITourService {

    /// <summary>
    /// Finds all tours in the EF DB
    /// </summary>
    /// <param name="areReferencesLoaded">Returns associated artist per tour if true</param>
    /// <returns>A <see cref="List" /> of <see cref="Tour"/> </returns>
    Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded);

    Task<Tour> GetTourById(int id);

    Task<IEnumerable<Tour>> GetToursByName(string name);

    Task<IEnumerable<Tour>> GetToursByCity(string city);

    Task<IEnumerable<Tour>> GetToursByDate(DateTime date);

    Task<IEnumerable<Tour>> GetToursByDateRange(DateTime initDate, DateTime endDate);

    /// <summary>
    /// Creates a new <see cref="Tour"/> entity in Database. 
    /// </summary>
    /// <param name="tour">A new tour entity</param>
    /// <returns>The created tour with an Id assigned</returns>
    Task<Tour> CreateTour(Tour tour);

    /// <summary>
    /// Updates the <see cref="Song"/> entity in EF DB
    /// </summary>
    /// <param name="tour">The Tour entity to update</param>
    /// <returns>The new tour with updated fields if successful</returns>
    Task<Tour> UpdateTour(Tour tour);

    /// <summary>
    /// Delete the <see cref="Tour"/> entity in EF DB
    /// </summary>
    /// <param name="id">The tour id to delete</param>
    /// <returns>Succesful response if tour id exists</returns>
    Task DeleteTourById(int id);

}