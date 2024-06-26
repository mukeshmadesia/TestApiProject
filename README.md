# Project Riverty

## Project Explanation and Details

This project is a SpecFlow-based testing framework for API testing, built using NUnit and C#. It tests various booking functionalities provided by the Restful Booker API. The framework includes features and step definitions for creating, retrieving, filtering, and updating bookings.

## Project Directory Structure

```plaintext
.github/
└── workflows/
    ├── AdhocRun.yml         # Workflow configuration for adhoc runs
    └── RunCI.yml            # Workflow configuration for CI execution on push and merge events

Features/
├── CreateBooking.feature    # Scenarios for creating a booking
├── FilterBookingID.feature  # Scenarios for filtering booking IDs
├── GetBooking.feature       # Scenarios for retrieving a booking
└── UpdateBooking.feature    # Scenarios for updating a booking

StepDefinitions/
├── CreateBookingSteps.cs    # Step definitions for creating a booking
├── FilterBookingIdSteps.cs  # Step definitions for filtering booking IDs
├── GetBookingSteps.cs       # Step definitions for retrieving a booking
├── Hooks.cs                 # Hooks for before and after scenario actions for Reporting
└── UpdateBookingSteps.cs    # Step definitions for updating a booking

Utilities/
├── Config.cs                # Configuration settings management
└── ExtentReportManager.cs   # Extent report management for test results

TestResults/
└── ExtentReport.html        # Generated test report

.env                         # Contains Base API URL and Authorization Key

## Framework

- Framework: NUnit
- BDD Tool: SpecFlow
- Language: C#
- Test Cases in Gherkin (Given/When/Then)
- Extent - html Report

## Local Setup

### Clone the Repository

```sh
git clone https://github.com/mukeshmadesia/TestApiProject.git
cd <path-to-project-folder>


## Install Dependencies
```sh
dotnet restore

Ensure you have the following installed:

- .NET SDK
- NUnit Console Runner
- SpecFlow for Visual Studio

## Set Up Environment Variables

Create a `.env` file in the root directory with the following content:

```plaintext
API_BASE_URL=https://restful-booker.herokuapp.com
AUTHORIZATION_KEY=your-authorization-key


## Build the Project
```sh
dotnet build

## Run Tests Locally
To run tests locally, execute the following command:
```sh
dotnet test


## Test Execution - Continuous Integration
### Adhoc Run Workflow
.github/workflows/AdhocRun.yml is triggered manually for adhoc testing.
- Go to `Actions`Tab
- Click on Riverty Adhoc Test`on left side in Workflow
- On Right - Click `Run Workflow`, Choose branch and `RunWorkflowà

### CI Pipeline
Push and Merge Triggers: The RunCI.yml workflow is configured to run on push and merge events. This ensures that the tests are executed as part of the CI pipeline.
.github/workflows/RunCI.yml is uto triggered on push and merge to main branch for continuous integration.


## Reporting

Extent Report - has been used for reporting
Report - TestResults/ExtentReport.html

CI Test Report can be downloaded from `Artifacts`section Within Test execution Pipeline

### Report Content
- No of Test caes passed/failed and Pie chart representation of same.
- Start Time , End Time
- Log events for each Test and it status


