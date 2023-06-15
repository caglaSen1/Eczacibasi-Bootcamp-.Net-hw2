using EczacibasiHW2.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace EczacibasiHW2.Models
{
    public class AddressRepository
    {
        private readonly CommerceContext _context;

        public AddressRepository(CommerceContext context)
        {
            _context = context;
        }

        public void Add(Address address)
        {
            _context.Addresses.Add(address);
            _context.SaveChanges();
        }

        public Address GetById(int id)
        {
            return _context.Addresses.FirstOrDefault(x => x.Id == id);
        }

        public List<Address> GetAll(int page, int pageSize)
        {
            return _context.Addresses.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public void Update(int id, Address address)
        {
            var a = _context.Addresses.FirstOrDefault(x => x.Id == id) ?? throw new System.Exception("Not Found");

            a.UserId = address.UserId;
            a.City = address.City;
            a.Title = address.Title;
            a.Text = address.Text;
            
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var a = _context.Addresses.FirstOrDefault(x => x.Id == id) ?? throw new System.Exception("Not Found");
           
            _context.Addresses.Remove(a);
            _context.SaveChanges();
        }

        public List<Address> Search(string name, int? userId, string city)
        {
            var query = _context.Addresses.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query.Where(x => x.Title.Contains(name));

            if (userId.HasValue)
                query.Where(x => x.UserId == userId);

            if (!string.IsNullOrWhiteSpace(city))
                query.Where(x => x.City.Contains(city));

            return query.ToList();
        }

        
    }
}
