# Nmap-Project



### Reqs
- Visual Studio Code installed (https://code.visualstudio.com/download)
- C# Dev Kit for VS Code (https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- Install .NET 9 SDK. Please install version SDK 9.0.308 and select the installer necessary for you're operating system. (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

#### MongoDB
- Download mongoDB shell if not already installed (https://www.mongodb.com/try/download/shell)
    - You will need to extract the files from the zip file to a directory of your choice for macOS and add the directory path for "mongosh" to your PATH environment variable. If using windows, extract the files to "C:\Users\<user>\AppData\Local\Programs\mongosh" and add the directory path for "mongosh.exe" to your PATH environment variable.
- Download MongoDB community server for the platform of your choice (https://www.mongodb.com/try/download/community).
    - On macOS, verify MongoDB was installed at "/usr/local/mongodb" and add the directory path to your PATH environment variable. If using windows, verify MongoDB was installed at "C:\Program Files\MongoDB by default. Add C:\Program Files\MongoDB\Server\<version_number>\bin" and add the directory path to your PATH environment variable.

- Once MongoDB is downloaded, please create a directory on your machine to store data. Run the following command after creating your directory, "mongod --dbpath 'path_to_your_directory'"


#### Nmap
- If not already installed, download the Nmap tool for  (https://nmap.org/download)




NOTE: If you install the .NET SDK after installing VS code and the C# dev kit, then please close and reopen VS code after installing the SDK.


1. dotnet dev-certs https --trust (select yes)
2. 