# ZipPay

To test the api use below url reference :

User API

GET : https://localhost:44386/api/User to display all users.
GET : https://localhost:44386/api/User/1 to display details of the user Id
POST : https://localhost:44386/api/User  to add new user
{
	"name": "Suresh Gandikota",
	"email": "suresh.gandikota@xyz.com",
	"salary": 2500,
	"expenses" : 1000
}


User Account API

POST : https://localhost:44386/api/UserAccount to create user account for the user
{
	"userId": 3 // User id 
}

GET : https://localhost:44386/api/UserAccount to get all user accounts
