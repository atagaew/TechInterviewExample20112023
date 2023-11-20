# File Processor Application

## Overview

This C# application is designed as an API to process files containing links. Its primary functionality is to access each link in the file, measure the time taken for access, and generate a report. The application can be run locally with a test file or configured to use a file stored in an Azure Blob.

## Getting Started

### Local Testing

1. Clone the repository to your local machine.
2. Open the solution in your preferred C# IDE.
3. Use the provided test file, `testFile.txt`, for local testing.
4. Run the application.

### Azure Blob Configuration

1. Open the `Program.cs` file.
2. Replace the following line:
   ```csharp
   builder.Services.AddScoped<IApplicationFileProvider, LocalFileProvider>();
   ```
   with
   ```csharp
   builder.Services.AddScoped<IApplicationFileProvider, AzureBlobFileProvider>();
   ```

3. Specify the required configuration settings in the `appsettings.json` file:
   ```json
   {
     "AzureBlobSettings": {
       "ConnectionString": "your_connection_string",
       "ContainerName": "your_container_name",
       "BlobName": "your_blob_name"
     }
   }
   ```

## API Endpoints

### 1. Start File Processing

- **Endpoint:** `POST https://localhost:7158/FileProcessor/testFile.txt`
- **Description:** Initiates the process of checking links in the specified file.
- **Returns:** A GUID representing the process ID.

### 2. Check Process Status

- **Endpoint:** `GET https://localhost:7158/FileProcessor/{processId}`
- **Description:** Retrieves the status of the process identified by the given process ID.
- **Returns:** The status of the process, including the time taken to access each link.

## Test File

A test file, `testFile.txt`, is included in the solution for local testing purposes. Ensure that the file contains valid links.

## Azure Blob Configuration

To use a file located in an Azure Blob, follow the instructions in the "Azure Blob Configuration" section above.

## Dependencies

- .NET Core 3.1 or later

## License

This project is licensed under the [MIT License](LICENSE). Feel free to modify and distribute it as needed.