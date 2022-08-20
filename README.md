# PublishPost
Technical test to Zemoga

I want to explain a little bit what I did and what is necessary to run the app. I worked with Role-based authorization in ASP.NET Core since it was necessary to create users, roles and do validations with that and, I used Swagger as the front-end to do the test and apply the validations. I used **SQL Server** and **entity framework core** to connect the data and the code. It's not necessary to do the database migration because when the app starts, I coded the Main method in Program class to generate the migration database and create the roles if they don't exist.

Before of initialize the app you need to change the database connection in the **appsettings.json** file the attribute you will need to change is: 
**ConnectionStrings -> DefaultConnection**

also, you need to run a database script in order to create the status records, I put the script.

**USE [PublishPost]
GO
INSERT [dbo].[Statuses] ([Name], [IsLock]) VALUES (N'Submitted', 0)
GO
INSERT [dbo].[Statuses] ([Name], [IsLock]) VALUES (N'Pending Approval', 1)
GO
INSERT [dbo].[Statuses] ([Name], [IsLock]) VALUES (N'Rejected', 0)
GO
INSERT [dbo].[Statuses] ([Name], [IsLock]) VALUES (N'Approved', 1)
GO**

Since I used swagger when the app is up you need to change the URL and add: **/swagger** this allows you to see the controllers and the endpoint that you can use.

In this app you should create the users and you can do it through **user controller** specifically the method: **/api/v1/identity/register** you have to put a valid email, valid password and the role name, the app contains 2 roles: **Writer, Editor** and regarding the test file you have some permissions according to the role. When you created the user the api returns you a Json object like this:

**{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGV4YW1wbGUuY29tIiwianRpIjoiMWQ5MTg2MTItMjNmZC00MjlhLWEzNGUtMmYxYmJjYmNmMWRmIiwiZW1haWwiOiJ1c2VyQGV4YW1wbGUuY29tIiwiaWQiOiJjNDU4ZjdlOS1hY2VjLTRlMzgtYTRjMS00NjFjZGQwZmRjMDYiLCJyb2xlIjoiRWRpdG9yIiwibmJmIjoxNjYwOTU4MTIyLCJleHAiOjE2NjA5NTgxNjcsImlhdCI6MTY2MDk1ODEyMn0.NGWF8G3bWoGC4ndxaAFuzfnHczFV83O2unmuIgsJAJM",
  "refreshToken": "f06addd2-06c0-4b94-a4ae-832a007821b6"
}**

In order to do the login, you just need to copy the value of the property **token** and then you click in the button Authorize this button is at the top, when you did click a modal window is opened, you should put in the **value textbox** as follows:

reserved word "bearer" + space + the token that you copied like this:

**bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGV4YW1wbGUuY29tIiwianRpIjoiMWQ5MTg2MTItMjNmZC00MjlhLWEzNGUtMmYxYmJjYmNmMWRmIiwiZW1haWwiOiJ1c2VyQGV4YW1wbGUuY29tIiwiaWQiOiJjNDU4ZjdlOS1hY2VjLTRlMzgtYTRjMS00NjFjZGQwZmRjMDYiLCJyb2xlIjoiRWRpdG9yIiwibmJmIjoxNjYwOTU4MTIyLCJleHAiOjE2NjA5NTgxNjcsImlhdCI6MTY2MDk1ODEyMn0.NGWF8G3bWoGC4ndxaAFuzfnHczFV83O2unmuIgsJAJM**

then you do click on **Authorize** button. The app generates the login according to this token, this implementation is called: **JWT Authorization header using the bearer scheme**.

If you've created the user, you need to use the other endpoint **/api/v1/identity/login** you just need to put the email and password that you previously created, and the app returns you a new token and it's just repeat the last steps to do the login.

That's it.

If you need me, please reach out me.
My email is: david.a.p1@hotmail.com

Thanks.
