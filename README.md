# Nmap-Project

Disclaimer: Below dependencies and run guides were completed/tested on a windows machine. I've included steps for macOS as well but they may vary slightly.

### Dependencies
- Visual Studio Code installed (https://code.visualstudio.com/download)
- C# Dev Kit for VS Code (https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- Install .NET 9 SDK. Please install version SDK 9.0.308 and select the installer necessary for you're operating system. (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

#### MongoDB
- Download mongoDB shell if not already installed (https://www.mongodb.com/try/download/shell)
    - If using windows, extract the files to "C:\Users\<user>\AppData\Local\Programs\mongosh" and add the directory path for "mongosh.exe" to your PATH environment variable. If using macOS, you will need to extract the files from the zip file to a directory of your choice for macOS and add the directory path for "mongosh" to your PATH environment variable.
- Download MongoDB community server for the platform of your choice (https://www.mongodb.com/try/download/community).
    - If using windows, verify MongoDB was installed at "C:\Program Files\MongoDB by default. Add C:\Program Files\MongoDB\Server\<version_number>\bin" and add the directory path to your PATH environment variable. On macOS, verify MongoDB was installed at "/usr/local/mongodb" and add the directory path to your PATH environment variable.

 1. Once MongoDB is downloaded, please create a directory on your machine to store data. Open a powershell/terminal instance and run the following command replacing "path_to_your_directory" with your actual directory path after creation, ```mongod --dbpath 'path_to_your_directory'```
 2. Run the mongoDB shell command ```mongosh```
 3. You should now be within the mongoDB shell executable "mongosh". Run the command ```use Nmap```. This will create a new empty database within your locally running mongo server name "Nmap".
 4. Next run the ```db.createCollection("NmapData")``` command to create an empty collection for document storage.
 5. You can now exit the mongosh executable with the ```exit``` command.


#### Nmap
- If not already installed, download the Nmap tool for  (https://nmap.org/download). Ensure the nmap.exe is added to your environment PATH variable.


NOTE: If you install the .NET SDK after installing VS code and the C# dev kit, then please close and reopen VS code after installing the SDK.

#### Running Locally

Please ensure you have downloaded and installed all of the above dependencies.

1. Open Visual Studio Code as an admin
2. Open the "NmapApi" folder within VS code. This will be one level below the root of the project (...\Nmap-Project\NmapApi).
3. Open an integrated terminal window in VS Code and run ```dotnet dev-certs https --trust``` to trust the dev certificate. Select "Yes" when prompted. 
4. Run ```dotnet run --launch-profile https``` from your integrated terminal. This will build and launch the API.

Note: If the defined application ports (https: 7178 and http: 5281) are already in use on your machine, you can update them to different available ports within the "launchSettings.json" file located here "...\Nmap-Project\NmapApi\Properties".

5. Navigate to the application URL "https://localhost:7178/swagger". Swagger is included with the application to define the API routes and give you a GUI to interact with the API. Expand the "Nmap" title to see the various routes. You can also directly interact with the endpoints using any tool of your choosing with the following URL prefix "https://localhost:7178/api/Nmap/" and append the API route name/data to that URL. I would recommend using swagger.
6. Once finished running the API locally, press "Ctrl + C" within your VS code terminal to terminate the running application.
