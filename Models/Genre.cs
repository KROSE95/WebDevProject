using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Genre
    {
       public int GenreId { get; set;} //primary key
       public string GenreName {get; set;} //genre name 

       public ICollection<GenreBook> GenreBooks { get; set;} //many to many with books
    }
}