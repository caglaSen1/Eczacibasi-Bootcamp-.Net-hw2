using EczacibasiHW2.Models.Entity;
using System.Collections.Generic;
using System.Linq;

namespace EczacibasiHW2.Models
{
    public class UserRepository
    {
        private readonly CommerceContext _context;

        public UserRepository(CommerceContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll(int page, int pageSize)
        {
            return _context.Users.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public void Delete(int id)
        {
            var p = _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new System.Exception("Not Found");
            
            _context.Users.Remove(p);
            _context.SaveChanges();
        }

        public void Update(int id, User user)
        {
            var u = _context.Users.FirstOrDefault(x => x.Id == id) ?? throw new System.Exception("Not Found");
            
            u.Name = user.Name;
            u.UserType = user.UserType;
            u.Addresses = user.Addresses;
            u.Gender = user.Gender;

            _context.SaveChanges(); 
        }

        
        public List<User> Search(string name, UserType userType,GenderType gender)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query.Where(x => x.Name.Contains(name));
            
            if (userType != 0)
                query.Where(x => x.UserType==userType);
            
            if(gender != 0)
                query.Where((x) => x.Gender==gender);

            return query.ToList();
        }
        
    }
}
