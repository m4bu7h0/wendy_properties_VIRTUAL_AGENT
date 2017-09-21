create database wendy_properties

use [wendy_properties]
go

create table [dbo].[AdminUsers](
	[UserGUID] varchar(40),
	[Username] varchar(100),
	[Password] varchar(100),

	primary key (UserGUID)
)

create table [dbo].[Agents](
	[AgentGUID] varchar(40) not null,
	[Avatar] varbinary(MAX),
	[FirstName] varchar(100),
	[LastName] varchar(100),
	[Cellphone] varchar(10),
	[Email] varchar(100),

	primary key (AgentGUID)
)

create table [dbo].[Listings](
	[ListingGUID] varchar(40) not null,
	[Price] decimal(16,2) not null,
	[Image] varbinary(MAX),
	[Bedrooms] int not null,
	[ReferenceNumber] varchar(40) not null,
	[MarketingHeading] nvarchar(1000) not null,
	[Description] nvarchar(1000) not null,
	[Suburb] varchar(500) not null,
	[AgentGUID] varchar(40)  foreign key references 
	[wendy_properties].[dbo].[Agents](AgentGUID),

	primary key (ListingGUID)   
)

create table [dbo].[Contact](
	[ContactGUID] varchar(4),
	[FirstName] varchar(100),
	[LastName] varchar(100),
	[Email] varchar(50),
	[Message] varchar(500),

	primary key ([ContactGUID])
)

create table [dbo].[FeaturedListings](
	[FeatureGUID] varchar(40),
	[FeaturedDate] datetime,
	[ListingGUID] varchar(40) foreign key references
	[wendy_properties].[dbo].[Listings]([ListingGUID]),

	primary key ([FeatureGUID])
)