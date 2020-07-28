Feature: Blog Articles
	The ability to create, update and read blog articles within the bloggy system

Scenario: As a user I create a blog article
	#Given I am a user with access to bloggy # TODO: add authorization
	When I submit a blog article with the title Hello and 2 sections with 2 images and 4 paragraphs each
	Then My blog article should exist within bloggy with the title Hello
	And My blog article has 2 sections with 2 images and 4 paragraphs each
	And My blog article has the correct data

Scenario: As a user I update a blog article
	#Given I am a user with access to bloggy
	Given I have a blog article with the id 96054de8-8b5c-447b-9185-eddc5d9fcc0b and sectionId 16054de8-8b5c-447b-9185-eddc5d9fcc0b and paragraphId 17054de8-8b5c-447b-9185-eddc5d9fcc0b
	When I update the blog article with id 96054de8-8b5c-447b-9185-eddc5d9fcc0b and sectionId 16054de8-8b5c-447b-9185-eddc5d9fcc0 and paragraphId 17054de8-8b5c-447b-9185-eddc5d9fcc0b and set paragraphTextArea to updatedParagraphTextArea
	Then The blog article with paragraphId 17054de8-8b5c-447b-9185-eddc5d9fcc0b should reflect the updated info with paragraphTextArea set to updatedParagraphTextArea