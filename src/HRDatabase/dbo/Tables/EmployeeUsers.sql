CREATE TABLE [dbo].[EmployeeUsers] (
    [EmployeeIndex] INT NOT NULL,
    [UserIndex]     INT NOT NULL,
    CONSTRAINT [PK_EmployeeUsers_1] PRIMARY KEY CLUSTERED ([EmployeeIndex] ASC),
    CONSTRAINT [FK_EmployeeUsers_HR_Employees] FOREIGN KEY ([EmployeeIndex]) REFERENCES [dbo].[HR_Employees] ([EmployeeIndex]),
    CONSTRAINT [FK_EmployeeUsers_Users] FOREIGN KEY ([UserIndex]) REFERENCES [dbo].[Users] ([UserIndex])
);

