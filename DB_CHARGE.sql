USE [HungryPizza]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Name], [Cpf], [Email], [ZipCode], [City], [Street], [Complement], [Reference]) VALUES (1, N'Pedro', N'11111111111', N'pedro@email.com', N'12123123', N'Salvador', N'Rua Direta', NULL, NULL)
INSERT [dbo].[User] ([Id], [Name], [Cpf], [Email], [ZipCode], [City], [Street], [Complement], [Reference]) VALUES (2, N'João', N'22222222222', N'joao@email.com', N'12123123', N'Salvador', N'Rua Sem Saida', N'Cond. Bosque das Flores, apt 1', N'Cond. fica após ao banco')
INSERT [dbo].[User] ([Id], [Name], [Cpf], [Email], [ZipCode], [City], [Street], [Complement], [Reference]) VALUES (3, N'Ana', N'33333333333', N'ana@email.com', N'12123123', N'Salvador', N'Rua do Viaduto', N'Casa 8', NULL)

SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [ProductType], [Price]) VALUES (1, N'Coke', N'SODA', 8.0000)
INSERT [dbo].[Product] ([Id], [Name], [ProductType], [Price]) VALUES (2, N'Heineken', N'BEER', 14.0000)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'3 Queijos', 50.0000, 0)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Frango com requeijão', 59.9900, 0)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Mussarela', 42.5000, 1)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Calabresa', 42.5000, 0)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Pepperoni', 55.0000, 0)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Portuguesa', 45.0000, 0)
INSERT [dbo].[Flavors] ([Flavor], [Price], [IsLacking]) VALUES (N'Veggie', 59.9900, 0)
GO
