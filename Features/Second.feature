Feature: Subtraction
  Scenario: Sub scenario
    Given I have entered 70 into the calculator
    And I have entered 50 into the calculator
    When I press sub
    Then the result should be 20 on the screen
