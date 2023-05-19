using DataAccess;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageToSync_artistDetails : Page
{
    private readonly SQL sql = new SQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var data = sql.ExecuteStoredProcedureDT("[GetArtistDetails]");
            if (data != null)
            {
                rptArtistDetails.DataSource = data;
                rptArtistDetails.DataBind();

                errorMessage.Visible = false;
                items.Visible = true;
            }
        }
        catch
        {
            errorMessage.Visible = true;
            items.Visible = false;
        }
    }

    protected void rptArtist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var row = e.Item.DataItem as DataRowView;
            if (row != null)
            {
                int artistID = Convert.ToInt32(row["artistID"]);

                var rptAlbums = e.Item.FindControl("rptAlbums") as Repeater;
                rptAlbums.DataSource = GetArtistAlbum(artistID);
                rptAlbums.DataBind();


                var rptTopSongs = e.Item.FindControl("rptTopSongs") as Repeater;
                rptTopSongs.DataSource = GetTopSongs(artistID);
                rptTopSongs.DataBind();

            }
        }
    }

    private DataTable GetTopSongs(int artistID)
    {
        sql.Parameters.Clear();
        sql.Parameters.Add("@artistId", artistID);
        return sql.ExecuteStoredProcedureDT("GetArtistTopSongs");
    }
    private DataTable GetArtistAlbum(int artistID)
    {
        sql.Parameters.Clear();
        sql.Parameters.Add("@artistId", artistID);
        return sql.ExecuteStoredProcedureDT("GetArtistAlbum");
    }
}