Feature: Retrieve Booking IDs

  Scenario Outline: Retrieve booking IDs based on different criteria
    Given I have a booking request with query parameters <FirstName>, <LastName>, <CheckInDate>, <CheckOutDate>
    When I send a GET request to retrieve the booking IDs
    Then the filter response status code should be 200
    And the response should contain atleast <NoOfBooking> booking IDs

    Examples:
      | FirstName | LastName | CheckInDate | CheckOutDate |NoOfBooking |
      |           |          |             |              | 1          | # Retrieves all booking IDs
      | Mukesh    | Madesia  |             |              | 0          | # Retrieves booking IDs by first name and last name
      |           |          | 2022-09-05  | 2024-06-25   | 1          | # Retrieves booking IDs by check-in and check-out dates
