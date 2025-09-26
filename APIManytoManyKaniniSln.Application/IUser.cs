using APIManytoManyKaniniSln.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIManytoManyKaniniSln.Application
{
    public interface IUser
    {
        Task<IEnumerable<Post>> GetPostbyTitle(string title);
        Task<IEnumerable<Post>> GetPostbyuser(string uname);    
        Task<Post> Addpostwithuser(Post post,string uname);
        Task<UserPost> AddUserPosts(int userid,int postid,bool isauthor);
       
    }
}
