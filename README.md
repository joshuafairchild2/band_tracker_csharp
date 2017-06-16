# Band Tracker

*Epicodus C# Independent Project - Week 4 - June 16, 2017*

**By Joshua Fairchild**

---

## Description

This application allows the user to:

* Create a new band/concert venue
* Assign an existing band to an existing venue or vice/versa
* View all venues/bands or view an individual venue/band
* Delete all venues/bands
* Remove the relationship between a band and a venue

---

#### Setup/Installation Requirements

* Install [Mono](http://www.mono-project.com/docs/getting-started/install/) or another C# compiler

* Install the [Nancy](http://nancyfx.org/) web framework

* Install the ASP.NET CORE framework for access to the Kestrel server

* Clone this repository

  `git clone https://github.com/joshuafairchild1/band_tracker_csharp`

* Navigate to the root directory

##### Database Setup

* In PowerShell, run the following commands:

  `SQLCMD -S "(localdb)\mssqllocaldb"`

  `CREATE DATABASE band_tracker;`

  `GO`

  `USE band_tracker;`

  `GO`

  `CREATE TABLE bands (id INT IDENTITY(1,1), name VARCHAR(255), number_of_members INT);`

  `GO`

  `CREATE TABLE venues (id INT IDENTITY(1,1), name VARCHAR(255), address VARCHAR(255));`

  `GO`

  `CREATE TABLE members (id INT IDENTITY(1,1), name VARCHAR(255), band_id INT);`

  `GO`

  `CREATE TABLE venues_bands (id INT IDENTITY(1,1), venue_id INT, band_id INT);`

  OR

*  Open Microsoft SQL Server Management Studio

* Select *File > Open > File* and select your database of choice from the root directory (band_tracker.sql for running the application, or band_tracker_test.sql for running tests)

* If the database does not already exist, add the following lines to the top of the script file:

  `CREATE DATABASE [your_database_name];`

  `GO`

* Save the file and click "! Execute"

* In PowerShell, run `dnx kestrel` (from the root directory) to start the server

* Navigate to `localhost:5004` in your web browser to view the application

* Tests can be run with the command `dnx test` from PowerShell while within the root directory



#### Known Bugs/Issues
* None known


#### Technologies Used
* Nancy web framework
* Razor C# syntax
* C#
* HTML
* CSS (Bootstrap)
* Kestrel web server
* xUnit
* SQL
* Microsoft SQL Server Management Studio


#### Legal

This software is licensed under the MIT license

Copyright (c) 2017 Joshua Fairchild
