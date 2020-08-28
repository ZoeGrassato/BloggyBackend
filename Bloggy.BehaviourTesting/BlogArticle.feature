Feature: Blog Articles
	The ability to create, update and read blog articles within the bloggy system

Scenario: As a user I create a blog article
	#Given I am a user with access to bloggy # TODO: add authorization
	When I submit a blog article with the title THIS and 1 sections with 1 images and 1 paragraphs each
	Then My blog article should exist within bloggy with the title THIS
	And My blog article has 1 sections with 1 images and 1 paragraphs each
	And My blog article has the correct data

Scenario: As a user I update a blog article
	#Given I am a user with access to bloggy # TODO: add authorization
	Given I have a blog article with a blogArticle id and a sectionId
	When I update the blog article with the blogArticle id and section id and paragraphId set paragraphTextArea to AGAINUPDATED 
	Then The blog article with the blogArticleId and sectionId and paragraphId should reflect the updated info with paragraphTextArea set to AGAINUPDATED

