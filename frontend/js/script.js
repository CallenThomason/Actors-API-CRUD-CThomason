const baseUrl = "http://localhost:5192/api/movie";

const statusEl = document.getElementById("status");
const list = document.getElementById("movies");

//Form Elements
const form = document.getElementById("movie-form"); 
const title = document.getElementById("title"); 
const director = document.getElementById("director"); 
const releaseYear = document.getElementById("year"); 
const rating = document.getElementById("rating");

//Arrow functions do not need to be async
//Building an li for each movie on out list
const showMovies = (movies) =>{

    //Clear out the old list first so movies don't show up twice
    list.textContent = ""; 

    //movies is the list
    //movie is the elements inside the list
    movies.forEach((movie) => {
        console.log(movie); 
        const item = document.createElement("li");
        item.classList.add("movie"); 
        const titleHeader = document.createElement("h2"); 
        titleHeader.textContent = `${movie.title} ${movie.releaseYear}`
        const details = document.createElement("p"); 
        details.textContent = `${movie.director}, Rating ${movie.rating}/10`;

        item.appendChild(titleHeader); 
        item.appendChild(details); 

        list.appendChild(item); 
    });
  
}

form.addEventListener("submit", async (event) =>{
    event.preventDefault(); //stops the page from reloading
   
    //This object becomes JSON body. Its shape matches our CreateMovieDto
    const movie = {
    title: title.value, 
    director: director.value, 
    releaseYear: releaseYear.value,
    rating: rating.value
}
   await createMovie(movie); 
   //Empty the form and reload thre list so the new movies show up
   form.reset(); 
  await movieFetch();
})

const movieFetch = async () => {
    // Try / Catch: if anything inside the try fails, we jump to the catch instead of crashing
    try{
         const response = await fetch(baseUrl); 
        //we check response.ok outselves, and through if the status is not 2xx
        if(!response.ok){
            throw new Error(`Request failed: ${response.status}`); 
        }
    const data = await response.json(); 
    console.log(data); 
    //Data is a list of movies
    statusEl.textContent = ""; 
    showMovies(data); 
    } catch (error){
        statusEl.textContent = "Could not load movies. Check the console"
        console.log(error); 
    }

   
} 

movieFetch(); 

// const movie = {
//     title: "2Fast 2Furious", 
//     director: "Callen Thomason", 
//     releaseYear: "2001",
//     rating: 100
// }

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