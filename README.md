# Weather Alert Application

A C# console application that monitors weather data and sends email notifications based on specific conditions.

## Prerequisites

Before running this project, ensure you have the following installed:
* **[.NET SDK](https://dotnet.microsoft.com/download)** (Version 6.0 or later recommended)
* An active **Gmail account** for sending alerts.

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/A-Chughtai/Rain-Alert-Mail.git
cd weather-alert
```

### 2. Install Dependencies

Run the following command to restore the required NuGet packages (like DotNetEnv and Newtonsoft.Json):

```bash
dotnet restore
```

### 3. Configuration

This application requires an environment file to handle email credentials securely.

1. Create a file named .env in the root directory of the project (the same folder as Program.cs).
2. Paste the following text into the .env file:
    ```
    GMAIL=
    GMAIL_USER=
    GMAIL_APP_PASSWORD=
    ```
3. Fill in the values:
    - **GMAIL:** The gmail address where you want to receive the alerts.
    - **GMAIL_USER:** The gmail user name.
    - **GMAIL_APP_PASSWORD:** Your 16-character Google App Password (required for security).
        - *Note: Do not use your regular Gmail password. You must generate an App Password in your Google Account Security settings.*
    
### 4. Run the Application

```bash
dotnet run
```

## Project Structure

* **Program.cs**: The entry point of the application. Contains the main logic for fetching weather data and triggering alerts.
* **HourlyData.cs**: Defines the structure for the hourly weather data points.
* **WeatherResponse.cs**: Represents the overall response object returned by the Weather API.
* **Weather Alert.csproj**: The project file that lists dependencies (NuGet packages) and build settings.
* **.env**: A local configuration file for storing sensitive credentials (excluded from version control).