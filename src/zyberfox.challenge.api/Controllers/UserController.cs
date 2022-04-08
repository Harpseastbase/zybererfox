using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using zyberfox.challenge.api.Model;

namespace zyberfox.challenge.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        /// <summary>
        ///  Creates a new user
        /// </summary>
        /// <param name="users">User request</param>
        /// <returns> Returns status 200 Code</returns>
        /// <response code="200"> User created </response>
        /// 

        [HttpPost]
        public IActionResult CreateUser(Users users)
        {

            bool result = Users.AddUser(users,"Add");
            
            if (result)
                return StatusCode(200);

            return StatusCode(400);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public List<UserUpdate> GetUsers()
        {
            return UserUpdate
                .GetUsers();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public UserUpdate GetUser(string id)
        {
            return UserUpdate
                .GetUser(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(string id)
        {
            bool result = UserUpdate
                .DeleteUser(id);

            if (result)
                return StatusCode(200);

            return StatusCode(400);

        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutUser(Users getUser)
        {
            bool result = Users.AddUser( getUser,"Update");

            if (result)
                return StatusCode(200);

            return StatusCode(400);

        }
    }
}
