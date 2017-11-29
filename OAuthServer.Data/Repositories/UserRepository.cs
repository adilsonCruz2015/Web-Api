using OAuthServer.Data.DataContexts;
using OAuthServer.Domain.Contracts;
using OAuthServer.Domain.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace OAuthServer.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private OAuthServerDataContext _db;

        public UserRepository(OAuthServerDataContext db)
        {
            this._db = db;
        }
        public User Autenticate(string username, string password)
        {
            return _db.Users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }

        public ICollection<User> Get()
        {
            return _db.Users.ToList();
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public void SaveOrUpdate(Domain.Entities.User entity)
        {
            if (entity.Id.Equals(0))
                _db.Users.Add(entity);
            else
                _db.Entry<User>(entity).State = EntityState.Modified;

            this.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Users.Remove(_db.Users.Find(id));
            this.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        private void SaveChanges()
        {
            _db.SaveChanges();
        }

    }
}
