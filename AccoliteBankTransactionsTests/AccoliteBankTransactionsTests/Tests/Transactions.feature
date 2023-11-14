Feature: Account Transactions
	Verify User Transactions and balance


Scenario Outline: Verify Bank balance on account creation
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify Response Status Code is 200
	And Verify account balance must be 100


Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |


	
Scenario Outline: Verify bank balance on multiple accounts creation
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	Then Verify account balance must be 100
	And Verify Response Status Code is 200

Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |

Scenario Outline: Verify bank balance when withraw 10 to check account should have 100 all the time
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	When Create User Account
	And Make Transaction
		| type     | amount |
		| withdraw | 10     |
	Then Verify Response Status Code is 500
	And Verify account balance must be 100
	

Examples:
	| name   | numberOfAccounts |
	| Chandu | 3                |
	| Demo   | 3                |


Scenario Outline: Verify bank balance when credit 10
	Given Customer Account number <accountNumber>
	When Make Transaction On the given account
		| type   | amount |
		| credit | 10     |
	Then Verify account balance must be 110
	And Verify Response Status Code is 200

Examples:
	| accountNumber |
	| 10332         |
	| 10333         |


Scenario Outline: Verify when user Credit 10001$
	Given Customer Account number <accountNumber>
	When Make Transaction On the given account
		| type   | amount |
		| credit | 10001  |
	Then Verify Response Status Code is 500
	And Verify account balance must be 100

Examples:
	| accountNumber |
	| 10332         |
	| 10333         |


	
Scenario Outline: Verify bank balance when try to withdraw more than 90%
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	When Make Transaction
		| type     | amount |
		| credit   | 1000   |
		| withdraw | 1000   |
	Then Verify Response Status Code is 500
	And Verify account balance must be 1100

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |

Scenario Outline: Verify withdraw amount which is equal to 90% of account balance
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	When Make Transaction
		| type     | amount |
		| credit   | 1000   |
		| withdraw | 990    |
	Then Verify Response Status Code is 200
	And Verify account balance must be 110


Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |



Scenario Outline: Verify bank balance after multiple credits
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	When Make Transaction
		| type   | amount |
		| credit | 1000   |
		| credit | 1000   |
		| credit | 1000   |
	Then Verify Response Status Code is 200
	And Verify account balance must be 3100
	

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |


Scenario Outline: Verify bank balance after multiple credits and withdrawls
	Given Customer <name> opens <numberOfAccounts>
	When Create User Account
	When Make Transaction
		| type     | amount |
		| credit   | 1000   |
		| credit   | 1000   |
		| credit   | 1000   |
		| withdraw | 500    |
		| withdraw | 500    |
		| withdraw | 500    |
	Then Verify account balance must be 1600

Examples:
	| name   | numberOfAccounts |
	| Chandu | 1                |
	| Demo   | 1                |

Scenario Outline: Verify Bank balance with Invalid account number
	Given Customer Account number <accountNumber>
	When Get account statement for account number <accountNumber>
	Then Statement should be empty

Examples:
	#    Invalid accounts
	| accountNumber |
	| 99            |
	| 88            |

	
Scenario: Verify credit on Invalid account number
	Given Customer Account number <accountNumber>
	When Try credit 100 on account number <accountNumber>
	Then Verify Response Status Code is 500
	
Examples:
#    Invalid accounts
	| accountNumber |
	| 99            |
	| 88            |

	
Scenario: Verify withdrawl on Invalid account number
	Given Customer Account number <accountNumber>
	When Try withdrawal 100 on account number <accountNumber>
	Then Verify Response Status Code is 500
	
Examples:
#    Invalid accounts
	| accountNumber |
	| 99            |
	| 88            |


Scenario Outline: Verify Bank balance with removed account number
	Given Customer <name> opens <numberOfAccounts>
	When Remove user account with account number
	And Get statement for account
	Then Statement should be empty


Examples:
	| name       | numberOfAccounts |
	| Test User1 | 1                |
	| Test User2 | 1                |

Scenario Outline: Verify Credit with negative number
	Given Customer Account number <accountNumber>
	When Make Transaction On the given account
		| type   | amount |
		| credit | -1000  |
	Then Verify Response Status Code is 500
	And Verify account balance must be 100

Examples:
#    Invalid accounts
	| accountNumber |
	| 10332         |
	| 10333         |

	
Scenario Outline: Verify withdrawal with negative number
	Given Customer Account number <accountNumber>
	When Make Transaction On the given account
		| type     | amount |
		| withdraw | -1000  |
	Then Verify Response Status Code is 500
	And Verify account balance must be 100

Examples:
#    Invalid accounts
	| accountNumber |
	| 10332         |
	| 10333         |