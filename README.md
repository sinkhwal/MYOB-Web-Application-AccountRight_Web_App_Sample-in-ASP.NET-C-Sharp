# AccountRight_WebApp_Sample-in-ASP.NET-C-Sharp
Web application developed in asp.net c# to for reporting and automatic backup purpose for company file in myob cloud.


C# .Net Sample Web App
ASP.Net sample app demonstrating accessing the AccountRight Live API using the SDK in c#.

AccountRight API - C#.Net Web Application
•	Uses the new .Net SDK  
•	Navigates available files on cloud server 
•	Manages OAuth login (note: SDK manages refreshing of tokens!) 
•	Demonstrates use of paging and filtering through listing of Purchase order Sales, Profit and loss, and balance sheets.
•	Gives automatic backup of purchase order and sales report of definite period of time which we can configure in webconfig.

Requirements
•	Visual Studio 
•	MYOBApi developer key and secret(To access cloud server)

Getting up and running
• Unzip source code to a local folder and open the solution file. 
• Restore Nuget Packages,
• Get Developer key and Developer Secret key for your account from developer section from account. For more detail go to http://developer.myob.com/api/accountright/api-overview/getting-started/
• Update developer key and Developer Secret key and CallBackUrl from there in webconfig in this project.
• then compile and run. 
• It will first ask for Username, Password of provide it. It will send you passkey to email, enter that key to two way authentication page. Then you are all set. It will take you to company listing with that account. Put user name and password for that company.. it wil take you to search page where you can search Purchase, Sales, Profit and Loss etc. 
• For automatic backup you have to create windows task which open browser with url http://localhost/Web/AutoBackup as parameter. You can put some triggers which will create backup files in excel format.

Happy Coding!!!
