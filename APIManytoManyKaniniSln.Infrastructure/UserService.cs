using APIManytoManyKaniniSln.Application;
using APIManytoManyKaniniSln.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIManytoManyKaniniSln.Infrastructure
{
    public class UserService
    {
        private readonly UserRepository _usrrepo;

        public UserService(UserRepository usrrepo)
        {
            _usrrepo = usrrepo;
        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _usrrepo.GetAll();
        }

        public async Task<User> GetUserbyid(int id)
        {
            return await _usrrepo.GetById(id);
        }

        public async Task<User> AddUser(User usr)
        {
          await _usrrepo.Add(usr);
          return usr;
        }

        public async Task UpdateUser(int id,User ur)
        {
          await _usrrepo.Update(id, ur);
        }

        public async Task<IEnumerable<Post>> GetPostbyuser(string uname)
        {
           return await _usrrepo.GetPostbyuser(uname);
        }
        public async Task<UserPost> AddUserPost(int userid, int postid, bool isauthor)
        {
           return await _usrrepo.AddUserPosts(userid, postid, isauthor);
        }
        public async Task<Post> Addpostwthuser(Post post, string username)
        {
            return await _usrrepo.Addpostwithuser(post, username);
        }
        public async Task<IEnumerable<Post>> GetPosttitle(string title)
        {
            return await _usrrepo.GetPostbyTitle(title);
        }
    }
}
