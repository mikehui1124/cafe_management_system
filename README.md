# cafe_management_system
cafe management/ordering system using WinForms and connected to SQL Server database, by Arcadis Ltd.

# Technology Used in the Development

- C# /.NET + WinForms
- User-Authentication done before access
- SQL language for making SELECT/ INSERT/ EDIT /DELETE queries to the database
- SQL Server database, define the schema for four tables for User, Item, Order and OrderDetails which are inter-related
- SQL Server database, define two bespoke view_tables using SQL language by joining fields from these 4 inter-related tables
- Use of connection-string to make connection between SQL Server database and the application
- Use of Class models that match the data-structure for these 4 database tables
- Use of inheritance class in the construction of new classes
- Use of SQL Server Management Studio v18.9

# Walk-through Video of the App

https://github.com/mikehui1124/cafe_management_system/assets/105307687/9c32cef9-be3f-4763-80fb-ae7bfaaa006b


# System in action

- User authentication portal. Input username and password are validated before success.
  
![userLogin_1](https://github.com/mikehui1124/cafe_management_system/assets/105307687/a427f39a-9c4f-4384-94ff-f88dc6656922)
![userLogin_2](https://github.com/mikehui1124/cafe_management_system/assets/105307687/0a491fd3-ccab-4f7a-90f3-09ca101a7aa4)

- Food ordering portal. Available food items are added to the Cart and then the order is placed for a customer.
  
![OrderForm_1](https://github.com/mikehui1124/cafe_management_system/assets/105307687/07302a67-1215-4f0a-9412-67bafe51756b)
![OrderForm_orderplaced](https://github.com/mikehui1124/cafe_management_system/assets/105307687/b59a95fb-e560-40d3-951d-a3fc351898f0)

- Filter out all items in the shop that are 'coffee' from the category menu

![OrderForm_2](https://github.com/mikehui1124/cafe_management_system/assets/105307687/6eb80e82-9b25-4e80-9d38-c05a3867871e)

- Order View List showing each order that are placed by the login-user 'Cheung' for the customer

![OrderViewForm_1](https://github.com/mikehui1124/cafe_management_system/assets/105307687/3a6443b2-bc4e-4290-8f02-51367b7f2322)

- Preview the Order summary printout for order no.(odr-008) after double-click on the Order View List
  
![OrderViewForm_printCheung](https://github.com/mikehui1124/cafe_management_system/assets/105307687/433b8e0f-3440-49dd-9797-62f0bdaff803)

- Food item management portal in action. Allow for Adding new item, Edit/Delete existing item from the Database

![itemForm_1](https://github.com/mikehui1124/cafe_management_system/assets/105307687/92971480-97e0-451a-beca-40f3082860ab)
![itemForm_added](https://github.com/mikehui1124/cafe_management_system/assets/105307687/f3001f01-7ac7-45ae-85ed-6987f6930a80)

- User management portal in action. Allow for adding new user to the system, or Edit/Delete existing user from the Database
  
![userForm_1](https://github.com/mikehui1124/cafe_management_system/assets/105307687/33ff55e3-e658-4785-8051-91d8a7a65ec2)
![userForm_Added](https://github.com/mikehui1124/cafe_management_system/assets/105307687/9d266bde-b73d-4a8b-b168-1920af864080)
