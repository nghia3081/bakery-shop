USE [master]
GO
/****** Object:  Database [BakingIngredients]    Script Date: 7/23/2022 12:31:19 AM ******/
CREATE DATABASE [BakingIngredients]

GO
ALTER DATABASE [BakingIngredients] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BakingIngredients].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BakingIngredients] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BakingIngredients] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BakingIngredients] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BakingIngredients] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BakingIngredients] SET ARITHABORT OFF 
GO
ALTER DATABASE [BakingIngredients] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BakingIngredients] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BakingIngredients] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BakingIngredients] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BakingIngredients] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BakingIngredients] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BakingIngredients] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BakingIngredients] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BakingIngredients] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BakingIngredients] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BakingIngredients] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BakingIngredients] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BakingIngredients] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BakingIngredients] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BakingIngredients] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BakingIngredients] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BakingIngredients] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BakingIngredients] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BakingIngredients] SET  MULTI_USER 
GO
ALTER DATABASE [BakingIngredients] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BakingIngredients] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BakingIngredients] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BakingIngredients] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BakingIngredients] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BakingIngredients] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BakingIngredients] SET QUERY_STORE = OFF
GO
USE [BakingIngredients]
GO
/****** Object:  Table [dbo].[blog]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[blog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](300) NOT NULL,
	[detail] [ntext] NOT NULL,
	[photo_link] [ntext] NOT NULL,
	[enable_status] [bit] NULL,
	[owner] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart_item]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart_item](
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[user_email] [nvarchar](100) NOT NULL,
	[added_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[user_email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[delivery_status]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[delivery_status](
	[order_item] [int] NOT NULL,
	[updated_time] [datetime] NOT NULL,
	[delivery_unit] [nvarchar](100) NOT NULL,
	[shipping_status] [ntext] NOT NULL,
	[shipping_completed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_item] ASC,
	[updated_time] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_email] [nvarchar](100) NULL,
	[user_name] [nvarchar](100) NULL,
	[phone] [char](10) NULL,
	[ship_address] [ntext] NULL,
	[amount] [money] NOT NULL,
	[payment_method] [int] NULL,
	[payment_status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_item]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [int] NULL,
	[product_name] [nvarchar](150) NOT NULL,
	[photo_link] [ntext] NOT NULL,
	[price] [money] NOT NULL,
	[quantity] [int] NOT NULL,
	[bought_date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment_method]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment_method](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[method] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[detail] [ntext] NULL,
	[photo_link] [ntext] NOT NULL,
	[price] [money] NOT NULL,
	[quantity] [int] NOT NULL,
	[category_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_quantity]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_quantity](
	[product_id] [int] NOT NULL,
	[shop_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[update_date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[shop_id] ASC,
	[update_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[role]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[id] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shop]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shop](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[staff_email] [nvarchar](100) NULL,
	[address] [ntext] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 7/23/2022 12:31:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[phone] [char](10) NULL,
	[name] [nvarchar](100) NULL,
	[address] [ntext] NULL,
	[age] [int] NULL,
	[gender] [bit] NULL,
	[photo_link] [ntext] NULL,
	[role_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[blog] ON 
GO
INSERT [dbo].[blog] ([id], [title], [detail], [photo_link], [enable_status], [owner]) VALUES (3, N'A Demo Blog', N'Lorem ipsum dolor sit amet consectetur adipisicing elit. Necessitatibus odit beatae illo praesentium, fuga maiores corporis cum laudantium ea, ipsum, assumenda obcaecati mollitia et? Modi perferendis nemo iste unde! Placeat.
Non alias vitae dignissimos, delectus qui quia ut laudantium fugit id eius! Amet optio itaque, sunt eveniet iusto ut consequatur vero debitis aut accusamus, explicabo quas alias assumenda deleniti odio.
Suscipit, atque accusamus eos cupiditate debitis error cum repellat qui alias neque sapiente illum fugit ut doloremque unde obcaecati est sequi aut. Debitis aperiam excepturi culpa nemo consequatur minima suscipit.
Ad cupiditate illum laboriosam consequatur modi unde, laudantium iusto. Dolorum ratione quis, totam eius mollitia tenetur consequatur consectetur. Velit enim, itaque eos temporibus voluptate harum deserunt quidem eveniet iure dolorem.
Earum nemo, ipsa eaque natus nobis impedit iusto! Accusantium quasi odit perspiciatis quam. Quasi officia fugit atque at exercitationem deleniti quisquam nisi, doloremque perferendis reiciendis, dolore delectus adipisci aut pariatur.
Quas earum incidunt tenetur temporibus dolor numquam veniam architecto animi quibusdam nulla, illo pariatur, totam est! Est corrupti aliquid provident nesciunt tempore nostrum. Ullam veniam repudiandae ipsum recusandae, error obcaecati?
Esse non nihil ut commodi nobis enim reprehenderit, sed magni at veniam placeat mollitia? Libero consectetur atque aspernatur voluptas non at necessitatibus reiciendis neque, amet qui, in totam ex velit!
Recusandae, laudantium laboriosam, incidunt magnam rerum maiores vero repellendus quis ipsa corrupti sunt provident doloribus perspiciatis beatae inventore temporibus eos deleniti nesciunt earum non natus assumenda molestiae? Iusto, quae unde.
Earum voluptate sunt sequi suscipit expedita laudantium explicabo totam adipisci, ipsam maiores, consequatur est nostrum neque illum hic minima voluptas quis ea aut beatae ipsa nulla autem saepe modi? Neque.
Dolore dolorem, aut accusamus tempora omnis sed corrupti? Nisi optio quam fugiat, nobis distinctio quisquam repudiandae alias quasi neque quae voluptatibus nam! Eum, maxime. Aliquid fugiat possimus quam consectetur porro.', N'img/09-mercedes-amg-gt-r-c-190-wallpaper.jpg', 1, N'admin@gmail.com')
GO
INSERT [dbo].[blog] ([id], [title], [detail], [photo_link], [enable_status], [owner]) VALUES (4, N'Test blog', N'Lorem ipsum dolor sit amet consectetur adipisicing elit. Necessitatibus odit beatae illo praesentium, fuga maiores corporis cum laudantium ea, ipsum, assumenda obcaecati mollitia et? Modi perferendis nemo iste unde! Placeat.
Non alias vitae dignissimos, delectus qui quia ut laudantium fugit id eius! Amet optio itaque, sunt eveniet iusto ut consequatur vero debitis aut accusamus, explicabo quas alias assumenda deleniti odio.
Suscipit, atque accusamus eos cupiditate debitis error cum repellat qui alias neque sapiente illum fugit ut doloremque unde obcaecati est sequi aut. Debitis aperiam excepturi culpa nemo consequatur minima suscipit.
Ad cupiditate illum laboriosam consequatur modi unde, laudantium iusto. Dolorum ratione quis, totam eius mollitia tenetur consequatur consectetur. Velit enim, itaque eos temporibus voluptate harum deserunt quidem eveniet iure dolorem.
Earum nemo, ipsa eaque natus nobis impedit iusto! Accusantium quasi odit perspiciatis quam. Quasi officia fugit atque at exercitationem deleniti quisquam nisi, doloremque perferendis reiciendis, dolore delectus adipisci aut pariatur.
Quas earum incidunt tenetur temporibus dolor numquam veniam architecto animi quibusdam nulla, illo pariatur, totam est! Est corrupti aliquid provident nesciunt tempore nostrum. Ullam veniam repudiandae ipsum recusandae, error obcaecati?
Esse non nihil ut commodi nobis enim reprehenderit, sed magni at veniam placeat mollitia? Libero consectetur atque aspernatur voluptas non at necessitatibus reiciendis neque, amet qui, in totam ex velit!
Recusandae, laudantium laboriosam, incidunt magnam rerum maiores vero repellendus quis ipsa corrupti sunt provident doloribus perspiciatis beatae inventore temporibus eos deleniti nesciunt earum non natus assumenda molestiae? Iusto, quae unde.
Earum voluptate sunt sequi suscipit expedita laudantium explicabo totam adipisci, ipsam maiores, consequatur est nostrum neque illum hic minima voluptas quis ea aut beatae ipsa nulla autem saepe modi? Neque.
Dolore dolorem, aut accusamus tempora omnis sed corrupti? Nisi optio quam fugiat, nobis distinctio quisquam repudiandae alias quasi neque quae voluptatibus nam! Eum, maxime. Aliquid fugiat possimus quam consectetur porro.', N'img/13-mercedes-amg-gt-r-c-190-wallpaper.jpg', 1, N'admin@gmail.com')
GO
SET IDENTITY_INSERT [dbo].[blog] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 
GO
INSERT [dbo].[category] ([id], [name]) VALUES (1, N'Caked')
GO
INSERT [dbo].[category] ([id], [name]) VALUES (3, N'Cookies')
GO
SET IDENTITY_INSERT [dbo].[category] OFF
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (1, CAST(N'2022-07-22T09:47:51.167' AS DateTime), N'GHTK', N'Taken product to ship', 0)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (1, CAST(N'2022-07-22T09:48:13.850' AS DateTime), N'GHTK', N'Product shipped to post office', 0)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (1, CAST(N'2022-07-22T10:02:07.957' AS DateTime), N'GHTK', N'Shipping to customer address', 0)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (1, CAST(N'2022-07-22T10:24:53.903' AS DateTime), N'', N'Order Complete Confirmed', 1)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (2, CAST(N'2022-07-22T10:02:08.033' AS DateTime), N'GHTK', N'Shipping to customer address', 0)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (2, CAST(N'2022-07-22T10:24:53.903' AS DateTime), N'', N'Order Complete Confirmed', 1)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (3, CAST(N'2022-07-22T10:02:08.037' AS DateTime), N'GHTK', N'Shipping to customer address', 0)
GO
INSERT [dbo].[delivery_status] ([order_item], [updated_time], [delivery_unit], [shipping_status], [shipping_completed]) VALUES (3, CAST(N'2022-07-22T10:24:53.903' AS DateTime), N'', N'Order Complete Confirmed', 1)
GO
SET IDENTITY_INSERT [dbo].[order] ON 
GO
INSERT [dbo].[order] ([id], [user_email], [user_name], [phone], [ship_address], [amount], [payment_method], [payment_status]) VALUES (5, N'customer@gmail.com', N'Nguyen Van Nghia', N'0963602512', N'Gần Nhà Hàng Hòa Lạc City, Thôn 6, Thạch Hòa, Thạch Thất, Hà Nội', 0.0000, 1, 0)
GO
INSERT [dbo].[order] ([id], [user_email], [user_name], [phone], [ship_address], [amount], [payment_method], [payment_status]) VALUES (7, N'customer@gmail.com', N'Nguyen Van Nghia', N'0963602512', N'Gần Nhà Hàng Hòa Lạc City, Thôn 6, Thạch Hòa, Thạch Thất, Hà Nội', 0.0000, 1, 0)
GO
INSERT [dbo].[order] ([id], [user_email], [user_name], [phone], [ship_address], [amount], [payment_method], [payment_status]) VALUES (8, N'customer@gmail.com', N'Nguyen Van Nghia', N'0789456111', N'So 3 Thang Loi', 7.0000, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[order] OFF
GO
SET IDENTITY_INSERT [dbo].[order_item] ON 
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (1, 5, N'Soft Cake', N'img/product-1.jpg', 1.0000, 6, CAST(N'2022-07-21T14:35:18.630' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (2, 5, N'Hard Cake', N'img/product-1.jpg', 2.0000, 3, CAST(N'2022-07-21T14:35:18.657' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (3, 5, N'Hard Bread', N'img/product-2.jpg', 2.0000, 1, CAST(N'2022-07-21T14:35:18.657' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (5, 7, N'Soft Cake', N'img/product-1.jpg', 1.0000, 6, CAST(N'2022-07-21T14:46:04.160' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (6, 7, N'Hard Cake', N'img/product-1.jpg', 2.0000, 3, CAST(N'2022-07-21T14:46:04.177' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (7, 7, N'Hard Bread', N'img/product-2.jpg', 2.0000, 1, CAST(N'2022-07-21T14:46:04.177' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (8, 8, N'Soft Cake', N'img/product-1.jpg', 1.0000, 1, CAST(N'2022-07-21T14:50:12.280' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (9, 8, N'Hard Cake', N'img/product-1.jpg', 2.0000, 2, CAST(N'2022-07-21T14:50:12.293' AS DateTime))
GO
INSERT [dbo].[order_item] ([id], [order_id], [product_name], [photo_link], [price], [quantity], [bought_date]) VALUES (10, 8, N'Hard Bread', N'img/product-2.jpg', 2.0000, 1, CAST(N'2022-07-21T14:50:12.293' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[order_item] OFF
GO
SET IDENTITY_INSERT [dbo].[payment_method] ON 
GO
INSERT [dbo].[payment_method] ([id], [method]) VALUES (1, N'Cash On Delivery')
GO
INSERT [dbo].[payment_method] ([id], [method]) VALUES (2, N'Paypal')
GO
INSERT [dbo].[payment_method] ([id], [method]) VALUES (3, N'Domestic Banking')
GO
SET IDENTITY_INSERT [dbo].[payment_method] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 
GO
INSERT [dbo].[product] ([id], [name], [detail], [photo_link], [price], [quantity], [category_id]) VALUES (1, N'Soft Cake', N'It is a soft cake', N'img/02-mercedes-amg-gt-r-c-190-wallpaper.jpg', 1.0000, 93, 1)
GO
INSERT [dbo].[product] ([id], [name], [detail], [photo_link], [price], [quantity], [category_id]) VALUES (2, N'Hard Cake', N'It is a hard cake', N'img/product-1.jpg', 2.0000, 95, 1)
GO
INSERT [dbo].[product] ([id], [name], [detail], [photo_link], [price], [quantity], [category_id]) VALUES (13, N'Hard Cokie', N'It is a hard cokie', N'img/09-mercedes-amg-gt-r-c-190-wallpaper.jpg', 1.0000, 0, 3)
GO
SET IDENTITY_INSERT [dbo].[product] OFF
GO
INSERT [dbo].[role] ([id], [name]) VALUES (1, N'Admin')
GO
INSERT [dbo].[role] ([id], [name]) VALUES (2, N'Staff')
GO
INSERT [dbo].[role] ([id], [name]) VALUES (3, N'Customer')
GO
INSERT [dbo].[user] ([email], [password], [phone], [name], [address], [age], [gender], [photo_link], [role_id]) VALUES (N'admin@gmail.com', N'123', NULL, N'Nguyen Van Nghia', NULL, NULL, NULL, NULL, 1)
GO
INSERT [dbo].[user] ([email], [password], [phone], [name], [address], [age], [gender], [photo_link], [role_id]) VALUES (N'customer@gmail.com', N'123', N'0789456111', N'Nguyen Van Nghia', N'Some where in Earth', 15, 1, N'img/IMG_0003.JPG', NULL)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__user__B43B145F5A2F0575]    Script Date: 7/23/2022 12:31:20 AM ******/
ALTER TABLE [dbo].[user] ADD UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[blog] ADD  DEFAULT ((1)) FOR [enable_status]
GO
ALTER TABLE [dbo].[delivery_status] ADD  DEFAULT ((0)) FOR [shipping_completed]
GO
ALTER TABLE [dbo].[blog]  WITH CHECK ADD FOREIGN KEY([owner])
REFERENCES [dbo].[user] ([email])
GO
ALTER TABLE [dbo].[cart_item]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[cart_item]  WITH CHECK ADD FOREIGN KEY([user_email])
REFERENCES [dbo].[user] ([email])
GO
ALTER TABLE [dbo].[delivery_status]  WITH CHECK ADD FOREIGN KEY([order_item])
REFERENCES [dbo].[order_item] ([id])
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD FOREIGN KEY([payment_method])
REFERENCES [dbo].[payment_method] ([id])
GO
ALTER TABLE [dbo].[order]  WITH CHECK ADD FOREIGN KEY([user_email])
REFERENCES [dbo].[user] ([email])
GO
ALTER TABLE [dbo].[order_item]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[order] ([id])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[product_quantity]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[product_quantity]  WITH CHECK ADD FOREIGN KEY([shop_id])
REFERENCES [dbo].[shop] ([id])
GO
ALTER TABLE [dbo].[shop]  WITH CHECK ADD FOREIGN KEY([staff_email])
REFERENCES [dbo].[user] ([email])
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([id])
GO
USE [master]
GO
ALTER DATABASE [BakingIngredients] SET  READ_WRITE 
GO
