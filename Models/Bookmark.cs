using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Bookmark 
    {
        public int BookmarkId { get; set;}//primary key
        public int UserId { get; set;}//Foreign key to user
        public int BookId { get; set;}//foreign key to book

        public string Status { get; set;} // "ToBeRead" or "Finished"
        public User User { get; set;}
        public Book Book { get; set;}
    }
}