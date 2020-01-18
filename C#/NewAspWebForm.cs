using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Resources;
using System.Configuration;
using System.Reflection;

namespace MyProyect.WEB.Forms
{
	public partial class Pag_ClassWebForm : MyProyect.MyProyectPage
	{
		#region DesctiptoresDeAcceso
		protected ResourceManager LocRM;
		private ClassBLL _objClassBLL;
		private LoginInfo _li;
		
		protected ClassBLL ObjClassBLL
		{
			get
			{
				if (_objClassBLL == null)
				{ _objClassBLL = new ClassBLL(); }
				return _objClassBLL;
			}
			set
			{ _objClassBLL = value; }
		}

		protected LoginInfo Li
		{
			get 
			{
				if (_li == null)
				{ _li = getLoginInfo(); }
				return _li; 
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			LocRM = new ResourceManager(ConfigurationManager.AppSettings["AppStrings"], Assembly.GetExecutingAssembly());
			if (!IsPostBack)
			{
				//Validar acceso al módulo
				if (!(new SecurityBLL(Li)).IsInRole("NumeroPermiso")) Response.Redirect("~/SinAcceso.aspx");

				LimpiarCampos();
				ModoBusqueda();
			}
		}
		#region Events
		/// <summary>
		/// New Surcharge Rate
		/// </summary>
		protected void BtnNewPage_Click(object sender, EventArgs e)
		{
			try
			{
				LimpiarCampos();
				ModoCreacion();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
                ////////////Se guarda un LOG ///////////////////////////////
				LogActionBLL objLogActionBLL = new LogActionBLL(Li);
				objLogActionBLL.InsertLogAction(3, String.Format("Se Insertó la tasa '{0}' del mes '{1}' del año '{2}'.", objTR.Tasa, objTR.StrgMonth, objTR.Property), objTR.IdClassWebForm);
				////////////////////////////////////////////////////////////////
			}
		}

		/// <summary>
		/// Botón para pasar a modo de Búsqueda
		/// </summary>
		protected void BtnSearchPage_Click(object sender, EventArgs e)
		{
			try
			{
				LimpiarCampos();
				ModoBusqueda();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Boton para limpiar campos
		/// </summary>
		protected void BtnClearFields_Click(object sender, EventArgs e)
		{
			try
			{
				LimpiarCampos();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Botón de aplicar
		/// </summary>
		protected void BtnApply_Click(object sender, EventArgs e)
		{
			try
			{
				/////////////////////////////////////////////////////////////////////////////////
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Botón de busqueda
		/// </summary>
		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				/////////////////////////////////////////////////////////////////////////////////
				lblError.Text = string.Empty;
			}
			catch(Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Este método ocurre al borrar un campo del grid de GenericGrid
		/// </summary>
		protected void GenericGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			try
			{
				dlgConfirmDelete.Visible = true;
				dlgConfirmDelete.VisibleOnLoad = true;
				UpdDelete.Update();
				hdnIdClassWebFormDelete.Value = e.Keys["IdClassWebForm"].ToString();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Botón de confirmacion de eliminar
		/// </summary>
		protected void BtnConfirmDelete_Click(object sender, EventArgs e)
		{
			try
			{
                /////////////////////////////////////////////////////////////////////////////////
				lblError.Text = string.Empty;
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Botón de cancelar eliminar
		/// </summary>
		protected void BtnCancelDelete_Click(object sender, EventArgs e)
		{
			try
			{
				dlgConfirmDelete.Visible = false;
				dlgConfirmDelete.VisibleOnLoad = false;
				UpdDelete.Update();
				hdnIdClassWebFormDelete.Value = "-1";
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Este metodo ocure al cambiar de página en el grid de recargos
		/// </summary>
		protected void GenericGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			try
			{
				IList<ClassWebForm> lstTR = new List<ClassWebForm>();
				lstTR = getListClassWebForm();
				GenericGrid.DataSource = lstTR;
				GenericGrid.PageIndex = e.NewPageIndex;
				GenericGrid.DataBind();
				UpdGenericGrid.Update();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Este metodo ocurre al editar un registro
		/// </summary>
		protected void GenericGrid_RowEditing(object sender, GridViewEditEventArgs e)
		{
			try
			{
				IList<ClassWebForm> lstTR = new List<ClassWebForm>();
				lstTR = getListClassWebForm();
				GenericGrid.DataSource = lstTR;
				GenericGrid.EditIndex = e.NewEditIndex;
				GenericGrid.DataBind();
				UpdGenericGrid.Update();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Este método ocurre al cancelar la edicion
		/// </summary>
		protected void GenericGrid_RowCancelingEditing(object sender, GridViewCancelEditEventArgs e)
		{
			try
			{
				IList<ClassWebForm> lstTR = new List<ClassWebForm>();
				lstTR = getListClassWebForm();
				GenericGrid.DataSource = lstTR;
				GenericGrid.EditIndex = -1;
				GenericGrid.DataBind();
				UpdGenericGrid.Update();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}

		/// <summary>
		/// Este metodo entra al actualizar el registro
		/// </summary>
		protected void GenericGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			try
			{
				ClassWebForm objTRUpdate = new ClassWebForm();
				objTRUpdate.IdClassWebForm = Convert.ToInt32(GenericGrid.DataKeys[e.RowIndex].Values[0]);
				objTRUpdate.Property = Convert.ToDecimal(((TextBox)GenericGrid.Rows[e.RowIndex].Cells[(int)grdClassWebForm.Tasa].FindControl("ControlASPX")).Text);
				ObjClassBLL.UpdateClassWebForm(objTRUpdate);
				IList<ClassWebForm> lstTR = new List<ClassWebForm>();
				lstTR = getListClassWebForm();
				GenericGrid.DataSource = lstTR;
				GenericGrid.EditIndex = -1;
				GenericGrid.DataBind();
				UpdGenericGrid.Update();
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
				lblError.Visible = true;
			}
		}
		#endregion

		#region Methods

		/// <summary>
		/// Este metodo elimina toda la información de todos los campos en el formulario y oculta el grid
		/// </summary>
		private void LimpiarCampos()
		{
			//ERROR SECTION

			//Controles
			LoadDDLMonths();
			LoadGenericGrid();
			UpdControls.Update();

			//Hidden
		}

		/// <summary>
		/// Este método deja en blanco el grid de ClassWebForm y lo oculta
		/// </summary>
		private void LoadGenericGrid()
		{
			IList<ClassWebForm> lstTR = new List<ClassWebForm>();
			lstTR.Add(new ClassWebForm());
			GenericGrid.DataSource = null;
			GenericGrid.DataSource = lstTR;
			GenericGrid.DataBind();
			GenericGrid.Visible = false;
			GenericGrid.Rows[0].Visible = false;
			UpdGenericGrid.Update();
		}

		/// <summary>
		/// Este método carga la información del grid de Meses
		/// </summary>
		private void LoadDDLMonths()
		{
			CatalogoBLL objCBLL = new CatalogoBLL(Li);
			IList<Catalogo> lstMonths = new List<Catalogo>();
			lstMonths = objCBLL.SelectMonths(Li.Culture.ToString());

			ddlMonths.Items.Clear();
			ddlMonths.SelectedIndex = -1;
			ddlMonths.SelectedValue = null;
			ddlMonths.ClearSelection();
			ddlMonths.DataSource = lstMonths.OrderBy(item => item.StringVar1);
			ddlMonths.DataTextField = "Nombre";
			ddlMonths.DataValueField = "StringVar1";
			ddlMonths.DataBind();
			ddlMonths.Items.Insert(0, "(---)");
			ddlMonths.Items[0].Value = "-1";
			ddlMonths.SelectedValue = "-1";
		}

		/// <summary>
		///	Este método se utiliza para mostrar el botón de busqueda y deshabilitar los validadores
		/// </summary>
		private void ModoBusqueda()
		{
			//TITLE SECTION

			//CONTROLS SECTION		

			//ERROR SECTION

			//Hidden
		}

		/// <summary>
		/// Este método se utiliza para mostrar el boton de "New" y habilitar los validadores
		/// </summary>
		private void ModoCreacion()
		{
			//TITLE SECTION

			//CONTROLS SECTION
			
			//ERROR SECTION

			//Hidden
		}

		/// <summary>
		/// Obtiene la lista de tasa recargo utilizando los controles del formulario como parametros de búsqueda
		/// </summary>
		/// <returns></returns>
		private IList<ClassWebForm> getListClassWebForm()
		{
			ClassWebForm objTRFinder = new ClassWebForm();
			IList<ClassWebForm> lstTR = new List<ClassWebForm>();
			objTRFinder = ReadHiddenControls();
			lstTR = ObjClassBLL.SelectClassWebForm(objTRFinder);

			//////////Si no se obtiene ningún resultado, no regresa nada////////
			if (lstTR == null || lstTR.Count == 0)
			{
				lstTR = new List<ClassWebForm>();
				lblError.Text = Resources.MyProyect.NullSearch;
				lblError.Visible = true;
			}
			return lstTR;
		}

		/// <summary>
		/// Método para leer los controles de formulario, validando sus respectivos campos
		/// </summary>
		/// <returns></returns>
		private ClassWebForm ReadControls()
		{
			ClassWebForm objClassWebForm = new ClassWebForm();
			int intergerProperty = -1;
			int		intergerMonth = -1;
			decimal decimalTasa   = -1;
			///////////Valida todos los controles para leerlos, si ocurre error al convertir, asigna -1
			objClassWebForm.Property = (int.TryParse(txtProperty.Text, out intergerProperty)) ? intergerProperty : -1;
			////////////////////////////////////////////////
			return objClassWebForm;
		}

		/// <summary>
		/// Método para leer los controles de Hidden, validando sus respectivos campos
		/// </summary>
		/// <returns></returns>
		private ClassWebForm ReadHiddenControls()
		{
			ClassWebForm objClassWebForm = new ClassWebForm();
			int intergerProperty = -1;
			///////////Valida todos los Hidden para leerlos, si ocurre error al convertir, asigna -1
			objClassWebForm.Property = (int.TryParse(hdnProperty.Value, out intergerProperty)) ? intergerProperty : -1;
			////////////////////////////////////////////////
			return objClassWebForm;
		}
		#endregion
	}
}