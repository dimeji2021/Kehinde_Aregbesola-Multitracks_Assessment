using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageToSync_artistDetails2 : System.Web.UI.Page
{
    private readonly SQL sql = new SQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var _artistID = Convert.ToInt32(Request.QueryString["id"]);

            if (_artistID <= 0)
            {
                Response.Redirect("artistDetails.aspx?id=1");
            }

            sql.Parameters.Add("@artistID", _artistID);
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