CREATE PROCEDURE [dbo].[GetArtistAlbum]

@artistID INT

AS
BEGIN

	SELECT Album.imageURL AS albumImageURL, Album.title AS albumTitle, Artist.title AS artistTitles
	FROM Album
	JOIN Artist ON Album.artistID = Artist.artistID
	WHERE Album.artistID = @artistID
	ORDER BY Album.dateCreation DESC
		
END