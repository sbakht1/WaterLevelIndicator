Email settings:
1. Home Controller -> SendWarningEmail ->
Enter your email and generate an
application specific google account password
2. Uncomment line 42
3. Enter your email line 47 and sender 
email in line 50
4. Do the same in WaterLevelApi Controller
-----> SendWarningEmail
---------------------------------------------
Database settings Web.config --> connectionStrings:

1. Replace NEW_SERVER_NAME with the new SQL Server's name or IP address.
2. integrated security: Set to False if you're using SQL Server authentication. Otherwise, if you're using Windows authentication, keep it as True.
3. user id and password: If using SQL Server authentication, provide the SQL Server username and password.
4. initial catalog: Keep the database name same.
----------------------------------------------
1. Create a db named WaterLevelIndicationDB
2. Restore the provided backup
2. SQL Server Agent ---> Jobs ---> Enable
---> WaterLevelIndication_StatusRecordafter30sec
3. Disable when you don't want more records