# netcore.angular
integration of angular and .net core mvc


To initialize: 

1. Install the bower package netcore-angular
2. Add angular service in startup.cs with: services.AddNetCoreAngular()
3. Add tag helpers in view imports with: @addTagHelper *, NetCore.Angular
4. Add generated scripts using at the end of the page using: <script-angular></script-angular>
