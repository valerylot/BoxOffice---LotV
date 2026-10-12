

using BoxOffice___LotV.DTOs;
using BoxOffice___LotV.Models;
using BoxOffice___LotV.Repository;

namespace BoxOffice___LotV.Services
{
    public class ConcertService : IConcertService
    {
        private readonly IConcertRepository _repository;

        public ConcertService(IConcertRepository repository)
        {
            _repository = repository;
        }

        public List<ConcertResponseDTO> GetAll()
        {
            return _repository.GetAll().Select(c => ToReadDTO(c)).ToList();
        }

        public ConcertResponseDTO? GetById(int id)
        {
            Concert? concert = _repository.GetById(id);

            if(concert == null)
            {
                return null;
            }

            return ToReadDTO(concert);
        }

        public ConcertResponseDTO? Create(CreateConcertDTO dto)
        {
            //checks that the date is in the future
            if (dto.Date <= DateTime.UtcNow)
            {
                return null;
            }
            //checks that the venue is not already booked on the same day
            bool isVenueBooked = _repository.GetAll().Any(c => c.Venue == dto.Venue && c.Date.Date == dto.Date.Date);
            if (isVenueBooked)
            {
                return null;
            }

            Concert concert = new Concert();
            concert.Artist = dto.Artist;
            concert.OpeningAct = dto.OpeningAct;
            concert.Venue = dto.Venue;
            concert.City = dto.City;
            concert.Date = dto.Date;
            concert.Genre = dto.Genre;
            concert.TicketPrice = dto.TicketPrice;
            concert.Capacity = dto.Capacity;

            Concert created = _repository.Create(concert);


            return ToReadDTO(created);
        }

        public bool Update(int id, UpdateConcertDTO concert)
        {
            Concert? exists = _repository.GetById(id);
            if(exists == null)
            {
                return false;
            }

            exists.Artist = concert.Artist;
            exists.OpeningAct = concert.OpeningAct;
            exists.Venue = concert.Venue;
            exists.City = concert.City;
            exists.Date = concert.Date;
            exists.Genre = concert.Genre;
            exists.TicketPrice = concert.TicketPrice;
            exists.Capacity = concert.Capacity;

            _repository.Update(id, exists);
            return true;
        }

        public void Delete(int id)
        {
            Concert? concert = _repository.GetById(id);

            if(concert != null)
            {
                _repository.Delete(concert);
            }
        }

        public int SellTickets(int id, int quantity)
        {
            Concert? exists = _repository.GetById(id);

            if(exists == null)
            {
                return -1; //returns 404 unknown id
            }
            //this checks if the capacity of the venue is less than the quantity tickets sold, throws an error
            if(exists.Capacity < quantity)
            {
                return -2; //return 409 if there's not enough tickets remaining
            }
            //this deducts the tickets sold from the total
            exists.Capacity -= quantity;

            _repository.Update(id, exists);
            return 1; //returns 200 if successful
        }

        //HELPER METHOD
        private static ConcertResponseDTO ToReadDTO(Concert concert)
        {
            ConcertResponseDTO outputDTO = new ConcertResponseDTO();
            outputDTO.Id = concert.Id;
            outputDTO.Artist = concert.Artist;
            outputDTO.OpeningAct = concert.OpeningAct;
            outputDTO.Venue = concert.Venue;
            outputDTO.City = concert.City;
            outputDTO.Date = concert.Date;
            outputDTO.Genre = concert.Genre;
            outputDTO.TicketPrice = concert.TicketPrice;
            outputDTO.Capacity = concert.Capacity;
            outputDTO.TicketsSold = concert.TicketsSold;
            outputDTO.TicketsRemaining = concert.Capacity - concert.TicketsSold;

            return outputDTO;
        }
    }
}