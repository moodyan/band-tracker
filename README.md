# _Band Tracker_

#### _App created for Epicodus Independent Project, C-Sharp - Week Four. Practice using SQL and Advanced Databases. June 16, 2017_

#### By _**Alyssa Moody**_

## Description

_A simple program that tracks bands and the venues where they've played shows._

## Program Specifications

| Description  | Input Example | Output Example |
| ------------- | ------------- | ------------- |
| 1. The program allows users to add a band to the database.  | Band Name: "Bon Iver" Members: "Justin Vernon" Genre: "Indie Folk" Details: "Bon Iver is an American indie folk band founded in 2007 by singer-songwriter Justin Vernon. Vernon released Bon Iver's debut album, For Emma, Forever Ago, independently in July 2007. The majority of that album was recorded while Vernon spent three months isolated in a cabin in northwestern Wisconsin." | "Success!"  |
| 2. The program allows users to view all bands.  | --   | "Here is a list of all bands."  |
| 3. The program allows users to add a venue to the database.  | Venue Name: "Crystal Ballroom" Venue Address: "1332 W. Burnside,
Portland, OR 97209" Venue details: "The historic Crystal Ballroom is one of those rare concert halls that can point to a proud, diverse history while also laying claim to an ongoing musical legacy."  | "Success!"  |
| 4. The program allows users to click on each band or venue and view their details.  | --   | --  |
| 5. The program allows users to add a band to a venue or a venue to a band.  | --   | --  |
| 6. The program allows users to update an existing venue's location and/or details.  | Venue's Location (if changed): "" Venue details: "The historic Crystal Ballroom -- now over a century old -- is one of those rare concert halls that can point to a proud, diverse history while also laying claim to an ongoing musical legacy. Every time you enter this majestic ballroom, let your imagination sense the tremors resonating from a century's worth of gatherings, and realize that you are joining a thriving, generations-long procession of show-goers. Welcome!"  | "Success!"  |
| 7. The program allows users to delete an existing venue from the database.  | "Delete?"  | "Success!"  |
| 8. The program allows users to delete an existing band from the database.  | "Delete?"  | "Success!"  |
| 9. The program allows users to search for a venue by location.  | Location: "Portland"  | Results: "Crystal Ballroom, Wonder Ballroom, Aladdin Theater, Doug Fir Lounge, Mississippi Studios, Dante's"  |
| 10. MAYBE The program allows users to add shows to a venue and/or a band.  | --   | --  |

## Setup/Installation Requirements

_Runs on the .Net Framework._

_Install Visual Studio 2015. https://go.microsoft.com/fwlink/?LinkId=532606 ._

_Install ASP.Net 5. This will give you access to the .NET Framework. https://go.microsoft.com/fwlink/?LinkId=627627 ._

_Restart PowerShell. While located in your machine's Home directory, enter the command > dnvm upgrade._

_Requires Nancy Web Framework located at: http://nancyfx.org/. You can also do this via Windows PowerShell with the command > **Install-Package Nancy**_

_**From GitHub: Download or clone project repository onto desktop from GitHub.**_

_In your preferred database management system (I use SSMS), open the band_tracker.sql file from the project folder. Run the execute command on the file. If this does not work, run the following command in SQLCMD:

CREATE DATABASE band_tracker; GO USE band_tracker; GO CREATE TABLE bands (id INT IDENTITY(1,1), band_name VARCHAR(100), members VARCHAR(255), genre VARCHAR(50), information text); GO CREATE TABLE venues (id INT IDENTITY(1,1), venue_name VARCHAR(100), location VARCHAR(100), details text); GO CREATE TABLE bands_venues (id INT IDENTITY(1,1), bands_id INT, venues_id INT); GO CREATE TABLE shows (id INT IDENTITY(1,1), city_state VARCHAR(50), date DATETIME); GO CREATE TABLE tour (id INT IDENTITY(1,1), bands_id INT, shows_id INT, venues_id INT);
GO_

_To create test database, in your preferred database management system (I use SSMS) open the band_tracker_test.sql file from the project folder. Run the execute command on the file. If this does not work, back up and restore the database as a test database in your preferred database management system.

_In PowerShell, cd into the project folder. Enter the command > **dnu restore**_

_Enter the command > **dnx kestrel**_

_In your preferred browser, navigate to http://localhost:5004/ and you should see the application._

## Known Bugs

_TO DO: Need to implement DeleteShow method for Venue and Band classes. Need to show band name and venue name on views. Find way to connect venues played at with shows._

## Support and contact details

_If you run into any issues or have questions, ideas or concerns, please contact Alyssa Moody at alyssanicholemoody@gmail.com_

## Technologies Used

_**Languages:** HTML, CSS, C#, SQL._

_**Frameworks:** Nancy, .Net._

_**Testing:** xUnit._

### License

*MIT license Agreement*

Copyright (c) 2017 **_Alyssa Moody_**
