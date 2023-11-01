CREATE PROCEDURE [dbo].[GetArtistDetails]
	@artistID INT 
AS
BEGIN
	SELECT Artist.biography AS artistBio, Artist.imageURL AS artistImage, Artist.heroURL AS artistheroUrl, Artist.title AS artistTitle,Artist.artistID AS artistId
	FROM Artist
	WHERE 
		Artist.artistID = @artistID
END