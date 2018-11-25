# netcore.angular
integration of angular and .net core mvc

## Installation:

For installation, you can either use the dotnet.csp expermental tool that would allow you to install client and server sides in a single command. Alternatively you can manually install each.

### Using dotnet csp:

If not already installed, you can install dotnet csp using:
```
dotnet tool install --global Dotnet.Csp --version 0.5.0-beta
```
Once installed, inside your .net core web project you can the following CLI:
```
dotnet csp add netcore.angular
```

OR

### install each manually:

First install the nuget package `NetCore.Angular`. 
For the js file, clone the git repo git@github.com:neville-nazerane/netcore-angular-bower.git and get the /js/netcore-angular.js file. 



## Server side set up:

1. Add angular service in startup.cs with: 
```
services.AddNetCoreAngular()
```
2. Add tag helpers by adding the following line at the bottom of ViewImports.cshtml: 
```
@addTagHelper *, NetCore.Angular
```
3. Add generated scripts using at the end of the razor page (layout page or every page) using: 
```
<script-angular></script-angular>
```

## Client side set up

1. Setup the default angularjs setup.

2. Include the netcore-angular.js scripts file

3. In your angular app module, include `netcore-angular`




