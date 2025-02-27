using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Bookmark 
    {
        public int BookmarkId { get; set;}//primary key
        public string UserId { get; set;} = string.Empty;//Foreign key to user (changed to string as identityuser uses string based ID)
        public int BookId { get; set;}//foreign key to book

        public string? Status { get; set;} // "ToBeRead" or "Finished"
        //set to nullable as might not be set immediately
        public required ApplicationUser User { get; set;} //adjusted to applicationuser type
        // a user is always required to create a bookmark.
        public required Book Book { get; set;}//a book is required to create a bookmark.
    }
}