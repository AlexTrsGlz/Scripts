using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Globalization;
using System.Threading;
using SupplyTrack.Objects;
using System.Resources;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Services;
using SupplyTrack.Objects.Others;
using SupplyTrack.BLL;
using SupplyTrack.Exceptions;
using SupplyTrack.Tools;

namespace SupplyTrack
{
    public class SupplyTrackPage : Page
    {
        protected bool popUp = false;

        protected bool PopUp
        {
            get { return popUp; }
            set { popUp = value; }
        }

        protected bool FullScreen
        {
            get { return (Request.QueryString["fullscreen"] ?? "") == "1"; }
        }



        protected bool ReadOnly
        {
            get { return (Request.QueryString["ReadOnly"] ?? "") == "1"; }
        }

        protected override void OnError(EventArgs e)
        {
            SupplyTrackUnhandledException currentError = null;
            try
            {
                Session["LastPageURL"] = Request.Url.AbsoluteUri;
                if (!(Server.GetLastError() is SupplyTrackUnhandledException) &&
                    Server.GetLastError() != null)
                    currentError = new SupplyTrackUnhandledException(Server.GetLastError(), ApplicationSettings.ConnectionString, getLoginInfo().Login, Request.Url.AbsoluteUri);
                else
                    if (Server.GetLastError() != null)
                        currentError = Server.GetLastError() as SupplyTrackUnhandledException;
            }
            catch
            {                
                Response.Redirect("~/WEB/Forms/Pag_Error.aspx" + (PopUp ? "?MasterType=pop" :  string.Empty), true);
            }

            if (currentError != null)
            {
                Response.Redirect("~/WEB/Forms/Pag_Error.aspx?IdError=" + currentError.IdError + (PopUp ? "&MasterType=pop" : string.Empty), true);
            }
        }

        protected override void InitializeCulture()
        {
            base.InitializeCulture();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture((string)Session["culture"]);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo((string)Session["culture"]);
        }

        protected override void  OnPreInit(EventArgs e)
        {
            if (Request.QueryString["MasterType"] != null)
            {
                switch (Request.QueryString["MasterType"])
                {
                case "pop":
                    MasterPageFile = "~/web/masters/pop_MasterDefault.Master";
                    PopUp = true;
                    break;
                default:
                    MasterPageFile = "~/web/masters/MasterDefault.Master";
                    break;
                }
            }
            if ((Request.QueryString["PopUp"] ?? "") == "1") PopUp = true;
 	        base.OnPreInit(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (PopUp)
            {
                Control btnClose = Master.FindControl("ContentPlaceHolder1").FindControl("btnClose") as Control;
                if (btnClose != null) btnClose.Visible = true;
            }
            if (ReadOnly)
            {
                Control btnNew = Master.FindControl("ContentPlaceHolder1").FindControl("btnNew") as Control;
                Control btnSearch = Master.FindControl("ContentPlaceHolder1").FindControl("btnSearch") as Control;
                Control btnApply = Master.FindControl("ContentPlaceHolder1").FindControl("btnApply") as Control;
                Control btnDelete = Master.FindControl("ContentPlaceHolder1").FindControl("btnDelete") as Control;
                if (btnNew != null) btnNew.Visible = false;
                if (btnSearch != null) btnSearch.Visible = false;
                if (btnApply != null) btnApply.Visible = false;
                if (btnDelete != null) btnDelete.Visible = false;
            }
            base.OnPreRender(e);
        }

        public static LoginInfo getLoginInfo()
        {
            LoginInfo li = (LoginInfo)(HttpContext.Current.Session["objLoginInfo"]);
            return li;
        }

        protected virtual string getSupplyTrackErrorMessage(string error)
        {
            return (Resources.Errors.ResourceManager.GetString(error) ?? //Errores en recursos "nuevos" (globales).
                (new ResourceManager(ConfigurationSettings.AppSettings["AppStrings"], Assembly.GetExecutingAssembly()).GetString(error) ?? //Errores en recursos "viejos".
                error));
        }

        [WebMethod]
        public static string getSupplyTrackErrorMessageWebMethod(string error)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture((string)HttpContext.Current.Session["Culture"]);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo((string)HttpContext.Current.Session["Culture"]);

            return (Resources.Errors.ResourceManager.GetString(error) ?? //Errores en recursos "nuevos" (globales).
                (new ResourceManager(ConfigurationSettings.AppSettings["AppStrings"], Assembly.GetExecutingAssembly()).GetString(error) ?? //Errores en recursos "viejos".
                error));
        }
        /// <summary>
        /// Merge rows with the same text
        /// </summary>
        /// <param name="gridView">Control</param>
        /// <param name="cols">Number of columns separate by commas</param>
        protected static void MergeRows(GridView gridView, string cols)
        {
            string[] arrCols = cols.Split(',');
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < arrCols.Length; i++)
                {
                    int colIndex = int.Parse(arrCols[i].ToString());
                    if (row.Cells[colIndex].Text == previousRow.Cells[colIndex].Text)
                    {
                        row.Cells[colIndex].RowSpan = previousRow.Cells[colIndex].RowSpan < 2 ? 2 : previousRow.Cells[colIndex].RowSpan + 1;
                        previousRow.Cells[colIndex].Visible = false;
                    }
                }
            }

            Color backColor = gridView.RowStyle.BackColor;
            for (int i = 0; i < arrCols.Length - 1; i++)
            {
                int colIndex = int.Parse(arrCols[i].ToString());
                for (int rowIndex = 0; rowIndex < gridView.Rows.Count ; rowIndex++)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    if (row.Cells[colIndex].Visible)
                    {
                        row.Cells[0].BackColor = backColor;
                        backColor = (backColor == gridView.RowStyle.BackColor ? gridView.AlternatingRowStyle.BackColor : gridView.RowStyle.BackColor);
                    }
                }
            }
        }
        
        protected static void MergeRowsCtl(GridView gridView, string cols,string ctl)
        {
            string[] arrCols = cols.Split(',');
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < arrCols.Length; i++)
                {
                    int colIndex = int.Parse(arrCols[i].ToString());

                    if (((LinkButton)row.Cells[colIndex].FindControl(ctl)).Text == ((LinkButton)previousRow.Cells[colIndex].FindControl(ctl)).Text 
                        && row.Cells[1].Text == previousRow.Cells[1].Text  )
                    {
                        row.Cells[colIndex].RowSpan = previousRow.Cells[colIndex].RowSpan < 2 ? 2 : previousRow.Cells[colIndex].RowSpan + 1;
                        previousRow.Cells[colIndex].Visible = false;
                    }
                }
            }

            Color backColor = gridView.RowStyle.BackColor;
            for (int i = 0; i < arrCols.Length - 1; i++)
            {
                int colIndex = int.Parse(arrCols[i].ToString());
                for (int rowIndex = 0; rowIndex < gridView.Rows.Count; rowIndex++)
                {
                    GridViewRow row = gridView.Rows[rowIndex];
                    if (row.Cells[colIndex].Visible)
                    {
                        row.Cells[0].BackColor = backColor;
                        backColor = (backColor == gridView.RowStyle.BackColor ? gridView.AlternatingRowStyle.BackColor : gridView.RowStyle.BackColor);
                    }
                }
            }
        }

        protected static String WordWrap(String str, int charactersPerLine)
        {
            String newStr = string.Empty;

            if (str.Length > charactersPerLine)
            {
                int i = 0;
                foreach (char caracter in str)
                {
                    i++;
                    
                    newStr = newStr + caracter;

                    if (caracter == ' ')
                        i = 0;
                    else
                    {
                        if (i == charactersPerLine)
                        {
                            i = 0;
                            newStr = newStr + " ";
                        }
                    }
                }
            }
            else
                newStr = str;

            return newStr;
        }

		public void ResponseRedirect(string url)
		{
			Context.Response.Redirect(url, false);
			Context.ApplicationInstance.CompleteRequest();
		}

        /// <summary>
        /// This method checks Page controls and avoid Doble Postback on strIdButton 
        /// </summary>
        /// <param name="parent"></param>
        public void IterateThroughControlsToAvoidDoublePostback(Control parent, string strIdButton, string additionalScript = null)
        {
            foreach (Control SelectedButton in parent.Controls)
            {
                if (SelectedButton is Button && SelectedButton.ID == strIdButton)
                {
                    string script = (additionalScript ?? string.Empty);

                    if( string.IsNullOrEmpty(additionalScript)) // evita hacer postback durante el evento click
                        script = "if (this.working) { return false; } else { this.working=true; }; ";

                    ((Button)SelectedButton).Attributes.Add("onclick", script + Page.ClientScript.GetPostBackEventReference(((Button)SelectedButton), null) + ";");
                }

                if (SelectedButton.Controls.Count > 0)
                {
                    IterateThroughControlsToAvoidDoublePostback(SelectedButton, strIdButton, additionalScript);
                }
            }
        }

        /// <summary>
        /// This method Finds a Control by Id from a Control Collection and Fills a List
        /// </summary>
        /// <param name="Page"></param>
        /// <param name="p_FoundControl"></param>
        /// <param name="ID"></param>
        public void FindControlByID(ControlCollection Page, IList<Control> p_FoundControl, string ID)
        {
            foreach (Control ctrl in Page)
            {
                if (ctrl != null)
                {
                    if (ctrl.ID == ID) // If Control matches with Specified ID
                    {
                        p_FoundControl.Add(ctrl);
                        break;
                    }

                    if (ctrl.HasControls()) // If Control has children, searchs again
                    {
                        FindControlByID(ctrl.Controls, p_FoundControl, ID);
                    }
                }
            }
        }
    }
}
