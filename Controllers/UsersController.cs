using dotnetRedis.Data;
using dotnetRedis.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnetRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepo _repo;

        public UsersController(IUserRepo repo)
        {
            _repo =repo;
        }

        [HttpDelete("{id}")]
        public ActionResult<List<User>> DeleteUser(string id)
        {
            var users = _repo.GetAllUsers();
            var user=users.FirstOrDefault(user=> user.Id==id);
            if(user==null)
            {
               return NotFound();
            }
            _repo.DeleteUser(id);
            return Ok(_repo.GetAllUsers());
        }

        [HttpGet]
        public ActionResult<User> GetAllUsers()
        {
            var users= _repo.GetAllUsers(); 

            if(users!=null)
            {
                return Ok(users);
            }
            return NotFound();
        }


        [HttpGet("{id}", Name="GetUserById")]
        public ActionResult<User> GetUserById(string id)
        {
            var user= _repo.GetUserById(id);

            if(user!=null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _repo.CreateUser(user);

            return CreatedAtRoute(nameof(GetUserById),new {Id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(string id, User user)
        {
            if(id!=user.Id)
            {
                return NotFound();
            }

            _repo.UpdateUser(id,user);

            return CreatedAtRoute(nameof(GetUserById),new {Id = user.Id}, user);


        }
    }
}