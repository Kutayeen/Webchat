using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Webchat.Dal;
using Webchat.Model;

namespace Webchat.Bll
{
    public class UserRepository : BaseRepository<User>
    {
        public User Login(string usermail, string password)
        {
            string haspassword = PasswordHash(password);
            return context.User.Where(User => User.UserMail == usermail && User.UserPassword == haspassword).ToList().FirstOrDefault();
        }

        public User Register(string username, string usermail, string userpassword, Byte[] userphoto)
        {
            try
            {
                User User = context.User.Add(new User { IsAdmin = false, UserName = username, UserCreated = DateTime.Now, UserMail = usermail, UserPassword = PasswordHash(userpassword), UserPhoto = userphoto });
                context.SaveChanges();
                return User;
            }
            catch (Exception e)
            {
                return null;
            }

        }


        public List<User> GetByName(string username)
        {
            return context.User.Where(User => User.UserName == username).ToList();
        }

        public User GetByMail(string usermail)
        {
            return context.User.Where(User => User.UserMail == usermail).ToList().FirstOrDefault();
        }

        public string PasswordHash(string newPassword)
        {
            using (var sha1 = new SHA1Managed())
            {
                var hash = Encoding.UTF8.GetBytes(newPassword);
                var generatedHash = sha1.ComputeHash(hash);
                var generatedHashString = Convert.ToBase64String(generatedHash);
                return generatedHashString;
            }
        }
    }
}
