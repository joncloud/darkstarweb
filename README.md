Configuration
-------------

Web.config stores all of the configuration for the application.
In order to ensure that the application has database permissions update Web.config's <connectionStrings> element.  Common changes will include changing the following properties:
  * server
  * uid
  * password

Database Installation
---------------------

In order to install the application's database use Visual Studio to apply Entity Framework's migrations:

  1. Right click the Dsp.Web project, and choose Set as StartUp Project.
  2. Open the NuGet Package Manager Console (Tools\NuGet Package Manager\Package Manager Console).
  3. In the NuGet Package Manager Console change the Default Project to "Dsp.Web".
  4. Enter Update-Database, and press enter.

The `accountcontext` database should now be installed on the database server specified in configuration.