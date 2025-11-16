# SauceDemo UI Test Automation Framework

This project is an automated test framework built with **.NET**, **Selenium WebDriver**, and **xUnit** for testing the [SauceDemo](https://www.saucedemo.com/) web application.  
It follows the **Page Object Model (POM)** pattern and provides reusable components for browser configuration, logging, and test execution.

---
## Task description
The task is to implement automated tests for the website https://www.saucedemo.com/
- UC-1: Login with Empty Credentials
  - Enter any text into the Username and Password fields.
  - Clear both inputs.
  - Click the Login button.
  - Verify the error message: "Epic sadface: Username is required".

- UC-2: Login with Missing Password
  - Enter any value in the Username field.
  - Enter any value in the Password field.
  - Clear the Password field.
  - Click the Login button.
  - Verify the error message: "Epic sadface: Password is required".

- UC-3: Login with Valid Credentials
  - Enter any valid username from the Accepted usernames section.
  - Enter the password secret_sauce.
  - Click Login and validate that the dashboard displays the title "Swag Labs".

- **Test Framework:** XUnit
- **Validation Framework:** FluentValidation
- **Locators:** XPath
- **Browsers:** Chrome, Firefox

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
