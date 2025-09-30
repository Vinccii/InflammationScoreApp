# Inflammation Score App

A simple C# console application to track potentially inflammatory food intake.  
This project was built as a learning exercise to practice CRUD operations with SQLite and improve skills in C#, database handling, and input validation.

---

## Features

- **Insert** new entries (date + quantity)
- **View** all entries in a formatted list
- **Update** existing entries by ID
- **Delete** entries by ID
- Robust input validation for dates and numbers
- Stores all data in a local **SQLite database** (`inflammation-score.db`)

---

## Example Usage
![0At5PSD0Jc](https://github.com/user-attachments/assets/09afe0b5-8056-4605-90b4-36c892c248d2)
Example output when viewing all stats:
<img width="488" height="91" alt="image" src="https://github.com/user-attachments/assets/ee6ddad0-a21c-4c2c-a8cc-fbef4a6a7401" />

## Tech Stack

- **C# (.NET 8)**
- **SQLite** with `Microsoft.Data.Sqlite`
- Console Application

## What I Learned

- Performing CRUD operations with SQLite
- Using SqliteDataReader to map query results into objects
- Writing clean, structured commit messages

## Future Improvements

- Integrate **Azure AI Services** for:
  - Smart food categorization (e.g. detecting inflammatory foods from text input)
  - Natural Language Commands ("log 200ml green tea today") 
- Add basic statistics/reports (totals, averages)
- Extend app into a full **Habit / Health Tracker** (Pomodoro, XP, streaks)



