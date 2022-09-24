# TEPSClientInstallService
Agent service that does the actual software management on a end users machine


## Testing the API 
- [Download Postman](https://www.postman.com/) or [insomnia](https://insomnia.rest/)
- Download the repo locally 
- build the application at least once
- open up a command prompt terminal in admin mode
- paste cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 in and press enter
- navigate to the debug (or release) folder of repo (mine is Z:\Share\VS2017\work-Colaboration\TEPSClientInstallService\bin\Debug)
- type in installutil.exe <PATH TO SERVICE EXECUTABLE IN YOUR DEBUG OR RELEASE DIR>\TEPSClientInstallService.exe
- open services on your local machine and start TEPS Automated Client Install Service
- interact with API via api interaction tool
  - endpoint should be http:\\<machineName>:8080
  - logging should be C:\ProgramData\New World Systems\IMS\Tyler Client Install Service\TEPS Automated Client Install Service <release number>.json
  - you will need the TEPS client installers in C:\ProgramData\New World Systems\IMS\Tyler Client Install Service\Clients
  - you will need the TEPS client pre reqs in C:\ProgramData\New World Systems\IMS\Tyler Client Install Service\PreReqs
   - TEPS testing pre reqs https://tylersftp.tylertech.com/w/f-9141c57b-f4b0-4d7f-b76d-40d62769c99d
  
