# Petstore API Test Automation Framework

A SpecFlow + C# API automation framework for Swagger Petstore.

## Tech Stack
- .NET 8
- Req&Roll + NUnit
- FluentAssertions
- HttpClient
- 
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

## Run With GitHub Actions 
I have set up a GitHub Actions workflow to run the tests in the cloud.
From the Actions tab in GitHub, you can manually trigger the workflow named `Run Tests Pipeline` to run the tests in the cloud.
