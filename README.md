The application is a comprehensive platform designed to facilitate disaster relief efforts, allowing users to report incidents, donate resources, and volunteer for tasks.

Features Implemented
The application includes the following core features, all built on a secure and robust backend:

User Authentication & Dashboard:

Secure user registration and login system powered by ASP.NET Core Identity.

A personalized user dashboard that provides a summary of the user's contributions, including their recent donations and assigned volunteer tasks.

Clean, minimalist "Apple-inspired" UI for the login and registration pages for a modern user experience.

Disaster Incident Reporting:

Authenticated users can submit detailed reports about disaster incidents, including name, location, date, and description.

A public-facing page lists all reported disasters in a clean, card-based layout.

A detailed view for each disaster shows all associated information, including a list of donations and volunteer tasks linked to that specific event.

Resource Donation Management:

Users can donate resources to specific disaster relief efforts.

The donation form includes a dropdown menu of active disasters to ensure donations are correctly allocated.

Users can view a history of their own past donations on their dashboard.

Volunteer Management:

A dedicated page lists all available volunteer tasks with descriptions and the associated disaster.

Users can sign up for "Open" tasks with a single click.

Once a user signs up, the task status is updated to "In Progress" and assigned to them, preventing others from signing up for the same task.

Technology Stack
Backend: ASP.NET Core MVC (.NET)

Database: Entity Framework Core with SQL Server

Authentication: ASP.NET Core Identity

Frontend: Razor Pages, HTML, Bootstrap 5

Version Control: Git, Azure Repos

CI/CD: Azure Pipelines

Development and Troubleshooting Journey
This project was built iteratively, involving a complete development lifecycle from backend coding to UI implementation and troubleshooting.

1. Backend Scaffolding
The project started by defining the data structure. All C# models (Disaster, Donation, VolunteerTask), the ApplicationDbContext, and the controllers (DisastersController, DonationsController, etc.) were created first to establish the application's core logic.

2. UI Implementation
With the backend logic in place, the focus shifted to the user interface. All necessary .cshtml view files were created to provide a functional frontend for every feature. This included creating forms, data tables, and detailed views.

3. Solving Build & Launch Errors
During the initial launch, we encountered and resolved several common but critical issues:

Build Error - "Unexpected 'foreach' keyword": The initial build failed due to a Razor syntax error in Views/Disasters/Index.cshtml. An extra @ symbol was present inside a C# code block. This was resolved by correcting the syntax, which allowed the project to build successfully.

Launch Error - "Startup project could not be launched": After fixing the build, the project still wouldn't start. We diagnosed this by:

Switching Views: Realizing Visual Studio was in "Folder View" instead of "Solution View". We fixed this by double-clicking the .sln file to load the project correctly.

Correcting the Solution File: After switching views, a new error appeared: "The project file could not be loaded." We inspected the GiftoftheGiversFoundation.sln file and found an incorrect path to the .csproj file (it had a duplicate folder name). We manually edited the .sln file to correct the path, which resolved the loading issue and allowed the project to run.

4. Version Control Setup
The final phase involved setting up version control and CI/CD in Azure DevOps.

Connecting to Azure Repos: We initialized a local Git repository. During the initial push, we discovered the remote origin was incorrectly pointing to a GitHub repository. We resolved this by using the git remote set-url origin command to point to the correct Azure Repos URL, successfully pushing the code.

Branching Strategy: We implemented the Gitflow branching strategy by creating main, develop, and feature/initial-mvp branches to demonstrate best practices.

Azure Pipelines: An automated build pipeline was configured to trigger on pushes to the develop branch, ensuring Continuous Integration.
