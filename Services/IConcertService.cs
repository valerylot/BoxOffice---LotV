using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice___LotV.DTOs;
using BoxOffice___LotV.Models;

namespace BoxOffice___LotV.Services
{
    public interface IConcertService
    {
        List<ConcertResponseDTO> GetAll();
        ConcertResponseDTO? GetById(int id);
        ConcertResponseDTO Create(CreateConcertDTO concert);
        bool Update(int id, UpdateConcertDTO concert);
        void Delete(int id);
        int SellTickets(int id, int quantity);
    }
}