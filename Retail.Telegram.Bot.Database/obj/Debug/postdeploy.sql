INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (1, N'Цена', 0, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (2, N'Размер', 1, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (3, N'Дизайн', 2, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (4, N'Цвет', 3, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (5, N'Удобство и комфорт', 4, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (6, N'Не определился с моделью', 5, 0)
GO
INSERT [bot].[RefusalType] ([Id], [Name], [Order], [HasComment]) VALUES (7, N'Другое', 6, 1)
GO
SET IDENTITY_INSERT [bot].[Store] ON 

GO
INSERT [bot].[Store] ([Id], [StoreId], [Name], [Address], [CreatedOn], [ModifiedOn]) VALUES (1, 101, N'Adidas', N'ул. Крылатская, д.15', CAST(N'2017-04-21T19:17:52.543' AS DateTime), CAST(N'2017-04-21T19:17:52.543' AS DateTime))
GO
SET IDENTITY_INSERT [bot].[Store] OFF
GO
SET IDENTITY_INSERT [bot].[User] ON 

GO
INSERT [bot].[User] ([Id], [UserName], [EmployeeId], [AccessCode], [PhoneNumber], [StoreId], [Status], [CreatedOn], [ModifiedOn], [LastLoginOn]) VALUES (1, N'Иванов', 43211, N'111', N'89031222341', 101, 1, CAST(N'2017-04-21T19:21:29.213' AS DateTime), CAST(N'2017-04-21T19:21:29.213' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [bot].[User] OFF
GO
