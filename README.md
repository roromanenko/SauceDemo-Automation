# SauceDemo UI Test Automation Framework

This project is an automated test framework built with **.NET**, **Selenium WebDriver**, and **xUnit** for testing the [SauceDemo](https://www.saucedemo.com/) web application.  
It follows the **Page Object Model (POM)** pattern and provides reusable components for browser configuration, logging, and test execution.

---

## Project Structure
```
Core – Configuration, WebDriver factory, logging
Pages – Page Object classes (LoginPage, DashboardPage)
Tests – Test classes, fixtures, and test data
```

---

## Technologies Used

- **.NET 9 / C#**
- **xUnit** – test framework  
- **Selenium WebDriver** – browser automation  
- **FluentAssertions** – expressive assertions  
- **log4net** – logging to console  
- **Page Object Model (POM)** – structured page abstraction  

---

## Implemented Test Scenarios

| ID       | Description                                                         |
| -------- | ------------------------------------------------------------------- |
| **UC-1** | Login with empty credentials → Verify “Username is required”        |
| **UC-2** | Login with only username → Verify “Password is required”            |
| **UC-3** | Login with valid credentials → Verify successful login to Dashboard |

---

## Key Features
- Logging with log4net
- Assertions with FluentAssertions
- Configurable browser and waits through `appsettings.json`

---
## Author
Developed by **Roman Romanenko**
