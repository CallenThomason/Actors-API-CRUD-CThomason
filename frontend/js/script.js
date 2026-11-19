const baseUrl = "http://localhost:5192/api/movie";

const movieFetch = async () => {
    const response = await fetch(baseUrl); 
    const data = await response.json(); 
    console.log(data); 

} 

movieFetch(); 

const movie = {
    title: "2Fast 2Furious", 
    director: "Callen Thomason", 
    releaseYear: "2001",
    rating: 100
}

const createMovie = async (newMovie) => {
    const response = await fetch(baseUrl + "/addmovie", {
        method: "POST", 
        //Tell the API that the body is json
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(newMovie) 
    }); 
    const data = await response.json(); 
    console.log(data); 
}

//createMovie(movie);