CREATE PROCEDURE [dbo].[GetArtistSong]
	@artistID INT
AS
BEGIN
	SELECT Song.title, Song.bpm, Album.title AS albumTitle, Album.imageURL AS albumImage
    FROM Song
    JOIN Album ON Song.albumID = Album.albumID
    WHERE Song.artistID = @artistID
    ORDER BY Song.dateCreation DESC
END