### Prepare to Run
1. modify the ```DatabaseConnection``` in ```appsettings.Development.json```

2. apply the latest migrations by running this command(run command from apis folder)
```
dotnet ef  database update  -s .\FireDetection.Backend.API\  -p .\FireDetection.Backend.Domain\ 
```
3. create migrations
```
dotnet ef migrations add Name -s .\FireDetection.Backend.API\  -p .\FireDetection.Backend.Domain\ 
```