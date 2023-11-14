Feature: User Accounts
	Verify User accounts


Scenario Outline: Verify User account creation
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 200
	And Verify user created with name <name>

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |

Scenario Outline: Verify User account creation with multiple accounts
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 200
	And Verify user created with name <name>

Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |


	


Scenario Outline: Verify User account creation With returned account number
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 200
	And Verify user created with returned account number

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |


	
Scenario Outline: Verify multiple user accounts creation With returned account number
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 200
	And Verify user created with returned account number


Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |


Scenario Outline: Verify Remove User account with Name
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	And Remove user account with name <name>
	Then Verify Response Status Code is 200
	And Verify user removed with name <name>

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |


Scenario Outline: Verify Remove multiple User accounts with Name
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	And Remove user account with name <name>
	Then Verify Response Status Code is 200
	And Verify user removed with name <name>

Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |





Scenario Outline: Verify Remove User account which does not exist with account number
	Given Customer Account number <accountNumber>
	When Remove User Account <accountNumber>
	Then Verify Response Status Code is 500
	
Examples:
	| accountNumber |
	| 99            |
	| 88            |



Scenario Outline: Verify Remove User accounts With returned account number
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	And Remove user account with account number
	Then Verify Response Status Code is 200
	And Verify user removed with returned account number

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |


	
Scenario Outline: Verify Remove multiple user accounts  With returned account number
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	And Remove user account with account number
	Then Verify Response Status Code is 200
	And Verify user removed with returned account number


Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |

Scenario: Verify User account with invalid user name
	Given Customer TestUser opens 1
	When Create User Account
	Then Verify user not exist with name InvalidUser

Scenario: Verify User account with invalid account number
	Given Customer TestUser opens 1
	When Create User Account
	Then Verify user not exist with account number 99

Scenario Outline: Verify User account creation with empty name
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 500
	
Examples:
	| name | numberOfAccounts |
	|      | 1                |
	|      | 1                |


