# MyCarApp (Graduation project for the C# MVC Frameworks Course @ SoftUni July 2018)

# Short description

This is an implementation of online car avertisements site.

You can use it when selling your vehicles. 

In the 'Search' section, you can find the following vehicles - cars.

You do not need to look for a particular model, but can use other search criteria such as price, first registration date, colour, etc.
After you have completed a search, you get to a list of search results that includes all the vehicles that match your search criteria. 

By clicking on the ad, you will get to the Detail View. Here, you will receive the remaining information about the vehicle and the vendor, whom you can also contact at this point.

# Project Assignment 

I.	General Requirements

The Web application should use the following technologies, frameworks and development techniques:
1.	The application must be implemented using ASP.NET Core 2.1 framework.
1.1.	 The application must have at least 8 web pages (views)
1.2.	The application must have at least 4 entity models (excluding users and roles)
1.3.	The application must have at least 4 controllers and at least 3 Razor pages
2.	Use Visual Studio 2017.
2.1.	Use the Razor template engine for generating the UI
-	Use sections and partial views.
-	Use display and editor templates.
2.2.	Optionally, could also use Web API to create a RESTful service and use JavaScript for
the front end 
3.	Use Microsoft SQL Server as database back-end
3.1.	Optionally, use multiple storages, e.g. files, other Web services, databases (e.g. MySQL / MongoDB / Cassandra / etc.)
4.	Use Entity Framework Core to access the database
4.1.	If need additional connectors to other databases, feel free to use them
5.	Use MVC areas to separate different parts of the application (e.g. area for administration)
6.	Adapt the default ASP.NET Core site template or get another free theme
6.1.	Use responsive design based on Twitter Bootstrap / Google Material design
6.2.	Or just design own
6.3.	Don’t get too involved in design but make the app usable and good-looking
7.	Use the standard ASP.NET Identity System for managing users and roles
7.1.	Registered users should have at least one of these roles: user and administrator
7.2.	If need, implement own user management system
8.	Create at least 4 controllers (with at least one action for each) and at least 3 Razor pages
9.	Optionally, use AJAX request to asynchronously load and display data somewhere in the application
10.	Write unit tests for logic, controllers, actions, helpers, etc.
10.1	Should have at least 10 test cases
11.	Implement error handling and data validation to avoid crashes when invalid data is entered
11.1.	Both client-side and server-side, even at the database(s)
12.	Handle correctly the special HTML characters and tags like <br /> and <script> (escape special characters)
13.	Use Dependency Injection
13.1.	The built-in one in ASP.NET Core is perfectly fine
14.	Optionally, use AutoМapping
15.	Prevent from security vulnerabilities like SQL Injection, XSS, CSRF, parameter tampering, etc.
14.	Additional Requirements
1.	Follow the best practices for Object Oriented design and high-quality code for the Web application:
o	Use the OOP principles properly: data encapsulation, inheritance, abstraction and polymorphism
-	Use exception handling properly
-	Follow the principles of strong cohesion and loose coupling
-	Correctly format and structure code, name identifiers and make the code readable
2.	Make the user interface (UI) good-looking and easy to use
3.	Support all major modern Web browsers
-	Optionally, make the site as responsive as possible – think about tablets and smartphones
4.	Use caching where appropriate
5.	Use a source control system by choice, e.g. GitHub, BitBucket
-	Submit a link to public source code repository
15.	Public Project Defense
Each student will have to deliver a public defense of its work in front of a trainer. 
Students will have only 10 minutes for the following:
-	Demonstrate how the application works (very shortly)
-	Show the source code and explain how it works
-	Answer questions related to the project (and best practices in general)
16.	Bonuses
1.	Anything that is not described in the assignment is a bonus if it has some practical use
2.	Examples
-	Use SignalR communication somewhere in the application.
-	Host the application in a cloud environment, e.g. in AppHarbor or Azure
-	Use a file storage cloud API, e.g. Dropbox, Google Drive or other for storing the files
-	Use of features of HTML 5 like Geolocation, Local Storage, SVG, Canvas, etc.
