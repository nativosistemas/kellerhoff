USE [db_Kellerhoff]
GO

/****** Object:  Table [dbo].[tmp_Solicitudes_Devoluciones_Items]    Script Date: 06/08/2020 14:43:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tmp_Solicitudes_Devoluciones_Items](
	[dev_numeroitem] [int] IDENTITY(1,1) NOT NULL,
	[dev_numerocliente] [int] NOT NULL,
	[dev_numerofactura] [nvarchar](13) NULL,
	[dev_nombreproductodevolucion] [nvarchar](75) NOT NULL,
	[dev_fecha] [datetime] NOT NULL,
	[dev_motivo] [int] NOT NULL,
	[dev_numeroitemfactura] [int] NULL,
	[dev_nombreproductofactura] [nvarchar](75) NULL,
	[dev_cantidad] [int] NOT NULL,
	[dev_numerolote] [nvarchar](75) NULL,
	[dev_fechavencimientolote] [date] NULL,
	[dev_idsucursal] [nvarchar](2) NOT NULL,
 CONSTRAINT [PK_tmp_Solicitudes_Devoluciones_Items] PRIMARY KEY CLUSTERED 
(
	[dev_numeroitem] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tmp_Solicitudes_Devoluciones_Items] ADD  CONSTRAINT [DF_tmp_Solicitudes_Devoluciones_Items_dev_idsucursal]  DEFAULT ('CC') FOR [dev_idsucursal]
GO

