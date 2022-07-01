using LibraryProject.Controllers.Requests;
using LibraryProject.Data;
using LibraryProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        private ILibraryDataStore _libraryDataStore;

        public LibraryController(ILibraryDataStore libraryDataStore)
        {
            _libraryDataStore = libraryDataStore;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> User([FromBody] UpdateUsersRequest request)
        {
            var user = new User()
            {
                Id = request.Id,
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age
            };
            
            await _libraryDataStore.Update(user);

            return new ObjectResult(new {message = "user_updated"}) {StatusCode = 200};
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(User))]
        public async Task<IActionResult> User([FromQuery]long userId)
        {
            var user = await _libraryDataStore.GetUser(userId);

            return Ok(user);
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> User([FromBody] DeleteUserRequest request)
        {
            await _libraryDataStore.Delete(request.userId);

            return new ObjectResult(new {message = "user_deleted"}) {StatusCode = 200};
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> User([FromBody]CreateUsersRequest request)
        {
            var user = new User()
            {
                Id = new Guid(),
                Name = request.Name,
                Surname = request.Surname,
                Age = request.Age,
                UserId = request.UserId
            };
            
           await _libraryDataStore.Insert(user);

           return new ObjectResult(new {message = "user_added"}) {StatusCode = 201};
        }
    }
}