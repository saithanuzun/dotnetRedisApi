using dotnetRedis.Models;

namespace dotnetRedis.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User? GetUserById(string id);
        IList<User>? GetAllUsers();
        void DeleteUser(string id);

        void UpdateUser(string id,User user);
       
    }
}