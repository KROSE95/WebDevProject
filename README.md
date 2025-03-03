Stories of Spain - Book Recommendation API
This is a .NET Core Web API for managing books set in Spain.
It allows users to view books, bookmark them, and manage their reading lists.

Features that have been implemented are;
- User authentication & registration
- Role-based access (Admin can create books, Users can bookmark)
- Token-based authentication (JWT)
- CRUD operations for books, authors, and genres
- Many-to-Many relationships (AuthorBook, GenreBook)
- Secure password hashing and email confirmation

To run the project you must
- Clone the repository
  git clone *url*

- Navigate to the project directory
  cd StoriesSpain

- Restore dependencies
  dotnet restore
  
- Apply migrations
  dotnet ef database update

- Run the API
  dotnet run

  Important endpoints involved POST method
  -/api/account/register   - to register a new user
  -/api/account/login      - to login and get a JWT token
  -/api/books              - to create books (only admin allowed)
  -/api/bookmarks          - to save books (only logged in users allowed)

