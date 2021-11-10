--create database Movies;

--use Movies;

--create table Movies (
--  ID int identity(1,1) primary key,
--  Name varchar(100) not null,
--  ReleaseDate date not null
--);

insert into Movies(Name,ReleaseDate) values
('Interstellar','2014-04-25'),
('Dune','2021-10-28'),
('F9','2021-08-25'),
('The Godfather','1972-03-23'),
('The Wolverine','2013-06-15'),
('Logan','2017-05-27');
 
--create table Actors (
--  ID int identity(1,1) primary key,
--  Name varchar(100) not null,
--  Surname varchar(100) not null,
--  BornDate date not null,
--);

--insert into Actors(Name,Surname,BornDate) values
--('Ellen','Burstun','1980-04-06'),
--('Matther','McConaughey','1990-05-21'),
--('Mackenzie','Foy','2004-07-21'),
--('Timothée','Chalamet','2000-07-21'),
--('Rebecca','Feruguson','1990-07-21'),
--('Oscar','Issac','2004-07-21'),
--('Vin','Diesel','1984-03-11'),
--('Michelle','Rodriguez','1989-07-21'),
--('John','Cena','1995-07-21'),
--('Marlo','Brando','1940-07-21'),
--('Al','Pacino','1950-07-21'),
--('James','Caan','1954-03-21'),
--('Hugh','Jackman','1968-10-12'),
--('Tao','Okamoto','1999-07-21'),
--('Rila','Fukushima','1989-07-21'),
--('Patrick','Steward','1988-03-25'),
--('Defne','Keen','1989-07-21');

--create table Roles(
--  ID int Identity(1,1) primary key,
--  Name varchar(100) not null,
--  ActorID int not null,
--  MovieID int not null,
--  foreign key (MovieID) references Movies(ID) on delete cascade,
--  foreign key (ActorID) references Actors(ID) on delete cascade
--);

--insert into Roles(Name,ActorID,MovieID) values
--('Murph(Old)',1,1),
--('Cooper',2,1),
--('T.A.R.S voice',3,1),
--('Paul Atreides',4,2),
--('Jessica Atreides',5,2),
--('Duke Leto',6,2),
--('Vin Diesel',7,3),
--('Michelle Rodriguez',8,3),
--('Jakob',9,3),
--('Don Vito Corleone',10,4),
--('Michael',11,4),
--('Sonny',12,4),
--('Logan',13,5),
--('Mariko',14,5),
--('Yukio',15,5),
--('Logan',13,6),
--('Charles',16,6),
--('Laura',17,6);

--create table Ratings(
--  ID int identity(1,1) primary key,
--  MovieID int not null,
--  Rating int not null,
--  foreign key (MovieID) references Movies(ID) on delete cascade
--);

--insert into Ratings(MovieID,Rating) values
--(5,1),
--(2,2),
--(4,3),
--(3,4),
--(5,5),
--(5,6);

--SELECT *
--FROM Movies
--ORDER BY ID
--OFFSET 3 ROWS FETCH NEXT 6 ROWS ONLY

--select count(*) from movies;
