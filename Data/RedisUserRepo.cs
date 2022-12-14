using System.Text.Json;
using dotnetRedis.Models;
using StackExchange.Redis;

namespace dotnetRedis.Data
{
    public class RedisUserRepo : IUserRepo
    {
        private IConnectionMultiplexer _redis;

        public RedisUserRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreateUser(User user)
        {
            if(user==null)
            {
                throw new ArgumentOutOfRangeException(nameof(user));
            }
            var db=_redis.GetDatabase();

            var serialUser = JsonSerializer.Serialize(user);

            db.StringSet(user.Id,serialUser);
        }

        public IList<User> getAllUsers()
        {
            throw new NotImplementedException();
        }

        public User? getUserById(string id)
        {
            var db =_redis.GetDatabase();
            var user=db.StringGet(id);
            if(!string.IsNullOrEmpty(user))
            {
                return JsonSerializer.Deserialize<User>(user);
            }
            return null;
        }
    }
}