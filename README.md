# MyRepos

Dev Tech Test HelloBuild

# Deliverables
ASP.NET Core Web Application:
   - Home Page
   - Register
   - Login
   - Profile Management
   - Logout
   - Github Connection (OAuth)
   - List of Repositories
   - Favorites (Starred repos)
   - Star repo action
   - Unstar repo action

# Configuration
1. Requisites:
   - .NET SDK 7.0 or later.
   - Visual Studio 14.0 or later.
2. Download or clone the project.
3. Change connection string from appsettings.json.
   ```json
   "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Trusted_Connection=True;MultipleActiveResultSets=true"
   },
   ```
5. Apply migrations in package manager console: (Make sure you selected MyRepos.Presentation project)
   ```bash
   update-database
   ```
   Note: If it doesn't work you can delete all migrations in migrations folder and create a new migration:
   ```bash
   add-migration initial_migration
   ```
   then update database.
4. Set MyRepos.Presentation as start project
5. Run the project

  
