select * from allvideo

select * from videonews

Drop table newsinfo

CREATE TABLE [dbo].[newsinfo] (
    [newsserial]  INT            IDENTITY (1, 1) NOT NULL,
    [title]       NVARCHAR (MAX) NOT NULL,
    [category]    NVARCHAR (200) NOT NULL,
    [description] NVARCHAR (MAX) NOT NULL,
    [author]      NVARCHAR (200) NULL,
    [datetime]    DATETIME       NOT NULL,
    [keyword]     NVARCHAR (MAX) NOT NULL,
	[captionPicture] nvarchar(max),
	[editor] nvarchar(100),
	[featureNews] nvarchar(100) 
    PRIMARY KEY CLUSTERED ([newsserial] ASC)
);







sp_help videonews

