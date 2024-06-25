Feature: Check Booking Details

  Scenario Outline: Fetch and verfiy booking details for a given/created booking
    Given I have a GET booking request with <FirstName>, <LastName>, <TotalPrice>, <DepositPaid>, <CheckInDate>, <CheckOutDate>, <AdditionalNeeds> and <BookingId>
    And I create a new booking if not ID provided
    When I retrieve booking details for <Scenario>, <BookingId> 
    Then the booking details should match for the <Scenario> data <FirstName>, <LastName>, <TotalPrice>, <DepositPaid>, <CheckInDate>, <CheckOutDate>, <AdditionalNeeds> and <BookingId>
    Examples:
      |Scenario     | FirstName | LastName | TotalPrice | DepositPaid | CheckInDate | CheckOutDate | AdditionalNeeds | BookingId |
      |created      | Mukesh    | Madesia  | 150        | false       | 2023-06-01  | 2023-06-10   | Dinner          |           |  # This will create a new booking
     #|provided     | John      | Smith    | 111        | true        | 2018-01-01  | 2019-01-01   | Breakfast       | 833       |  # This will Existing/ Provided booking details
      |Non-existing | Mother    | Teressa  | 150        | false       | 2023-06-01  | 2023-06-10   | Dinner          | 999999    |  # This is to check not Found


