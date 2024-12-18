# Lottery - Deloitte Case Study

This project consists of a backend API built with .NET Core and a frontend client developed with Angular. Below are the instructions to start and run both the API and the client.

---

## Overview

The Lottery project is a case study application designed to demonstrate technical and creative skills. It allows users to simulate lottery draws, store the draw history, and choose between client-side or server-side number generation. 

---

## Prerequisites

1. **Backend**:
   - .NET SDK 9.0 or higher.
   - A database connection if required for further development.

2. **Frontend**:
   - Node.js (LTS version recommended).
   - Angular CLI installed globally (`npm install -g @angular/cli`). Before running the application, ensure to install all dependencies by running `npm install` in the Angular client folder.

---

## Starting the API

1. Navigate to the API folder:
   ```bash
   cd Lottery.Presentation.Server.Api
   ```

2. Run the API:
   ```bash
   dotnet run
   ```

3. The API should be available at:
   - **Base URL**: `http://localhost:5249`
   - **Documentation**: `http://localhost:5249/scalar/v1`

4. **Note**: If the API starts on a different port, update the Angular project's API base URL in the following file:
   - `Lottery.Presentation.Client.Angular.Web/src/app/core/services/api.service.ts`

---

## Starting the Client

1. Navigate to the Angular client folder:
   ```bash
   cd Lottery.Presentation.Client.Angular.Web
   ```

2. Start the Angular development server:
   ```bash
   ng serve
   ```

3. The client should be available at:
   - **Base URL**: `http://localhost:4200/`

4. **CORS Configuration**: 
   - If the Angular application runs on a different port, update the allowed origins in the API's `appsettings.json` file under CORS configuration to match the new port.

---

## Technologies Used

- **.NET 9**: Backend API with Entity Framework for database interactions.
- **Angular 17**: Frontend with state management and routing.
- **Tailwind CSS**: Styling.
- **Entity Framework**: DB Connection.
- **MSSQL**
- **Serilog**: Logging for tracking application behavior.
- **Scalar**: API documentation and client generation.

---

## Project Structure

### Project Structure

### Application
- **Services**: Business logic for generating and storing draws.
- **Configuration**

### Domain
- **Models**: Core domain models such as `DrawLog` and `DrawNumber` Entities, DTOs and common models.
- **Specifications**: Encapsulated queries for filtering, sorting and includes.
- **Abstract**: Contracts for repository and service layers.

### Infrastructure
- **Repositories**: Data access layer using Generic Repository pattern.
- **Ef**: Configurations and migrations for the database.

### Presentation
#### Backend (Lottery.Presentation.Server.Api)
- **Controllers**: Handle API requests and responses.
- **Middlewares**: Global exception handling and logging middleware.

#### Frontend (Lottery.Presentation.Client.Angular.Web)
- **Components**: Angular components for UI rendering.
- **Models**
- **Core**: Shared functionality and logics like API configurations and interceptors.

---

## Features

- **Lottery Draw Simulation**: Simulates a lottery draw with numbers generated on either the client or server.
- **History**: View all past draws with filters, sorting, and pagination.
- **Switchable Modes**: Users can toggle between client-side and server-side number generation.

---

## Notes

- Ensure both the backend API and Angular client are running concurrently for the application to work correctly.
- Any modifications to the base URLs must be reflected in the respective projects for proper communication between the frontend and backend.

---

## Troubleshooting

1. **API not reachable**:
   - Ensure the API is running and the base URL is correctly configured in the Angular project.

2. **CORS Issues**:
   - Update the API's `appsettings.json` to include the frontend URL in the allowed origins.

3. **Port Conflicts**:
   - Adjust the ports for either the API or Angular client to resolve conflicts.

---

## Author

**Martin Skořupa**  
- Website: [www.skorupa.dev](https://www.skorupa.dev)  
- LinkedIn: [linkedin.com/in/martinskorupa](https://linkedin.com/in/martin-skorupa)

