create database TestDB;
use TestDB;


create table [dbo].[User](
	[Name] [varchar](10) not null,
	[Email] [varchar](50) not null,
	[Password] [varchar](50) not null,
	[UserID] [int] identity not null,
)ON [PRIMARY]