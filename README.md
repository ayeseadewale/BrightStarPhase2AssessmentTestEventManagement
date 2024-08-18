Steps to Run the Project
Clone the Repository:
git clone https://github.com/ayeseadewale/BrightStarPhase2AssessmentTestEventManagement.git
cd BrightStarPhase2AssessmentTestEventManagement

Set Up the Database:
Open the appsettings.json file and update the connection string to point to your SQL Server instance.
JSON

"ConnectionStrings": {
  "DefaultConnection": "Data Source=Adewale\\sqlexpress;Initial Catalog=EventManagerDb;Integrated Security=True;Pooling=False; TrustServerCertificate=True"
}
Apply Migrations:
Open a terminal in the project directory and run the following commands to apply the migrations and create the database:
dotnet ef migrations add InitialCreate
dotnet ef database update

Run the Project:
You can run the project using Visual Studio by pressing F5 or by using the terminal:
dotnet run

Access the Application:
Open your web browser and navigate to https://localhost:5001 (or the URL specified in your launch settings).
Additional Notes
User Roles: By default, the application does not create any roles. You will need to manually create roles (Admin, Organizer, Participant) and assign them to users.
Email Notifications: Configure the email settings in appsettings.json to enable email notifications.
API Endpoints: The API endpoints for events can be accessed at https://localhost:5001/api/EventApi.
Example appsettings.json
JSON

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "Key": "YourSecretKeyHere",
    "Issuer": "YourIssuerHere"
  },
  "Email": {
    "SmtpServer": "smtp.your-email-provider.com",
    "Port": "587",
    "Username": "your-email@example.com",
    "Password": "your-email-password",
    "From": "no-reply@example.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
