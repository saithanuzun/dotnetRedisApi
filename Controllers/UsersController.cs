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

   

        [HttpGet("{id}", Name="GetUserById")]
        public ActionResult<User> GetUserById(string id)
        {
            var user= _repo.getUserById(id);

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
    }
}