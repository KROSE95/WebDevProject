using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Author
    {
        public int AuthorId { get; set;}//primary key
        public string Name { get; set;}

        public ICollection<AuthorBook> AuthorBooks {get; set;}
    }
}