# SFAsyncSearch
An asynchronous implementation of Sitefinity CMS Search allowing the site to be searchable from different clients such as browser or mobile apps using a Web API

# Features

- Super easy to set up
- No bloated dependencies except for Sitefinity and ASP.NET WebAPI
- Extensible
- Includes an example page

# Install

1- You will need to copy the custom folder to the Sitefinity Web Application project

2- You must enable ASP.NET Web API with a compatible version for .NET 4. 

You can use the nuget package manager to install the right version. In the Package Manager Console use the following:

```shell
install-package Microsoft.AspNet.WebApi -Version 4.0.30506.0
```
3- In Global.asax (You will need to add a Global Configuration File if you haven't one already) you will need to add the configuration. 

```c#
        protected void Application_Start(object sender, EventArgs e)
        {
            //SUBSCRIBE TO SITEFINITY BOOTSTRAP EVENTS
            SystemManager.ApplicationStart += OnBootstrapperApplicationStart;
            Bootstrapper.Initialized += OnBootstrapperInitialized;
        }
```

```c#
        protected static void OnBootstrapperInitialized(object sender, ExecutedEventArgs e)
        {


            //REGISTER MVC CONFIGS AND ROUTES
            if (e.CommandName == "RegisterRoutes")
            {
                WebApiConfig.Register(GlobalConfiguration.Configuration);
            }
            
        }
```
The WebAPIConfig class can be found under Custom/Search/Configuration.

4- At this point you should be ready to make a request to the Search controller /api/search/get?searchTerm=term

