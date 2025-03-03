using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace StoriesSpain.Models 
{
    public class ApplicationUser : IdentityUser
    {
       
      

       public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>(); //User's Saved books 
    }
}