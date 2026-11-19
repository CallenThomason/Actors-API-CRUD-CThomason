# Callen Thomason
- 11/19/2026
- Actor API CRUD
- I refreshed my knowledge from the combine by creating the basic crud functions starting from a repository, making my way all the way back to the controller. I made sure each endpoint worked in postman and the right status codes where called. After getting all the to work I went back and ensure the user could not add the same actor more than once to the database. I even went as far as to update the Update endpoint to now allow the user to update an actor to a name that already exists. The framework was already in place, so I decided to include it. For the second part of the assignment I went through and edited each layer to include async and await. 
- Peer Reviewer: Zionn Showers
- Review: There seems to be a bug when creating or updating an id, with an error message popping up saying that a name exists, even though it doesn't. The name and new id still get added though. Everthing else seems to work fine!