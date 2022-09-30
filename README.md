# TEPS Client Install Agent
Agent service that does the actual software management on a end users machine


## Testing the API 
- [Download Postman](https://www.postman.com/) or [insomnia](https://insomnia.rest/)
- follow install directions (if running for the first time)
- interact with API via api interaction tool
  - check logging for end point and port
  - logging should be C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\TEPS Automated Client Install Agent (release number).json
     - EX C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\TEPS Automated Client Install Agent 1.22.9.3.json
  - you will need the TEPS client installers in C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\Clients
  - you will need the TEPS client pre reqs in C:\ProgramData\Tyler Technologies\Public Safety\Tyler-Client-Install-Agent\PreReqs
   - TEPS testing pre reqs https://tylersftp.tylertech.com/w/f-9141c57b-f4b0-4d7f-b76d-40d62769c99d
   - Below is the recommended JSON test package (Feel free to chage the values themselves to whatever you prefer)
   - {
    "ESSServer" : "TEST1",
    "MSPServer" : "TEST2",
    "CADServer" : "TEST3",
    "GISServer" : "TEST4",
    "GISInstance" : "TEST5",
    "MobileServer" : "Test6",
    "PoliceList": [
    {
      "FieldName": "ORI1",
      "ORI": "TX1234567"
    },
    {
      "FieldName": "ORI2",
      "ORI": "TX0987654"
    }
  ],
  "FireList": [
    {
      "FieldName": "FDID1",
      "FDID": "12345"
    },
    {
      "FieldName": "FDID2",
      "FDID": "09876"
    }
  ]
}
  
## Install the service
- download the tool from the most recent official release [of the agent updating tool](https://github.com/davasorus/TEPSClientInstallService-UpdateUtility/tags)
  
## Uninstall the service
-  open up a command prompt terminal in admin mode
- paste cd C:\Windows\Microsoft.NET\Framework\v4.0.30319 in and press enter
- navigate to the folder that the service is running out of
  - EX C:\Services\Tyler-Client-Install-Agent
- type in installutil.exe -u (PATH TO SERVICE EXECUTABLE IN YOUR DEBUG OR RELEASE DIR)\TEPSAutomatedClientInstallAgent.exe
  - EX installutil.exe -u C:\Services\Tyler-Client-Install-Agent\TEPSAutomatedClientInstallAgent.exe
- press enter 
