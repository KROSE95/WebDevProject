using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Genre
    {
       public int GenreId { get; set;} //primary key
       public string GenreName {get; set;} = string.Empty;//genre name 

       public ICollection<GenreBook> GenreBooks { get; set;} = new List<GenreBook>();//many to many with books
    }
}