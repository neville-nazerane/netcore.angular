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


## Usage

This library provides you with intellisense as long as you have the properties in your view model in a razor page. 

Let's say you have an `Employee` class with properties `FirstName` and `Age`. Your view model can have an `Employee` property. You can now have intellisense for several angular tags by using `ang` instead of `ng` like this:

```
<div ang-bind="Employee.FirstName">
    <span ang-bind="Employee.Age">
```  

This code makes sure your angular tags are valid as per your c# classes and that you get intellisense for everything.
 
If your `Employee` property has a value in c#, you can pass this value to angular without needing to setup an API endpoint and consume it with a angular controller. You can simply use the following:

```
<div ang-data="Employee">
  <div ang-bind="Employee.FirstName">
      <span ang-bind="Employee.Age"></span>
  </div>
</div>
```

`ang-data` would add the value of `Employee` from c# into an the `$scope.employee` in angular js. If you don't want the output to be `$scope.employee`, but instead set into a custom variable like `$scope.customEmployee`, you can replace `ang-data="Employee"` with `ang-bind="Employee" ang-scope-dest="customEmployee"`.  

This code doesn't need an angular controller to be setup. However, for additional functionality, js code can be integrated with this.

```
<div ng-controller="myController" ang-data="Employee">
  <div ang-bind="Employee.FirstName">
      <span ang-bind="Employee.Age"></span>
  </div>
</div>
```
You can then create a controller in js and manage your `$scope.employee`. 
      

