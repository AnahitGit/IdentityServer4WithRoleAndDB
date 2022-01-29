Steps
1.Create database with name

2 Create Users table by script
        USE [CompEmpIdentityServer]
        GO

        /****** Object:  Table [dbo].[Users]    Script Date: 1/29/2022 8:15:16 PM ******/
        SET ANSI_NULLS ON
        GO

        SET QUOTED_IDENTIFIER ON
        GO

        CREATE TABLE [dbo].[Users](
          [Id] [int] IDENTITY(1,1) NOT NULL,
          [UserName] [nvarchar](50) NOT NULL,
          [Password] [nvarchar](50) NOT NULL,
          [Role] [nvarchar](50) NULL,
         CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
        (
          [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
        GO
3. Uncomment .MigrateDatabase() part in program.cs
4. Run 
5. During first run migration will do automaticaly, after that go and again comment migration part  

In Postmal call https://localhost:5005/connect/token  post  method with (client_id, client_secret, grant_type, username, password ) body keys
Take token, and use it in api call https://localhost:5001/api/companies  - in header add Authorization with bearer




