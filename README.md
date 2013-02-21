Service is left for Visual Studio to create a test host.
A Customer Table is needed for testing purposes.

CREATE TABLE [dbo].[Customer] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] VARCHAR (255) NULL,
    [LastName]  VARCHAR (255) NULL,
    [Email]     VARCHAR (255) NULL,
    [Curp]      VARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Curp] ASC)
);

