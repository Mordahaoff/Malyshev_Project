drop schema public cascade;
create schema public;
grant all on schema public to postgres;
grant all on schema public to public;

create table Roles(
ID_Role serial primary key,
Name_ varchar(50) not null
);

create table Users(
ID_User serial primary key,
Login varchar(255) not null,
Password_ varchar(255) not null,
Role_ID integer not null references Roles(ID_Role) on update cascade default 1,
Surname varchar(50),
Name_ varchar(50),
Patronymic varchar(50),
Gender varchar(1),
Email varchar(255),
Telephone varchar(10),
Photo varchar(400) not null default 'https://cdn-icons-png.flaticon.com/512/149/149071.png'
);

create table Categories_of_Products(
ID_Category serial primary key,
Name_ varchar(50) not null,
Description text not null
);

create table Brands(
ID_Brand serial primary key,
Name_ varchar(50) not null,
--Description text not null,
URL varchar(255) not null
);

create table Products(
ID_Product serial primary key,
Name_ varchar(255) not null,
Photo varchar(400) not null,
Description text not null,
Price integer not null,
Volume integer not null,
Category_ID integer not null references Categories_of_Products(ID_Category) on update cascade,
Brand_ID integer not null references Brands(ID_Brand) on update cascade
);

create table Discounts(
ID_Discount serial primary key,
Product_ID integer not null references Products(ID_Product) on delete cascade,
Amount smallint not null,
Date_of_Start date not null default current_date,
Date_of_End date not null
);

create table Reviews(
ID_Review serial primary key,
User_ID integer not null references Users(ID_User) on delete cascade,
Product_ID integer not null references Products(ID_Product) on delete cascade,
Text_ text not null,
Grade smallint not null,
Date_of_Creation date not null default current_date
);

create table States_of_Order(
ID_State serial primary key,
Name_ varchar(100) not null
);

create table Addresses(
ID_Address serial primary key,
City varchar(30) not null,
Street varchar(30) not null,
Building varchar(5) not null
);

create table Stores(
ID_Store serial primary key,
Address_ID integer not null references Addresses(ID_Address) on delete cascade
);

create table Orders(
ID_Order serial primary key,
State_of_Order_ID integer not null references States_of_Order(ID_State) on update cascade default 1,
Store_ID integer references Stores(ID_Store) on delete cascade,
User_ID integer  not null references Users(ID_User) on delete cascade,
Date_of_Status_Change timestamp not null default current_timestamp
);

create table Orders_Products(
ID_Orders_Products serial primary key,
Order_ID integer not null references Orders(ID_Order) on delete cascade on update cascade,
Product_ID integer not null references Products(ID_Product) on delete cascade,
Count_of_Product smallint not null default 1
);

create table Stores_Products(
ID_Stores_Products serial primary key,
Store_ID integer not null references Stores(ID_Store) on delete cascade,
Product_ID integer not null references Products(ID_Product) on delete cascade,
Count_of_Product smallint not null default 0
);
