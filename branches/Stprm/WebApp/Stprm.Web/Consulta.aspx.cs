using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stprm.DataEx;

namespace Stprm.Web
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _bf_buscar.Busqueda += new DataEx.TrabajadorEventHander(_bf_buscar_Busqueda);
            _btn_contratos.Click += new EventHandler(_btn_contratos_Click);
            _btn_escalafon.Click += new EventHandler(_btn_escalafon_Click);
            _btn_militancia.Click += new EventHandler(_btn_militancia_Click);

            _it_planta.SeleccionarButton.Click += new EventHandler(it_plantaSeleccionarButton_Click);
            _it_transitorio.SeleccionarButton.Click += new EventHandler(it_transitorioSeleccionarButton_Click);
        }

        void it_transitorioSeleccionarButton_Click(object sender, EventArgs e)
        {
            _it_transitorio.SetSeleccionar(true);
            _it_planta.SetSeleccionar(false);

            FichaActual = _it_transitorio.Ficha.Text;
            CargarPantallaActual();
        }

        private void it_plantaSeleccionarButton_Click(object sender, EventArgs e)
        {
            _it_planta.SetSeleccionar(true);
            _it_transitorio.SetSeleccionar(false);

            FichaActual = _it_planta.Ficha.Text;
            CargarPantallaActual ();
        }

        void _btn_militancia_Click(object sender, EventArgs e)
        {
            string ficha = FichaActual;
            PantallaActual = "militancia";

            SelectButton(_btn_militancia);

            if (ficha != string.Empty)
                using (BaseDatos bd = BaseDatos.CreateStprmConnection())
                {
                    Trabajador trab = new Trabajador(bd);
                    trab.Ficha = ficha;
                    if (trab.Actualizar())
                        CargarMilitancia(trab);
                }
        }

        void _btn_escalafon_Click(object sender, EventArgs e)
        {
            string ficha = FichaActual;
            PantallaActual = "escalafon";
            
            SelectButton(_btn_escalafon);
            
            if (ficha != string.Empty)
                using (BaseDatos bd = BaseDatos.CreateStprmConnection())
                {
                    Trabajador trab = new Trabajador(bd);
                    trab.Ficha = ficha;
                    if (trab.Actualizar())
                        CargarEscalafon(trab);
                }
        }

        void _btn_contratos_Click(object sender, EventArgs e)
        {
            string ficha = FichaActual;
            PantallaActual = "contratos";

            SelectButton(_btn_contratos);

            if (ficha != string.Empty)
            using (BaseDatos bd = BaseDatos.CreateStprmConnection())
            {
                Trabajador trab = new Trabajador(bd);
                trab.Ficha = ficha;
                if (trab.Actualizar ())
                    CargarContratos(trab);
            }
        }

        private void _bf_buscar_Busqueda(object sender, DataEx.TrabajadorEventArgs args)
        {
            Trabajador trab = args.Trabajador;
            _it_planta.Setear("N/A");
            _it_transitorio.Setear("N/A");

            _it_planta.ActualizarDesdeTrabajador(trab);

            if (trab.RegimenContractual == "PS")
            {
                Trabajador recomendado;
                string parentesco;
                if (trab.GetRecomendado(out recomendado, out parentesco))
                {
                    _it_transitorio.ActualizarDesdeTrabajador (recomendado);
                }
            }

            it_plantaSeleccionarButton_Click(this, EventArgs.Empty);
        }

        private void SelectButton(Button button)
        {
            _btn_contratos.CssClass = "button_unselected";
            _btn_escalafon.CssClass = "button_unselected";
            _btn_militancia.CssClass = "button_unselected";

            button.CssClass = "button_selected";
        }

        private BoundField NewBoundField(string datafield, string headertext)
        {
            BoundField field = new BoundField();
            field.DataField = datafield;
            field.HeaderText = headertext;

            return field;
        }

        public void CargarPantallaActual()
        {
            switch (PantallaActual)
            {
                case "escalafon":
                    _btn_escalafon_Click(this, EventArgs.Empty);
                break;

                case "militancia":
                    _btn_militancia_Click(this, EventArgs.Empty);
                break;

                default:
                    _btn_contratos_Click(this, EventArgs.Empty);
                break;
            }
        }

        public void CargarContratos(Trabajador trabajador)
        {
            _gv_contratos.Columns.Clear();

            _gv_contratos.Columns.Add (NewBoundField ("Plaza", "Plaza"));
            _gv_contratos.Columns.Add (NewBoundField ("Folio", "Folio"));
            _gv_contratos.Columns.Add (NewBoundField ("Categoria", "Categoría"));
            _gv_contratos.Columns.Add (NewBoundField ("Inicio", "Inicio"));
            _gv_contratos.Columns.Add (NewBoundField ("Termino", "Término"));
            _gv_contratos.Columns.Add (NewBoundField ("Terminacion", "Terminación"));
            _gv_contratos.Columns.Add (NewBoundField("Dias", "Días"));

            DataSet ds = new DataSet();
            trabajador.GetContratosInAdapter().Fill(ds);

            _gv_contratos.DataSource = ds;
            _gv_contratos.DataBind();
        }

        public void CargarEscalafon(Trabajador trabajador)
        {
            _gv_contratos.Columns.Clear();

            _gv_contratos.Columns.Add(NewBoundField("Posicion", "#"));
            _gv_contratos.Columns.Add(NewBoundField("Ficha", "Ficha"));
            _gv_contratos.Columns.Add(NewBoundField("Nombre", "Nombre"));
            _gv_contratos.Columns.Add(NewBoundField("Plaza", "Plaza"));
            _gv_contratos.Columns.Add(NewBoundField("Categoria", "Categoría"));
            _gv_contratos.Columns.Add(NewBoundField("Clasificacion", "Clasificación"));
            //_gv_contratos.Columns.Add(NewBoundField("Escalafon", "Escalafón"));
            _gv_contratos.Columns.Add(NewBoundField("Depto", "Depto"));
            

            DataSet ds = new DataSet ();

            trabajador.GetEscalafonInAdapter ().Fill (ds);
            _gv_contratos.DataSource = ds;
            _gv_contratos.DataBind();
        }

        public void CargarMilitancia(Trabajador trabajador)
        {
            _gv_contratos.Columns.Clear();

            _gv_contratos.Columns.Add(NewBoundField("Evento", "Evento"));
            _gv_contratos.Columns.Add(NewBoundField("Lugar", "Lugar"));
            _gv_contratos.Columns.Add(NewBoundField("Fecha", "Fecha"));
            _gv_contratos.Columns.Add(NewBoundField("Tipo_apoyo", "Tipo de apoyo"));

            DataSet ds = new DataSet();

            trabajador.GetMilitanciaInAdapter().Fill(ds);

            _gv_contratos.DataSource = ds;
            _gv_contratos.DataBind();
        }

        public string FichaActual
        {
            get { return ViewState["FichaActual"] == null ? string.Empty : ViewState["FichaActual"].ToString(); }
            set { ViewState["FichaActual"] = value; }
        }

        public string PantallaActual
        {
            get { return ViewState["PantallaActual"] == null ? string.Empty : ViewState["PantallaActual"].ToString(); }
            set { ViewState["PantallaActual"] = value; }
        }
    }
}