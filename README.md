# BikeDB 2025
A Windows application to manage bikes, routes, speedometer data and much more.

## What is BikeDB 2025
Since more than 20 years I'm saving my bicycle data, collected normally with Sigma Sport speedometers. First I used a local web page which was created with PHP 4.2. Later I developed a MS Access database, but I also used spreadsheets.

Time passed and in 2024 I wanted to recreate the project from scratch. I used Visual Studio 2022 with a local SQL database.

## Features of BikeDB 2025
- Runs under Windows 10/11 64bit without installation. Local installation could be used on multiple computers from a USB stick.
- Multiuser interface. It can be used with administrators and multiple users. Thus, more than one user can have their own data and profiles. Admins can setup network data and modules, what users cannot do.
- Admin accounts can install the software on servers, create other databases than the local one, make the software available on a LAN or WAN (not yet tested).
- Export and import data as Excel *.xls data. Users can only dump their own data, admins can dump the whole database.
- Multiple vehicle support: started with bicycles, the applications now supports any vehicle type: cars, motorbikes, e-bikes; you could track your hiking by feet or running training sessions.
- Every user can have any amount of vehicles. It is also possible that a car which is used by more than one user, is tracked individually.
- Several modules included: tracks, cities, countries. You can create routes that you regularly use, or one-time routes. Daily route home to work, or routes that you drove during a vacation. There are no limits.
- OpenStreetMap is included and the only functionality that needs web access. The application supports GPS coordinates on every level.
- More modules: notes and plans, persons, calendar, birthdays.
- "Notes and plans": you can define targets that you want to achieve or just leave a freetext note without any timer. If you achieve a target ("do 100 km on a bike in October"), it will be marked as achieved automatically.
- "Persons" can be used to track every activity that you did with family and friends. When you add them to your private list, you can also be reminded of their birthdays.
- More modules: FlightDB and QR-Codes. FlightDB can manage planes, flights, destinations etc. QR-Codes can be used to generate QR-Codes from any URL that is used within the application.

## FlightDB is in alpha status
As the FlightDB is an optional module and not yet complete, admins can decide to deactivate it globally, thus no user can access it and menu items will disappear.

## Achtung: BikeDB2025 ist momentan nur auf Deutsch verfügbar
Ohne Unterstützung wird es wohl auch so bleiben, da ich die BikeDB nur für mich selbst entwickelt habe. Meine Anforderungen prägen das Projekt und ich muss sie nicht in eine andere Sprache übersetzen, da sie für mich funktioniert.

## Import of speedometer data (Sigma Sport TL 2009/2012)
If you have a Sigma Sport Topline docking station, then you can use my other app, SigmaNotificationApp, to detect if a speedometer was attached to the docking station. This allows you to directly import the data from the speedometer to BikeDB.

## Current status of BikeDB 2025
I use this application regularly, but there are still some issues that I need to debug. Anyway, BikeDB 2025 is 99% complete for a local installation with one admin and multiple users.
Export/Import is broken, because the underlying database has changed to much. This will be fixed once I deem the database structure final.
FlightDB is currently not further developed, thus it will be deactivated in the final release version 1.0.
The admin module is not yet fully complete.
Installation on web servers and other databases is prepared, but neither tested nor fully functional. The Windows application can already be configured as client and maybe you can make the database run on a server and connect from the client. But it is not garanteed.

Thus, still in development, but made open for public. Feel free to contact me here on Github!
