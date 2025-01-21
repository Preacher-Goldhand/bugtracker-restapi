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
- **Backend**: .NET 6/7
  - ASP.NET Core Web API for the backend services
  - Entity Framework Core for database access
  - JWT Authentication for secure login
- **Database**: MS SQL Server
  - Used for storing user, board, and task data

## Installation

### Prerequisites

- .NET SDK (6/7)
- Node.js
- SQL Server (local or remote)
- Angular CLI

### Steps to set up the application

1. Clone the repository:
    ```bash
    git clone <repository_url>
    cd kanban-task-manager
    ```

2. Set up the backend:
    - Navigate to the backend folder and restore dependencies:
        ```bash
        cd KanbanApi
        dotnet restore
        ```

    - Configure your SQL Server connection string in the `appsettings.json` file:
        ```json
        "ConnectionStrings": {
            "DefaultConnection": "Server=your_server;Database=KanbanDB;User Id=your_user;Password=your_password;"
        }
        ```

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
        cd frontend
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

The database consists of the following main tables:

1. **Users**: Stores user information (id, username, password hash, etc.)
2. **Boards**: Stores board details (id, name, user_id, etc.)
3. **Columns**: Stores the columns for each board (id, board_id, name)
4. **Tasks**: Stores task details (id, board_id, column_id, title, description, assigned_user_id, due_date, etc.)
5. **TaskComments**: Stores comments on tasks (id, task_id, user_id, content, date)

## API Endpoints

The API exposes the following endpoints:

- `GET /api/boards`: Retrieve all boards
- `POST /api/boards`: Create a new board
- `GET /api/boards/{id}/tasks`: Retrieve all tasks for a board
- `POST /api/boards/{id}/tasks`: Create a new task for a board
- `PUT /api/tasks/{id}`: Update task details (move task between columns, change priority, etc.)
- `DELETE /api/tasks/{id}`: Delete a task

## Contributing

If you would like to contribute to this project, please follow these steps:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Make your changes
4. Commit your changes (`git commit -am 'Add new feature'`)
5. Push to the branch (`git push origin feature/your-feature`)
6. Create a new pull request

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

