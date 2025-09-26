using APIManytoManyKaniniSln.Application;
using APIManytoManyKaniniSln.Data;
using APIManytoManyKaniniSln.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIManytoManyKaniniSln.Infrastructure
{
   //Interface - Repository - Service - Controller
    public class UserRepository : IUserPost<User, int>,IUser
    {
        private readonly UserPostContext _context;

        public UserRepository(UserPostContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            await _context.users.AddAsync(entity);
           await _context.SaveChangesAsync();
            return entity;

        }       

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.users.Include(u => u.userposts!).ThenInclude(up => up.post).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.users.FirstOrDefaultAsync(x => x.UsrId == id) ?? throw new KeyNotFoundException("Userid not found");
        }
         

        public async Task Update(int id, User entity)
        {
           if(id!=entity.UsrId)
            {
                throw new KeyNotFoundException("Record doesnot exist");
            }
           _context.users.Update(entity);
           await _context.SaveChangesAsync();
           
        }

        public async Task<IEnumerable<Post>> GetPostbyuser(string uname)
        {
            var query =  from u in _context.users
                         where u.UserName == uname
                         join up in _context.Userposts on u.UsrId equals up.UserId
                         join p in _context.Posts on up.PostId equals p.PostId
                         select p;

            return await query.Include(p=>p.Userposts).ToListAsync();
        }
        
        public async Task<UserPost> AddUserPosts(int userid, int postid, bool isauthor)
        {
            var userExists = await _context.users.AnyAsync(u => u.UsrId == userid);
            var postExists = await _context.Posts.AnyAsync(p => p.PostId == postid);
          
            if (!userExists || !postExists)
                throw new KeyNotFoundException("User or Post not found");

            var existing = await _context.Userposts.FindAsync(userid, postid);
            if (existing is not null)
            {
                existing.IsAuthor = isauthor;
                await _context.SaveChangesAsync();
                return existing;
            }
            var link = new UserPost
            {
                UserId = userid,
                PostId = postid,
                IsAuthor = isauthor
            };
            _context.Userposts.Add(link);
            await _context.SaveChangesAsync();
            return link;
        }
        public async Task<IEnumerable<Post>> GetPostbyTitle(string title)
        {
           return await _context.Posts.Where(x=>x.Title.ToLower().Contains(title.ToLower())).ToListAsync();
        }
        public async Task<Post> Addpostwithuser(Post post,string username)
        {
          User user = await _context.users.FirstOrDefaultAsync(u=>u.UserName==username) ?? throw new KeyNotFoundException("Username doesnot exist");
          UserPost up = new UserPost()
          {
                UserId = user.UsrId,
                PostId = post.PostId,
                IsAuthor = true
          };
          _context.Posts.Add(post);
          _context.SaveChanges();
          return post;
        }
    }
}
