using System.Linq;
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

            db.HashSet("hashUser", new HashEntry[] {new HashEntry(user.Id,serialUser)});
        }
        public void DeleteUser(string id)
        {
            _redis.GetDatabase().HashDelete("hashUser",id);
        }
        public IList<User?>? GetAllUsers()
        {

            var db = _redis.GetDatabase();
            var completeSet = db.HashGetAll("hashUser");

            if (completeSet.Length > 0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<User>(val.Value)).ToList();
                
                return obj;   
            }
             return null;
            
        }

        public User? GetUserById(string id)
        {
            var db =_redis.GetDatabase();
            var user = db.HashGet("hashUser",id);
            if(!string.IsNullOrEmpty(user))
            {
                return JsonSerializer.Deserialize<User>(user);
            }
            return null;
        }

        public void UpdateUser(string id,User user)
        {
            if(user==null)
            {
                throw new ArgumentOutOfRangeException(nameof(user));
            }
            if(id!=user.Id)
            {
                return;
            }
            var db =_redis.GetDatabase();
            var json= JsonSerializer.Serialize(user);
            db.HashSet("hashUser", new HashEntry[] {new HashEntry(user.Id,json)});

        }

       
    }
}