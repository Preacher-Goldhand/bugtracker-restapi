# Kanban Task Management Web Application

This is a web-based application designed for managing Kanban boards and the tasks assigned to them. The system allows users to create boards, define columns (such as "To Do," "In Progress," "Completed"), and manage tasks with various statuses, priorities, and assignments.

## Features

- **Create and manage Kanban boards**: Users can create multiple boards and customize them according to their needs.
- **Manage tasks**: Tasks can be added, assigned to users, moved across columns, edited, and deleted.
- **User management**: Users can sign up, log in, and manage their accounts.
- **Task filtering**: Filter tasks based on their status, due date, priority, and assigned user.
- **Real-time updates**: Changes to tasks are updated in real-time for all users viewing the board.
- **Task details**: Each task has detailed information, including description, due date, comments, and attachments.
- **Database integration**: MS SQL Server is used to store all board, task, and user data securely.

## Tech Stack

- **Frontend**: Angular
  - TypeScript, HTML, CSS
  - Angular Material for UI components
- **Backend**: .NET 6
  - ASP.NET Core Web API for the backend services
  - Entity Framework Core for database access
  - JWT Authentication for secure login
- **Database**: MS SQL Server
  - Used for storing user, board, and task data

## Installation

### Prerequisites

- .NET SDK (6)
- Node.js (^15.2.2)
- SQL Server (Express 2019)
- Angular CLI

### Steps to set up the application

1. Clone the repository:
    ```bash
    git clone https://github.com/Preacher-Goldhand/bugtracker-restapi.git
    cd bugtracker-restapi
    ```

2. Set up the backend:
    - Run the migrations to set up the database schema:
        ```bash
        dotnet ef database update
        ```

    - Start the backend server:
        ```bash
        dotnet run
        ```

3. Set up the frontend:
    - Navigate to the frontend folder:
        ```bash
        cd Bugtracker.Client
        ```

    - Install dependencies using npm:
        ```bash
        npm install
        ```

    - Start the frontend server:
        ```bash
        ng serve
        ```

4. Open the browser and navigate to `http://localhost:4200` to use the application.

## Usage

- **Sign up**: Register a new account to start creating boards and tasks.
- **Create a board**: After logging in, you can create a new Kanban board and define columns.
- **Add tasks**: Each board allows you to add tasks, assign users, set priorities, and deadlines.
- **Move tasks**: Drag and drop tasks between columns to reflect their progress.

## Database Schema

![ERDiagram](https://github.com/user-attachments/assets/36d9e2d1-9a4f-4a55-a9aa-ae594e98793d)


## API Endpoints

The API requests are included in **BugTracker.postman_collection.json** file.
  

## UI Views

![login_page](https://github.com/user-attachments/assets/db1e5f59-b703-4b71-aa09-ad7450625a06)

![boards_page](https://github.com/user-attachments/assets/5e8d899c-de8d-49dd-a196-3a5032edc256)

![tasks_page_with_searching](https://github.com/user-attachments/assets/2d1de62c-5a5d-4d87-a573-abc2feb1bbd2)
  

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

