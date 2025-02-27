using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace StoriesSpain.Models 
{
    public class ApplicationUser : IdentityUser
    {
       //public int UserId { get; set;}//primary key
       //public string Username { get; set;}//unique username
       //public string Email { get; set;}
       //public string PasswordHash { get; set;} these are not needed as it inherits from identityuser

       public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>(); //User's Saved books 
    }
}