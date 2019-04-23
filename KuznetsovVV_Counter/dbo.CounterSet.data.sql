SET IDENTITY_INSERT [dbo].[CounterSet] ON
INSERT INTO [dbo].[CounterSet] ([CounterId], [Type], [Description]) VALUES (1, N'Вода', N'Счетчик измеряет расход воды в литрах')
INSERT INTO [dbo].[CounterSet] ([CounterId], [Type], [Description]) VALUES (2, N'Газ', N'Счетчик измеряет расход газа в кубических метрах')
INSERT INTO [dbo].[CounterSet] ([CounterId], [Type], [Description]) VALUES (3, N'Электричество', N'Счетчик измеряет расход электроэнергии в киловатт-часах')
SET IDENTITY_INSERT [dbo].[CounterSet] OFF
