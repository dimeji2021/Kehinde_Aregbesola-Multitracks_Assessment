CREATE PROCEDURE [dbo].[GetArtistTopSongs]
	@artistID INT
AS
BEGIN
	SELECT TOP 3 Song.title AS songTitle, Song.bpm As songBpm, Album.title AS albumTitle, Album.imageURL AS albumImage
    FROM Song
    JOIN Album ON Song.albumID = Album.albumID
    WHERE Song.artistID = @artistID
    ORDER BY Song.dateCreation DESC
END