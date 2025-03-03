using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class GenreBook{
        public int BookId { get; set;}
        public int GenreId {get; set ;}
        
        public Book? Book { get; set;}
        public Genre? Genre { get; set;}
        
    }
}
