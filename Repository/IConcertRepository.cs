using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice___LotV.Models;

namespace BoxOffice___LotV.Repository
{
    public interface IConcertRepository
    {
        List<Concert> GetAll();
        Concert? GetById(int id);
        Concert Create(Concert concert);
        void Update(int id, Concert concert);
        void Delete(Concert concert);
    }
}