using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoxOffice___LotV.Data;
using BoxOffice___LotV.Models;

namespace BoxOffice___LotV.Repository
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly AppDbContext _db;
        
        //constructor
        public ConcertRepository (AppDbContext db)
        {
            _db = db;
        }

        public List<Concert> GetAll()
        {
            return _db.Concerts.ToList();
        }

        public Concert? GetById(int id)
        {
            return _db.Concerts.FirstOrDefault(c => c.Id == id);
        }

        public Concert Create(Concert concert)
        {
            _db.Concerts.Add(concert);
            _db.SaveChanges();
            return concert;
        }

        public void Update(int id, Concert concert)
        {
            _db.SaveChanges();
        }

        public void Delete(Concert concert)
        {
            _db.Concerts.Remove(concert);
            _db.SaveChanges();
        }
    }
}