using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Staff_UploadVideo : System.Web.UI.Page
{
    DS_VIDEO.VIDEOMST_SELECTDataTable VDT = new DS_VIDEO.VIDEOMST_SELECTDataTable();
    DS_VIDEOTableAdapters.VIDEOMST_SELECTTableAdapter VAdapter = new DS_VIDEOTableAdapters.VIDEOMST_SELECTTableAdapter();
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl.Text = "";
        lblsave.Text = "";
        if (Page.IsPostBack == false)
        {
            VDT = VAdapter.Select_By_Staff(Session["name"].ToString());
            GridView4.DataSource = VDT;
            GridView4.DataBind();
            lbl.Text = "Total = " + GridView4.Rows.Count.ToString();
        }
    }
    protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "a")
        {
            VDT = VAdapter.Select_By_ID(Convert.ToInt32(e.CommandArgument.ToString()));

            if (VDT.Rows[0]["Status"].ToString() == "Active")
            {
                VAdapter.VIDEOMST_UPDATE_STATUS(Convert.ToInt32(e.CommandArgument.ToString()), "InActive");

            }
            else
            {
                VAdapter.VIDEOMST_UPDATE_STATUS(Convert.ToInt32(e.CommandArgument.ToString()), "Active");
            }
            VDT = VAdapter.Select_By_Staff(Session["name"].ToString());
            GridView4.DataSource = VDT;
            GridView4.DataBind();
            lbl.Text = "Total = " + GridView4.Rows.Count.ToString();
        }
    }
    protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        VAdapter.Delete(Convert.ToInt32(GridView4.DataKeys[e.RowIndex].Value));
        VDT = VAdapter.Select();
        GridView4.DataSource = VDT;
        GridView4.DataBind();
        lbl.Text = "Total = " + GridView4.Rows.Count.ToString();
    }
    protected void Button12_Click(object sender, EventArgs e)
    {

        //using (BinaryReader br = new BinaryReader(FileUpload1.PostedFile.InputStream))
      //  {
           // byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);
            //string strConnString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
          //  using (SqlConnection con = new SqlConnection(strConnString))
           // {
              //  using (SqlCommand cmd = new SqlCommand())
               // {
                    //cmd.CommandText = "insert into tblFiles(Name, ContentType, Data) values (@Name, @ContentType, @Data)";
                    //cmd.Parameters.AddWithValue("@Name", Path.GetFileName(FileUpload1.PostedFile.FileName));
                    //cmd.Parameters.AddWithValue("@ContentType", "video/mp4");
                    //cmd.Parameters.AddWithValue("@Data", bytes);
                    //cmd.Connection = con;
                    //con.Open();
                    //cmd.ExecuteNonQuery();
                    //con.Close();
               // }
           // }

           // string mm = Path.GetFileName(FileUpload1.PostedFile.FileName);
            
                    if (FileUpload1.HasFile)
                   {
                        FileUpload1.SaveAs(Server.MapPath("~/Staff/Pdf/") + FileUpload1.FileName);
                        string abc = FileUpload1.FileName.ToString();
                      //  byte[] bytes = br.ReadBytes((int)FileUpload1.PostedFile.InputStream.Length);

                        VAdapter.Insert(Session["name"].ToString(), Session["cname"].ToString(), txttitle.Text,"~/Staff/Pdf/" + FileUpload1.FileName);
                        lblsave.Text = "PDF File Uploaded";
                        txttitle.Text = "";

                        VDT = VAdapter.Select_By_Staff(Session["name"].ToString());
                        GridView4.DataSource = VDT;
                        GridView4.DataBind();
                        lbl.Text = "Total = " + GridView4.Rows.Count.ToString();
                   }
                   else
                   {
                       lblsave.Text = "Please, Select PDF File.";
                   }
      //  }
       // Response.Redirect(Request.Url.AbsoluteUri);
    }
}