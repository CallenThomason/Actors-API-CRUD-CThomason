
//The Movie entity: One Movie Object is one row in the movies table
//It inherits Id, CreatedAt and Updated At from BaseEntity
namespace api.Models
{
    public class Movie : BaseEntity
    {
        //string.Empty gives a starting value so the text is never null
        public string Title {get; set;} = string.Empty; 
        public string Director{get;set;} = string.Empty; 
        public int ReleaseYear{get; set;}
        public int Rating{get; set;}
    }
}