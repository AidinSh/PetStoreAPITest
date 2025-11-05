# Petstore API Test Automation Framework

A SpecFlow + C# API automation framework for Swagger Petstore.

## Tech Stack
- .NET 8
- SpecFlow + NUnit
- FluentAssertions
- HttpClient
- LivingDoc reporting

---

## Setup

1. Clone this repo  
2. Install dependencies:
   ```bash
   dotnet restore

## Run Tests
Just simply go to the project directory and run:
   ```bash
   dotnet test --logger "trx;LogFileName=TestResult.trx" 
   ```
   This will execute all the tests and generate a test result file named `TestResult.trx`.