# LostandFound

The Lost and Found App simplifies the hassle of traditional lost and found systems, eliminating tedious paperwork and long wait times. It provides a central platform where users can report lost items and search for their belongings with ease. To maintain community standards, an admin reviews posts before they go live. With a user-friendly interface, the app encourages active participation, helping people recover anything from phones and jewelry to even lost pets.

Key Features

Our application allows users to upload the post with pictures by using MySQL server. Users can post their requirement for lost goods or seeking for person who lost something. Administrator can approve these posts and delete them.
![image](https://github.com/user-attachments/assets/1844cff5-b96f-4f6d-9e41-2ae2049e4e96)



Usage Instructions
1.	Environment set up
1.1 To launch this application, it requires a external MySQL local server. 
1.2  Once the MySQL server installed, open MySQL Command line Client
1.3 Enter password to login.
1.4 Create a new database use the name lostandfound_db. The command is 'create database lostandfound_db'
1.5 Use the database by typing the command 'use lostandfound_db'
1.6 To import all data into this database, we need to find the path to our database file in our computer. Your command would be 'source+path'
  ![image](https://github.com/user-attachments/assets/8cc05f6c-c491-497c-b8f8-c973ab4df721)

1.7 change few lines in \LostandFound\Common\ MySQLHelper.cs 
  ![image](https://github.com/user-attachments/assets/5fb78c4d-64c8-41ac-9c05-f06c1158ec95)

    The uid should be “root” by default and the password is the password that user set for your own MYSQL server.

2.	How to use?
  
We will see a login page once we open the application. To use the admin function, we need to login with account “admin” and password “123” and switch the role to admin. To login in as a user, we can simply use a test account “user” and password “123” or register a new account. 

2.1 User section
  We can see a home page when we login in. 
  All of lost goods information are posted on this page. We can find the detail by click view detail button. 
  The status section shows if the good is claimed or not. Once the status is changed to complete, user cannot contact the poster anymore. If the status is processing, user     can see the contact information by click contact. 
  User can also post a requirement or a finding good on the home page by select add a post button in the My post section. 
  Once a good is claimed user can switch the post states to complete by click End Post.
  
 
2.2 Admin section
  In the admin section, all posts will be showed, and admin can simply decide if we want to show a certain post on the home page or not. 
  In the Users list section, we can change a user’s detail and delete a user. 



