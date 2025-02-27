using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Book
    {
        public int BookId { get; set;} //primary key
        public string Title { get; set;} = string.Empty;//book title
       // public string Country { get; set;} // keeping if I wanted to expand.
        public string City { get; set;} = string.Empty;//specific city
        public int YearPublished { get; set;}
        public string Description { get; set;} = string.Empty;//short synopsis
        public string? CoverImageURL { get; set;} //url to a book cover

        //relationships to other models
        public ICollection<AuthorBook> AuthorBooks {get; set;} = new List<AuthorBook>();//many to many with authors
        public ICollection<GenreBook> GenreBooks {get;set;} = new List<GenreBook>();//many to many with genres
    //initialisation added as both lists will be non-nullable.
    //=string.empty added to attributes that were non-nullable properties.    
    }
}