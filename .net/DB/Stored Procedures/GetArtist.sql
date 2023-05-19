CREATE PROCEDURE [dbo].[GetArtist]
	@artistID INT
AS
BEGIN
	SELECT *
	FROM Artist
	WHERE 
		Artist.artistID = @artistID
END