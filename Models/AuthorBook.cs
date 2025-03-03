using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace StoriesSpain.Models 
{
    public class AuthorBook
    {
        public int BookId { get; set;}
        
        public int AuthorId { get; set;}

        public Book ? Book { get; set;} // navigation property to access full book details

        public Author ? Author { get; set;}
        //ensures every entry in authorbook has a book and author associated.
    }
}