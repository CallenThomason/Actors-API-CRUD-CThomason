//DTOS are JSON shapes that the client sees. The Movie entity stays inside the API

//

namespace api.Dtos
{

    //No CreatedAt or UpdatedAt: The client only gets the fields we choose to share. 
    public class MovieResponseDto
    {
        public int Id{get; set;}
        public string Title{get; set;} = string.Empty; 
        public string Director{get; set;} = string.Empty; 
        public int ReleaseYear{get; set;}
        public int Rating{get; set;} 

    }
}