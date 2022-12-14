using dotnetRedis.Models;

namespace dotnetRedis.Data
{
    public interface IUserRepo
    {
        void CreateUser(User user);
        User? getUserById(string id);
        IList<User> getAllUsers();

    }
}