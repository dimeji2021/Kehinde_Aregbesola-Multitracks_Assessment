CREATE PROCEDURE [dbo].[GetArtistDetails]
AS
BEGIN
	SELECT Artist.biography AS artistBio, Artist.imageURL AS artistImage, Artist.heroURL AS artistheroUrl, Artist.title AS artistTitle,Artist.artistID AS artistId
	FROM Artist
	ORDER BY Artist.title DESC
END