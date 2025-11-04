Feature: Pets

This Feature File consists test cases regarding PetStore project


Scenario: Create and Verify a Pet
	Given Have a new Pet
	When Create a new pet in the store
	And Retrieve the pet by ID
	Then Verify Pet details

Scenario: Update and Verify a Pet
	Given Have an existing pet
	When Update the pet name
	And Retrieve the pet by ID
	Then Veirfy updates on pet details

Scenario: Find Pet by Status 
	Given Have at least one AVAILABLE pet
	When Search for pets by status AVAILABLE
	Then Verify search results contain only pets with status AVAILABLE
	And Validate the response schema for search results


Scenario: Delete a pet 
	Given Have an existing pet
	When Delete Pet by ID
	Then Verify the pet is deleted successfully
