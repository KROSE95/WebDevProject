using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class AuthorBook
    {
        public int BookId { get; set;}
        
        public int AuthorId { get; set;}

        public Book Book { get; set;} // navigation property to access full book details

        public Author Author { get; set;}
    }
}