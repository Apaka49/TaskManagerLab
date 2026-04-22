# Task Manager Application (Final Project)

A professional task management system built with C# and WPF, following modern software engineering principles and architectural patterns.

## Architecture & Patterns
The project implements a **3-Tier (N-Tier) Architecture** combined with the **MVVM (Model-View-ViewModel)** design pattern:
1.  **TaskManager.WPF (Presentation Layer):**
    * Responsive UI with WPF.
    * MVVM pattern for clear separation of concerns.
    * Asynchronous commands and UI state management (`IsBusy` indicator).
2.  **TaskManager.Services (Business Layer):**
    * Business logic implementation.
    * Data Transfer Objects (DTOs) for secure communication between layers.
3.  **TaskManager.Repositories (Data Layer):**
    * **Entity Framework Core** with **SQLite** for persistent storage.
    * Repository pattern for data abstraction.
    * Cascade deletion for data integrity.

## Key Features
* **Full CRUD:** Create, Read, Update, and Delete operations for both Projects (Level 1) and Tasks (Level 2).
* **Real-time Search:** Filter lists dynamically as you type.
* **Sorting:** Toggle alphabetical sorting for all lists.
* **Async/Await:** Non-blocking UI during database operations.
* **Auto-Seeding:** Initial database population on the first launch.
* **Modern UI:** Clean, responsive design with loading indicators and visual feedback.

## Technologies
* **.NET 8.0**
* **WPF** (Windows Presentation Foundation)
* **Entity Framework Core**
* **SQLite**
* **Microsoft.Extensions.DependencyInjection** (IoC/DI)

## Conclusion (Висновок)
В ході виконання фінального проєкту було розроблено повнофункціональний багатошаровий застосунок. Було закріплено навички роботи з реляційними базами даних (SQLite) через ORM Entity Framework Core, реалізовано асинхронну модель взаємодії з даними та побудовано гнучкий інтерфейс на базі MVVM. Проєкт відповідає принципам SRP та IoC, забезпечуючи надійну роботу та зручне масштабування.