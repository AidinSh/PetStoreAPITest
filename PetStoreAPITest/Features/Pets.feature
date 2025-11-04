Feature: Pets

This Feature File consists test cases regarding PetStore project

@tag1
Scenario: Create and Verify a Pet
	Given I have a new pet
	When I create a new pet in the Store
	And I retrieve the pet by ID
	Then I verify the pet details

Scenario: Update and Verify a Pet
	Given I have an existing pet
	When I update the pet name
	And I retrieve the pet by ID
	Then I verify the pet details

Scenario: Find Pet by Status 
	Given I have at least one 'AVAILABLE' pet
	When I search for pets by status 'AVAILABLE'
	Then I verify the search results contain only pets with status 'AVAILABLE'
	And I validate the response schema for the search results

Scenario: Delete a pet 
	Given I have an existing pet
	When I delete the pet by ID
	Then I verify the pet is deleted successfully
