use [wendy_properties]
go

create proc [dbo].[GetFeaturedListingsFE]
as
begin
	
	-- Columns from Listings table, so join
	select li.[ListingGUID], li.[MarketingHeading],
		li.[Description], li.[Price], li.[Image],
		li.[ReferenceNumber], li.[AgentGUID],
		li.[Bedrooms], li.[Suburb]	
	from [dbo].[FeaturedListings] fl
	join [dbo].[Listings] li
	on li.[ListingGUID] = fl.[ListingGUID]
end
go

create proc [dbo].[GetListingDetailsFE]
	@ListingGUID varchar(40)
as
begin
	
	-- Get Listing Details
	select * 
	from [dbo].[Listings]
	where [ListingGUID] = @ListingGUID
end
go

create proc [dbo].[GetListingsPricesFE]
as
begin
	
	-- Get Listings prices
	select [Price] from [dbo].[Listings]
end
go

create proc [dbo].[GetListingsBedroomsFE]
as
begin
	
	-- Get Listings bedrooms
	select distinct [Bedrooms] from [dbo].[Listings]
	order by [Bedrooms]
end
go

create proc [dbo].[GetListingsSuburbsFE]
as
begin
	
	-- Get Suburbs
	select distinct [Suburb]
	from [dbo].[Listings]
end
go

create proc [dbo].[SearchListingsFE]
	@Suburb varchar(500),
	@MinPrice decimal(5,2),
	@MaxPrice decimal(5,2),
	@Bedrooms int
as
begin
	
	-- Using a search criteria for listing
	select * from [dbo].[Listings]
	where [Suburb] = @Suburb
	and [Price] >= @MinPrice
	and [Price] >= @MaxPrice
	and [Bedrooms] = @Bedrooms
end
go

create proc [dbo].[AddContactFE]
	@FirstName varchar(100),
	@LastName varchar(100),
	@Email varchar(50),
	@Message varchar(500)
as
begin
	
	-- Create a contact information
	insert into [dbo].[Contact](
		[ContactGUID],[FirstName], 
		[LastName], [Email], [Message]
	)values(
		newid(), @FirstName, @LastName,
		@Email, @Message
	)
end
go

create proc [dbo].[AddAdminUserBE]
	@Username varchar(100),
	@Password varchar(100)
as
begin
	
	-- Create an admin user
	insert into [dbo].[AdminUsers](
		[UserGUID], [Username], [Password]
	)values(
		newid(), @Username, @Password
	)
end
go

create proc [dbo].[AddAgentBE]
	@Avatar varbinary(MAX),
	@FirstName varchar(100),
	@LastName varchar(100),
	@Cellphone varchar(10),
	@Email varchar(100)
as
begin
	
	-- Create an agent
	insert into [dbo].[Agents](
		[AgentGUID], [Avatar],
		[FirstName], [LastName],
		[Cellphone], [Email]
	)values(
		newid(), @Avatar, @FirstName,
		@LastName, @Cellphone, @Email
	)
end
go

create proc [dbo].[AddListingBE]
	@Price decimal(5,2),
	@Bedrooms int,
	@Image varbinary(MAX),
	@ReferenceNumber varchar(40),
	@MarketingHeading nvarchar(1000),
	@Description nvarchar(1000),
	@Suburb varchar(500),
	@AgentGUID varchar(40)
as 
begin
	
	-- Create a listing
	insert into [dbo].[Listings](
		[ListingGUID], [Price], [Bedrooms],
		[Image], [ReferenceNumber], 
		[MarketingHeading], [Description], 
		[Suburb], [AgentGUID]
	)values(
		newid(), @Price, @Bedrooms, @Image,
		@ReferenceNumber, @MarketingHeading,
		@Description, @Suburb, @AgentGUID
	) 
end
go

create proc [dbo].[UpdateFeaturedBE]
	@ListingGUID varchar(40)
as
begin
	
	-- Update featured listings
	update [dbo].[FeaturedListings] 
	set [ListingGUID] = @ListingGUID,
		[FeaturedDate] = getdate()
	where [ListingGUID] = (
		select top 1 [ListingGUID] 
		from [dbo].[FeaturedListings]
		order by [FeaturedDate])
end