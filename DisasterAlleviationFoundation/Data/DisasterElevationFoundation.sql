
-- create database DisasterEllevationFoundation
USE DisasterEllevationFoundation
create table Doner (DonerID int identity(1,1) primary key,
Name varchar(20) not null,
Surname varchar(20) not null,
Email varchar(50) not null,
Password varchar(100) not  null,
UserType INT NOT NULL DEFAULT 0);

create table Category (CategoryID int identity(1,1) primary key,
CategoryName varchar(20));

create table MonetaryDonation(MonetaryDonationID int identity(1,1) primary key,
DonationDate datetime not null, 
Amount float not null, 
DonerID int not null foreign key references Doner (DonerID));

create table GoodsDonation (GoodsDonationID int identity(1,1) primary key,
NumberOfItems INT not null,
DonationDate datetime not null,
CategoryID int not null foreign key references Category (CategoryID),
DescriptionOfItems varchar(200) not null,
DonerID int not null foreign key references Doner (DonerID));

create table DisasterDetails (DisasterID int identity(1,1) primary key,
StartDate datetime not null,
EndDate datetime not null,
Location varchar(100) not null,
Description varchar(300) not null,
RequiredAid varchar(200),
DonerID int not null foreign key references Doner (DonerID));


--part 2 admin alocation
create table DisasterMonetaryAllocation (DisasterMonetaryAllocationID int identity(1,1) primary key,
AllocationDate datetime not null,
Amount float not null,
DisasterID int not null foreign key references DisasterDetails (DisasterID),
DonerID int not null foreign key references Doner (DonerID));

create table DisasterGoodsAllocation (DisasterGoodsAllocationID int identity(1,1) primary key,
AllocationDate datetime not null,
DisasterID int not null foreign key references DisasterDetails (DisasterID),
GoodsDonationID int not null foreign key references GoodsDonation (GoodsDonationID),
DonerID int not null foreign key references Doner (DonerID));

--for purchased goods
create table PurchasedGoods (PurchasedGoodsID int identity(1,1) primary key,
GoodsDonationID int not null foreign key references GoodsDonation (GoodsDonationID),
Amount float not null,
PurchaseDate DATETIME NOT NULL,
);


/*
drop table PurchasedGoods;
drop table DisasterGoodsAllocation;
drop table DisasterMonetaryAllocation;
drop table MonetaryDonation;
drop table DisasterDetails;
drop table GoodsDonation;
drop table Category;
drop table Doner;

*/
insert into Doner values ('Anonymous','Anonymous','Anonymous@gmail.com','1024A',DEFAULT); 
insert into Doner values ('AnonymousAdmin','AnonymousAdmin','AnonymousAdmin@gmail.com','1024A',DEFAULT); 
insert into Category values ('Clothes');
insert into Category values ('Non-perishable foods');

UPDATE Doner SET UserType = 1 WHERE DonerID = 2;

select * from Doner;

select * from GoodsDonation gd 
inner join DisasterGoodsAllocation dga on gd.GoodsDonationID = dga.GoodsDonationID 
inner join DisasterDetails dd on dd.DisasterID =dga.DisasterID;