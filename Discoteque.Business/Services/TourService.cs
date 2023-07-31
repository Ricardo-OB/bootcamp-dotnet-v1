using Discoteque.Data;
using Discoteque.Data.Models;
using Discoteque.Business.IServices;

namespace Discoteque.Business.Services;

/// <summary>
/// This is a Tour service implementation of <see cref="ITourService"/> 
/// </summary>
public class TourService : ITourService {

    private readonly IUnitOfWork _unitOfWork;
    public TourService(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public async Task<Tour> CreateTour(Tour tour) {

        var newTour = new Tour {
            Name = tour.Name,
            City = tour.City,
            Date = tour.Date,
            IsSoldOut = tour.IsSoldOut,
            ArtistId = tour.ArtistId
        };
        
        try {
            if (DateTime.Parse(tour.Date).Year < 2021) {
                throw new ArgumentException("The year must be greater than or equal to 2021");
            }
            
            await _unitOfWork.TourRepository.AddAsync(newTour);
            await _unitOfWork.SaveAsync();

        } catch (Exception e) {
            throw new Exception(e.Message + ". Error in CreateTour method");
        }
        
        return newTour;
    }

    public async Task<Tour> UpdateTour(Tour tour) {
        await _unitOfWork.TourRepository.Update(tour);
        await _unitOfWork.SaveAsync();
        return tour;
    }

    public async Task DeleteTourById(int id) {
        await _unitOfWork.TourRepository.Delete(id);
        await _unitOfWork.SaveAsync();
    }

    public async Task<IEnumerable<Tour>> GetToursAsync(bool areReferencesLoaded) {  
        IEnumerable<Tour> tours;
        if (areReferencesLoaded) {
            tours = await _unitOfWork.TourRepository.GetAllAsync(null, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        } else {
            tours = await _unitOfWork.TourRepository.GetAllAsync();
        }
        return tours;
    }

    public async Task<Tour> GetTourById(int id) {
        var tour = await _unitOfWork.TourRepository.FindAsync(id);
        return tour;
    }

    public async Task<IEnumerable<Tour>> GetToursByName(string name) {
        IEnumerable<Tour> tours;
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.Name.ToLower().Equals(name.ToLower()), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    public async Task<IEnumerable<Tour>> GetToursByCity(string city) {  
        IEnumerable<Tour> tours;
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => x.City.ToLower().Equals(city.ToLower()), x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    public async Task<IEnumerable<Tour>> GetToursByDate(DateTime date) {
        var NewDate = DateTime.Parse(date.ToString("yyyy-MM-dd"));
        IEnumerable<Tour> tours;
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => DateTime.Parse(x.Date) == NewDate, x => x.OrderBy(x => x.Id), new Artist().GetType().Name);
        return tours;
    }

    public async Task<IEnumerable<Tour>> GetToursByDateRange(DateTime initDate, DateTime endDate) {
        var NewInitDate = DateTime.Parse(initDate.ToString("yyyy-MM-dd"));
        var NewEndDate = DateTime.Parse(endDate.ToString("yyyy-MM-dd"));
        IEnumerable<Tour> tours;
        tours = await _unitOfWork.TourRepository.GetAllAsync(x => DateTime.Parse(x.Date) >= NewInitDate  && DateTime.Parse(x.Date) <= endDate, 
                                                             x => x.OrderBy(x => x.Id),
                                                             new Artist().GetType().Name
                                                            );
        return tours;
    }

}
