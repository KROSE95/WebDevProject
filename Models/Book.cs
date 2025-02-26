using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Book
    {
        public int BookId { get; set;} //primary key
        public string Title { get; set;} //book title
        public string Country { get; set;}//do i need if its only spain?
        public string City { get; set;}//specific city
        public int YearPublished { get; set;}
        public string Description { get; set;}//short synopsis
        public string CoverImageURL { get; set;} //url to a book cover

        //relationships to other models
        public ICollection<AuthorBook> AuthorBooks {get; set;}//many to many with authors
        public ICollection<GenreBook> GenreBooks {get;set;} //many to many with genres

        
    }
}