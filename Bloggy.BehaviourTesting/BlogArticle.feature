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
	And I have a blog article with the following information
	| id | ... |
	When I update the blog article with the following information
	| id | ...
	Then The blog article should reflect the updated info in bloggy