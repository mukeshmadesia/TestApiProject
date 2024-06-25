Feature: Create Booking API

  Scenario Outline: Create booking with different data inputs
    Given I have a booking request with <FirstName>, <LastName>, <TotalPrice>, <DepositPaid>, <CheckInDate>, <CheckOutDate>, and <AdditionalNeeds>
    When I send a POST request to create the booking
    Then the booking creation should be <ExpectedStatus> with the same <FirstName>, <LastName>, <TotalPrice>, <DepositPaid>, <CheckInDate>, <CheckOutDate>, and <AdditionalNeeds>
    And the response status code should be <ExpectedStatusCode>

    Examples:
          | ScenariosDescription    | FirstName | LastName | TotalPrice | DepositPaid | CheckInDate | CheckOutDate | AdditionalNeeds | ExpectedStatusCode | ExpectedStatus |
          | All Valid Data          | Mukesh    | Madesia  | 150        | true        | 2024-06-25  | 2025-01-05   | None            | 200                | Successfull    |
          | Zero Total Price        | Jim       | Brown    | 0          | true        | 2025-01-01  | 2026-01-01   | Breakfast       | 200                | Successfull    |
          | False Depositpaid       | Jane      | Doe      | 2000       | false       | 2023-06-01  | 2023-06-10   | Dinner          | 200                | Successfull    |
          | Blank AdditionalNeeds   | John      | Smith    | 10         | true        | 2022-12-15  | 2023-01-05   |                 | 200                | Successfull    |
         #| Blank FirstName         |           | Madesia  | 150        | true        | 2024-06-25  | 2025-01-05   | None            | 400                | Failed         |
         #| Incorrect CheckInDate   | Mukesh    | Madesia  | 150        | true        | 2024-18-25  | 2025-01-05   | None            | 400                | Failed         |