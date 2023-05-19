using DataAccess;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PageToSync_songDetails : Page
{
    private readonly SQL _sql = new SQL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var _artistID = Convert.ToInt32(Request.QueryString["id"]);

            _sql.Parameters.Add("@artistID", _artistID);
            var data = _sql.ExecuteStoredProcedureDT("[GetArtist]");

            rptArtist.DataSource = data;
            rptArtist.DataBind();

            errorMessage.Visible = false;
            items.Visible = true;
        }
        catch (Exception)
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
                var _artistID = Convert.ToInt32(Request.QueryString["id"]);
                var rptSongs = e.Item.FindControl("rptSongs") as Repeater;
                rptSongs.DataSource = GetArtistSongs(_artistID);
                rptSongs.DataBind();
            }
        }
    }
    private DataTable GetArtistSongs(int artistID)
    {
        _sql.Parameters.Clear();
        _sql.Parameters.Add("@artistID", artistID);
        return _sql.ExecuteStoredProcedureDT("[GetArtistSong]");
    }
}