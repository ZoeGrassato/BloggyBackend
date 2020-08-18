Feature: Blog Articles
	The ability to create, update and read blog articles within the bloggy system

Scenario: As a user I create a blog article
	#Given I am a user with access to bloggy # TODO: add authorization
	When I submit a blog article with the title Hello and 1 sections with 1 images and 1 paragraphs each
	Then My blog article should exist within bloggy with the title Hello
	And My blog article has 1 sections with 1 images and 1 paragraphs each
	And My blog article has the correct data

Scenario: As a user I update a blog article
	#Given I am a user with access to bloggy # TODO: add authorization
	Given I have a blog article with the id 0537b53f-67ab-4520-819d-394663934ddf and sectionId 5f89a27c-676c-4575-a03f-de6f091a5fa5
	When I update the blog article with id 0537b53f-67ab-4520-819d-394663934ddf and sectionId 5f89a27c-676c-4575-a03f-de6f091a5fa5 and paragraphId 8622825b-b0e1-4d96-8ee5-6d449fab2873 and set paragraphTextArea to AGAINUPDATED
	Then The blog article with id 0537b53f-67ab-4520-819d-394663934ddf and sectionId 5f89a27c-676c-4575-a03f-de6f091a5fa5 and paragraphId 8622825b-b0e1-4d96-8ee5-6d449fab2873 should reflect the updated info with paragraphTextArea set to AGAINUPDATED

