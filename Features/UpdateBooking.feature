Feature: Update Booking API

  Scenario Outline: Update booking with different data inputs
    Given I have a created booking with <FirstName>, <LastName>, <TotalPrice>, <DepositPaid>, <CheckInDate>, <CheckOutDate>, and <AdditionalNeeds>
    When I send a PUT request to update the booking with <UpdatedFirstName>, <UpdatedLastName>, <UpdatedTotalPrice>, <UpdatedDepositPaid>, <UpdatedCheckInDate>, <UpdatedCheckOutDate>, and <UpdatedAdditionalNeeds>
    Then the booking update should be <ExpectedStatus> with the same <UpdatedFirstName>, <UpdatedLastName>, <UpdatedTotalPrice>, <UpdatedDepositPaid>, <UpdatedCheckInDate>, <UpdatedCheckOutDate>, and <UpdatedAdditionalNeeds>
    And the update response status code should be <ExpectedStatusCode>

    Examples:
      |ScenarioDescription| FirstName | LastName | TotalPrice | DepositPaid | CheckInDate | CheckOutDate | AdditionalNeeds | UpdatedFirstName | UpdatedLastName | UpdatedTotalPrice | UpdatedDepositPaid | UpdatedCheckInDate | UpdatedCheckOutDate | UpdatedAdditionalNeeds | ExpectedStatusCode | ExpectedStatus |
      |All valid data     | Mukesh    | Madesia  | 150        | true        | 2024-06-25  | 2025-01-05   | None            | James            | Brown           | 111               | true               | 2018-01-01         | 2019-01-01          | Breakfast              | 200                | Successfull    |


