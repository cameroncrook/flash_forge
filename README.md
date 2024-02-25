# Overview

FlashForge! A C# terminal study application.

A C# study application that integrates with a Firestore database to store flash cards for individual users and enables users to store these flashcards in organization units. Application offers different activities of study the terms to increase long term memorization of the terms.

There are many study applications available. My personal favorite is Quizlet but they can be a little pricey. The purpose of this application is to offer an alternative to Quizlet and seeks to implement the essential functions of a solid study app.

[Software Demo Video](https://youtu.be/CyaYALgFUWE?si=9pfrLYkfAZPtWXza)

# Cloud Database

Google Firestore

3 collections:
users
- Stores username and encrypted password

folders
- stores folder names and references a parent folder if it is within another folder

study_sets
- stores term data 
- references folder that the set is contained in

# Development Environment

- VScode
- C#
- .NET

libraries
- BCrypt.Net;
- Google.Cloud.Firestore;

# Useful Websites

- [Google Firestore Documentation](https://cloud.google.com/firestore/docs)
- [W3Schools](https://www.w3schools.com/cs/cs_intro.php)

# Future Work

- Display folder contents
- Implement study_set database crud functions