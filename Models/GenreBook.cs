using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class GenreBook{
        public int BookId { get; set;}
        public int GenreId {get; set ;}
        
        public required Book Book { get; set;}
        public required Genre Genre { get; set;}
        //non-nullable properties. When a book is created it should have a genre. And 
        //other details filled.
    }
}
