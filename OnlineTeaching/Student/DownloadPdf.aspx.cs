using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Student_DownloadPdf : System.Web.UI.Page
{
    DS_VIDEO.VIDEOMST_SELECTDataTable VDt = new DS_VIDEO.VIDEOMST_SELECTDataTable();
    DS_VIDEOTableAdapters.VIDEOMST_SELECTTableAdapter VAdapter = new DS_VIDEOTableAdapters.VIDEOMST_SELECTTableAdapter();
    DS_STAFF.STAFFMST_SELECTDataTable SDT = new DS_STAFF.STAFFMST_SELECTDataTable();
    DS_STAFFTableAdapters.STAFFMST_SELECTTableAdapter SAdapter = new DS_STAFFTableAdapters.STAFFMST_SELECTTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl.Text = "";
        if (Page.IsPostBack == false)
        {
            SDT = SAdapter.Select_By_Course(Session["cname"].ToString());
            drpteacher.DataSource = SDT;
            drpteacher.DataTextField = "Name";
            drpteacher.DataValueField = "sid";
            drpteacher.DataBind();
            drpteacher.Items.Insert(0, "SELECT");
            lblcourse.Text = Session["cname"].ToString();
            lblcourse0.Text = Session["cname"].ToString();
            VDt = VAdapter.Select_By_Course(lblcourse.Text);
            GvUpload.DataSource = VDt;
            GvUpload.DataBind();
            lbl.Text = "Total = " + GvUpload.Rows.Count.ToString();
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        VDt = VAdapter.Select_By_Staff(drpteacher.SelectedItem.Text);
        GvUpload.DataSource = VDt;
        GvUpload.DataBind();

        lbl.Text = "Total = " + GvUpload.Rows.Count.ToString();
    }
    protected void GvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "read")
        {
            VAdapter.VIDEOMST_DOWMLOAD(Convert.ToInt32(e.CommandArgument.ToString()));
            VDt = VAdapter.Select_By_ID(Convert.ToInt32(e.CommandArgument.ToString()));

            Response.Redirect(VDt.Rows[0]["Video"].ToString());
        }
        else if (e.CommandName == "download")
        {
            Response.Redirect(e.CommandArgument.ToString());
        }
    }
}