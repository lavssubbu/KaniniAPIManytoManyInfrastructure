using APIManytoManyKaniniSln.Domain;
using APIManytoManyKaniniSln.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIManytoManyKanini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _servce;

        public UserController(UserService servce)
        {
            _servce = servce;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _servce.GetAllUser());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return Ok(await _servce.GetUserbyid(id));
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User u)
        {
            return Ok(await _servce.AddUser(u));
        }
        [HttpPut]
        public async Task<ActionResult> PutUser(int id, User u)
        {
            await _servce.UpdateUser(id, u);
            return NoContent();
        }
        [HttpGet("by-name/{title}")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByTitke(string title)
        {
            return Ok(await _servce.GetPosttitle(title));
        }

        [HttpGet("by-name/{userName}/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByUserName(string userName)
        {
            var posts = await _servce.GetPostbyuser(userName);
            return posts.Any() ? Ok(posts) : NoContent();
        }

        [HttpPost("{userId:int}/posts/{postId:int}")]
        public async Task<ActionResult<UserPost>> AddUserPst(int userId, int postId, [FromQuery] bool isAuthor = false)
        {
            var link = await _servce.AddUserPost(userId, postId, isAuthor);
            return Created(string.Empty, link);
        }
        [HttpPost("posts/{username}")]
        public async Task<ActionResult<Post>> Addpostwithuser([FromBody] Post post,[FromRoute] string username)
        {
            await _servce.Addpostwthuser(post, username);
            return Ok(post);
        }
    }
}
