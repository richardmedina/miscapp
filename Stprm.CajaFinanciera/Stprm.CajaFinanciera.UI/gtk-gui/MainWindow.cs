// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------



public partial class MainWindow {
    
    private Gtk.UIManager UIManager;
    
    private Gtk.Action ArchivoAction;
    
    private Gtk.Action quitAction;
    
    private Gtk.Action AyudaAction;
    
    private Gtk.Action helpAction;
    
    private Gtk.Action homeAction;
    
    private Gtk.Action missingImageAction;
    
    private Gtk.Action aboutAction;
    
    private Gtk.VBox vbox1;
    
    private Gtk.MenuBar menubar1;
    
    private Gtk.HBox hbox1;
    
    private Gtk.EventBox _eb_toolbar;
    
    private Gtk.VBox vbox3;
    
    private Gtk.EventBox _eb_cuentas;
    
    private Gtk.VBox vbox2;
    
    private Gtk.EventBox _eb_search;
    
    private Gtk.EventBox _main_container;
    
    private Gtk.Statusbar statusbar;
    
    private Gtk.EventBox _eb_msg;
    
    private Gtk.Label _lbl_msg;
    
    private Gtk.ProgressBar _prg_progress;
    
    protected virtual void Build() {
        Stetic.Gui.Initialize(this);
        // Widget MainWindow
        this.UIManager = new Gtk.UIManager();
        Gtk.ActionGroup w1 = new Gtk.ActionGroup("Default");
        this.ArchivoAction = new Gtk.Action("ArchivoAction", Mono.Unix.Catalog.GetString("_Archivo"), null, null);
        this.ArchivoAction.ShortLabel = Mono.Unix.Catalog.GetString("_Archivo");
        w1.Add(this.ArchivoAction, null);
        this.quitAction = new Gtk.Action("quitAction", Mono.Unix.Catalog.GetString("_Quit"), null, "gtk-quit");
        this.quitAction.ShortLabel = Mono.Unix.Catalog.GetString("_Quit");
        w1.Add(this.quitAction, null);
        this.AyudaAction = new Gtk.Action("AyudaAction", Mono.Unix.Catalog.GetString("_Ayuda"), null, null);
        this.AyudaAction.ShortLabel = Mono.Unix.Catalog.GetString("_Ayuda");
        w1.Add(this.AyudaAction, null);
        this.helpAction = new Gtk.Action("helpAction", Mono.Unix.Catalog.GetString("_Temas de ayuda..."), null, "gtk-help");
        this.helpAction.ShortLabel = Mono.Unix.Catalog.GetString("_Temas de ayuda...");
        w1.Add(this.helpAction, null);
        this.homeAction = new Gtk.Action("homeAction", Mono.Unix.Catalog.GetString("_Sitio del proyecto"), null, "gtk-home");
        this.homeAction.ShortLabel = Mono.Unix.Catalog.GetString("_Sitio del proyecto");
        w1.Add(this.homeAction, null);
        this.missingImageAction = new Gtk.Action("missingImageAction", Mono.Unix.Catalog.GetString("_Reportar un error"), null, "gtk-missing-image");
        this.missingImageAction.ShortLabel = Mono.Unix.Catalog.GetString("_Reportar un error");
        w1.Add(this.missingImageAction, null);
        this.aboutAction = new Gtk.Action("aboutAction", Mono.Unix.Catalog.GetString("_Acerca de..."), null, "gtk-about");
        this.aboutAction.ShortLabel = Mono.Unix.Catalog.GetString("_Acerca de...");
        w1.Add(this.aboutAction, null);
        this.UIManager.InsertActionGroup(w1, 0);
        this.AddAccelGroup(this.UIManager.AccelGroup);
        this.Name = "MainWindow";
        this.Title = Mono.Unix.Catalog.GetString("MainWindow");
        this.Icon = Gdk.Pixbuf.LoadFromResource("CajaFinanciera.png");
        this.WindowPosition = ((Gtk.WindowPosition)(1));
        this.Gravity = ((Gdk.Gravity)(5));
        // Container child MainWindow.Gtk.Container+ContainerChild
        this.vbox1 = new Gtk.VBox();
        this.vbox1.Name = "vbox1";
        this.vbox1.Spacing = 6;
        // Container child vbox1.Gtk.Box+BoxChild
        this.UIManager.AddUiFromString("<ui><menubar name='menubar1'><menu name='ArchivoAction' action='ArchivoAction'><menuitem name='quitAction' action='quitAction'/></menu><menu name='AyudaAction' action='AyudaAction'><menuitem name='helpAction' action='helpAction'/><separator/><menuitem name='homeAction' action='homeAction'/><menuitem name='missingImageAction' action='missingImageAction'/><separator/><menuitem name='aboutAction' action='aboutAction'/></menu></menubar></ui>");
        this.menubar1 = ((Gtk.MenuBar)(this.UIManager.GetWidget("/menubar1")));
        this.menubar1.Name = "menubar1";
        this.vbox1.Add(this.menubar1);
        Gtk.Box.BoxChild w2 = ((Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
        w2.Position = 0;
        w2.Expand = false;
        w2.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this.hbox1 = new Gtk.HBox();
        this.hbox1.Name = "hbox1";
        this.hbox1.Spacing = 6;
        // Container child hbox1.Gtk.Box+BoxChild
        this._eb_toolbar = new Gtk.EventBox();
        this._eb_toolbar.Name = "_eb_toolbar";
        this.hbox1.Add(this._eb_toolbar);
        Gtk.Box.BoxChild w3 = ((Gtk.Box.BoxChild)(this.hbox1[this._eb_toolbar]));
        w3.Position = 0;
        w3.Expand = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.vbox3 = new Gtk.VBox();
        this.vbox3.Name = "vbox3";
        this.vbox3.Spacing = 6;
        // Container child vbox3.Gtk.Box+BoxChild
        this._eb_cuentas = new Gtk.EventBox();
        this._eb_cuentas.Name = "_eb_cuentas";
        this.vbox3.Add(this._eb_cuentas);
        Gtk.Box.BoxChild w4 = ((Gtk.Box.BoxChild)(this.vbox3[this._eb_cuentas]));
        w4.Position = 0;
        w4.Fill = false;
        this.hbox1.Add(this.vbox3);
        Gtk.Box.BoxChild w5 = ((Gtk.Box.BoxChild)(this.hbox1[this.vbox3]));
        w5.Position = 1;
        w5.Expand = false;
        // Container child hbox1.Gtk.Box+BoxChild
        this.vbox2 = new Gtk.VBox();
        this.vbox2.Name = "vbox2";
        this.vbox2.Spacing = 6;
        // Container child vbox2.Gtk.Box+BoxChild
        this._eb_search = new Gtk.EventBox();
        this._eb_search.Name = "_eb_search";
        this.vbox2.Add(this._eb_search);
        Gtk.Box.BoxChild w6 = ((Gtk.Box.BoxChild)(this.vbox2[this._eb_search]));
        w6.Position = 0;
        w6.Fill = false;
        this.hbox1.Add(this.vbox2);
        Gtk.Box.BoxChild w7 = ((Gtk.Box.BoxChild)(this.hbox1[this.vbox2]));
        w7.PackType = ((Gtk.PackType)(1));
        w7.Position = 2;
        w7.Expand = false;
        this.vbox1.Add(this.hbox1);
        Gtk.Box.BoxChild w8 = ((Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
        w8.Position = 1;
        w8.Expand = false;
        w8.Fill = false;
        // Container child vbox1.Gtk.Box+BoxChild
        this._main_container = new Gtk.EventBox();
        this._main_container.Name = "_main_container";
        this.vbox1.Add(this._main_container);
        Gtk.Box.BoxChild w9 = ((Gtk.Box.BoxChild)(this.vbox1[this._main_container]));
        w9.Position = 2;
        // Container child vbox1.Gtk.Box+BoxChild
        this.statusbar = new Gtk.Statusbar();
        this.statusbar.Name = "statusbar";
        this.statusbar.Spacing = 6;
        // Container child statusbar.Gtk.Box+BoxChild
        this._eb_msg = new Gtk.EventBox();
        this._eb_msg.Name = "_eb_msg";
        // Container child _eb_msg.Gtk.Container+ContainerChild
        this._lbl_msg = new Gtk.Label();
        this._lbl_msg.Name = "_lbl_msg";
        this._eb_msg.Add(this._lbl_msg);
        this.statusbar.Add(this._eb_msg);
        Gtk.Box.BoxChild w11 = ((Gtk.Box.BoxChild)(this.statusbar[this._eb_msg]));
        w11.PackType = ((Gtk.PackType)(1));
        w11.Position = 1;
        w11.Expand = false;
        // Container child statusbar.Gtk.Box+BoxChild
        this._prg_progress = new Gtk.ProgressBar();
        this._prg_progress.Name = "_prg_progress";
        this.statusbar.Add(this._prg_progress);
        Gtk.Box.BoxChild w12 = ((Gtk.Box.BoxChild)(this.statusbar[this._prg_progress]));
        w12.PackType = ((Gtk.PackType)(1));
        w12.Position = 2;
        w12.Expand = false;
        this.vbox1.Add(this.statusbar);
        Gtk.Box.BoxChild w13 = ((Gtk.Box.BoxChild)(this.vbox1[this.statusbar]));
        w13.Position = 3;
        w13.Expand = false;
        w13.Fill = false;
        this.Add(this.vbox1);
        if ((this.Child != null)) {
            this.Child.ShowAll();
        }
        this.DefaultWidth = 433;
        this.DefaultHeight = 333;
        this.Show();
        this.quitAction.Activated += new System.EventHandler(this.OnQuitActionActivated);
        this.aboutAction.Activated += new System.EventHandler(this.OnAboutActionActivated);
    }
}
