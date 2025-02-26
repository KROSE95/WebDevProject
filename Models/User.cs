using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class User
    {
       public int UserId { get; set;}//primary key
       public string Username { get; set;}//unique username
       public string Email { get; set;}
       public string PasswordHash { get; set;}

       public ICollection<Bookmark> Bookmarks { get; set;} //User's Saved books 
    }
}