# Nmap-Project

Disclaimer: Below dependencies and run guides were completed/tested on a windows machine. I've included steps for macOS as well but they may vary slightly.

## Dependencies

#### .NET/C#
- Visual Studio Code installed (https://code.visualstudio.com/download)
- C# Dev Kit for VS Code (https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit)
- Install .NET 9 SDK. Please install version SDK 9.0.308 and select the installer necessary for you're operating system. (https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

#### MongoDB
- Download mongoDB shell if not already installed (https://www.mongodb.com/try/download/shell)
    - If using windows, extract the files to "C:\Users\\\<user>\AppData\Local\Programs\mongosh" where \<user> is your username and add the directory path where "mongosh.exe" is saved to your PATH environment variable. This will be within the bin folder of the files you just extracted. If using macOS, you will need to extract the files from the zip file to a directory of your choice for macOS and add the directory path for "mongosh" to your PATH environment variable.
- Download MongoDB community server for the platform of your choice (https://www.mongodb.com/try/download/community).
    - If using windows, verify MongoDB was installed at "C:\Program Files\MongoDB by default. Add "C:\Program Files\MongoDB\Server\\\<version_number>\bin" where \<version_number> is your version number and add the directory path to your PATH environment variable. On macOS, verify MongoDB was installed at "/usr/local/mongodb" and add the directory path to your PATH environment variable.

 1. Once MongoDB and it's shell are downloaded, please create a directory on your machine to store data. Open a powershell/terminal instance and run the following command replacing "path_to_your_directory" with your actual directory path after creation, ```mongod --dbpath 'path_to_your_directory'```
 2. Run the mongoDB shell command ```mongosh```
 3. You should now be within the mongoDB shell executable "mongosh". Run the command ```use Nmap```. This will create a new empty database within your locally running mongo server named "Nmap".
 4. Next run the ```db.createCollection("NmapData")``` command to create an empty collection for document storage.
 5. You can now exit the mongosh executable with the ```exit``` command.


#### Nmap
- If not already installed, download the Nmap tool for  (https://nmap.org/download). Ensure the path to nmap.exe is added to your environment PATH variable.


NOTE: If you install the .NET SDK after installing VS code and the C# dev kit, then please close and reopen VS code after installing the SDK.

### Running Locally

Please ensure you have downloaded and installed all of the above dependencies.

1. Open Visual Studio Code as an admin
2. Open the "NmapApi" folder within VS code. This will be one level below the root of the project (...\Nmap-Project\NmapApi).
3. Open an integrated terminal window in VS Code and run ```dotnet dev-certs https --trust``` to trust the dev certificate. Select "Yes" when prompted. 
4. Run ```dotnet run --launch-profile https``` from your integrated terminal. This will build and launch the API.

Note: If the defined application ports (https: 7178 and http: 5281) are already in use on your machine, you can update them to different available ports within the "launchSettings.json" file located here "...\Nmap-Project\NmapApi\Properties".

5. Navigate to the application URL "https://localhost:7178/swagger". Swagger is included with the application to define the API routes and give you a GUI to interact with the API. Expand the "Nmap" title to see the various routes. You can also directly interact with the endpoints using any tool of your choosing with the following URL prefix "https://localhost:7178/api/Nmap/" and append the API route name/data to that URL. I would recommend using swagger.
6. Once finished running the API locally, press "Ctrl + C" within your VS code terminal to terminate the running application.


## Testing

This is a subset of possible testing that can and should be done in a real environment.

### Unit Testing

#### NmapProcessingTasks.cs

Note: We will be mocking out the NmapService and would look to do the same with our helper classes. This is so that we will be able to fully control the exact outputs we get from these outside resources.

##### SendNmapRequest

- Pass hostName containing spaces and assert that we receive an error.
- Pass invalid hostName and assert that we receive an error.
- Pass various flagged values and different types of formatted strings and assert that we receive errors for all cases.
- Pass in a valid hostName that returns ports and assert that we have a successful response from the API and that all expected ports are returned in our response.
- Pass in a valid hostName that returns ports and assert that we get a successful response along with the correct IP address
- Perform various combinations of the above validity tests, asserting that any combination of valid hostName's is returning expected ports and IP addresses.

##### SendNmapRequestWithReport

- Pass hostName containing spaces and assert that we receive an error message.
- Pass hostName that hasn't been scanned before and assert we get an error message.
- Pass hostName that gives us an error and assert that error is in the response.
- Pass hostName that gives us many different combinations of newly opened ports compared to the most recent scan for that hostName. Assert that all newly opened ports are returned in the response.
- Pass hostName that gives us many combinations of newly closed ports compared to the most recent scan. Assert that all of the newly closed ports are returned in the response.
 - Pass hostName that gives us a valid time span between scans and assert that the response displays the correct time difference.

 ##### GetAllNmapResults

- Begin test by forcing many various types of exceptions to be thrown. Assert that a non-successful response with an error message is returned to the user.
- Begin test by returning many different combinations of results to be returned. Assert that the response is successful and all expected results are included in the response.

##### GetMostRecentNmapResult

- Begin test by forcing many exceptions to be thrown, asserting that non-successful response are returned.
- Begin test by returning many types of successful responses with all expected recent scans for the specified hostName returned.

#### NmapHelper.cs

##### RunNmapProcess

- Pass various valid targetIp strings which we expect to return valid nmap results. Assert that all expected results are returned as a list.
- Pass various invalid targetIp strings and assert that we get no results/empty list returned in all of these cases.
- Expand upon testing and error handling for possible exceptions. Asserting that exceptions will get caught and bubble up within our application.

#### XmlParser.cs

##### ParseXmlString

 - Pass many different combinations of xmlString asserting that all values such as our IpAddress, HostName, ScanCompletedAt, and Port object properties are successfully built out. Assert that a list of NmapResults is returned with expected values.
 - Pass different combinations of xmlString that do not contain element values such as address, host, port, etc. and assert that we do not return a list of Nmap results.
 - Expand upon testing and error handling for possible exceptions. Asserting that exceptions will get caught and bubble up within our application.

### Integration Testing

For our integration testing, we want to actually test our API with controlled versions of all our components/services. In this case we want to test our API will work in it's communication to our mongoDB server along with nmap. We must build out a way to stand up a fully controlled environment every time we want to do this.

Firstly, we'll want to create a new testing project within our web api solution. We can create and define tests that do not mock out our services like we did for the unit tests. Our test cases can be similar to the unit tests but instead of mocking the services we're actually going to be connecting to and using a fully-controlled version of mongoDB. We will standup this mongoDB testing container each time we run our integration testing suite.

To handle the setup of the mongoDB container, we can create a script to pull down and run a local container using mongoDB. This will assume we have docker/Docker Desktop installed locally. When running our test suite, all configuration and containers will be scripted out. They will be built up and torn down upon start/end of the testing run.


### Load Testing

Testing the load we can handle is also an important step in our process to ensure we have a system that can scale. My proposal for this solution is to create a load testing suite using NBomber. This will allow us to stay within the same language being used for the API itself and NBomber is a great lightweight framework for getting this done.

Similar to our integration tests, we can build out scripts to host local containers running our mongoDB server and our Nmap API. We can then configure our load testing suite to hit those instances to get a baseline for performance against our API fixing any glaring issues right away. From the data obtained, we'll extrapolate our system performance from this small contained performance test. Once we're ready, we can then use our NBomber testing suite against a more production like environment (thinking of a dev or staging environment). This should give us insight into our API's performance and what we'd need to change.