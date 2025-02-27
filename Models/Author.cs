using System.Collections.Generic;

namespace StoriesSpain.Models 
{
    public class Author
    {
        public int AuthorId { get; set;}//primary key
        public string AuthorName { get; set;} = string.Empty;

        public ICollection<AuthorBook> AuthorBooks {get; set;} = new List<AuthorBook>();
    //initialises to prevent null reference errors.
    }
}