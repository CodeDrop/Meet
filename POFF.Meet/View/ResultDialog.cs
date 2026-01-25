using POFF.Meet.Domain;
using POFF.Meet.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace POFF.Meet.View;

public class ResultDialog : Form
{
    #region  Windows Form Designer generated code 

    public ResultDialog() : base()
    {

        // This call is required by the Windows Form Designer.
        InitializeComponent();

        // Add any initialization after the InitializeComponent() call

    }

    // Form overrides dispose to clean up the component list.
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components is not null)
            {
                components.Dispose();
            }
        }
        base.Dispose(disposing);
    }

    // Required by the Windows Form Designer
    private System.ComponentModel.IContainer components;

    // NOTE: The following procedure is required by the Windows Form Designer
    // It can be modified using the Windows Form Designer.  
    // Do not modify it using the code editor.
    private Button _OKButton;

    internal virtual Button OKButton
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _OKButton;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _OKButton = value;
        }
    }
    private Button _AbortButton;

    internal virtual Button AbortButton
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _AbortButton;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _AbortButton = value;
        }
    }
    private ImageList _SetResultImageList;

    internal virtual ImageList SetResultImageList
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _SetResultImageList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _SetResultImageList = value;
        }
    }
    private DataGrid _SetResultDataGrid;

    private DataGrid SetResultDataGrid
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _SetResultDataGrid;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _SetResultDataGrid = value;
        }
    }
    private DataGridTableStyle _DataGridTableStyle1;

    internal virtual DataGridTableStyle DataGridTableStyle1
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _DataGridTableStyle1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _DataGridTableStyle1 = value;
        }
    }
    private DataGridTextBoxColumn _DataGridTextBoxColumn1;

    internal virtual DataGridTextBoxColumn DataGridTextBoxColumn1
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _DataGridTextBoxColumn1;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _DataGridTextBoxColumn1 = value;
        }
    }
    private DataGridTextBoxColumn _DataGridTextBoxColumn2;

    internal virtual DataGridTextBoxColumn DataGridTextBoxColumn2
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _DataGridTextBoxColumn2;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _DataGridTextBoxColumn2 = value;
        }
    }
    private Label _VersusLabel;

    internal virtual Label VersusLabel
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _VersusLabel;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _VersusLabel = value;
        }
    }
    private Label _Team2Label;

    internal virtual Label Team2Label
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _Team2Label;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _Team2Label = value;
        }
    }
    private Label _Team1Label;

    internal virtual Label Team1Label
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            return _Team1Label;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        set
        {
            _Team1Label = value;
        }
    }
    [DebuggerStepThrough()]
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultDialog));
        _OKButton = new Button();
        _AbortButton = new Button();
        _SetResultImageList = new ImageList(components);
        _SetResultDataGrid = new DataGrid();
        _DataGridTableStyle1 = new DataGridTableStyle();
        _DataGridTextBoxColumn1 = new DataGridTextBoxColumn();
        _DataGridTextBoxColumn2 = new DataGridTextBoxColumn();
        _VersusLabel = new Label();
        _Team2Label = new Label();
        _Team1Label = new Label();
        ((System.ComponentModel.ISupportInitialize)_SetResultDataGrid).BeginInit();
        SuspendLayout();
        // 
        // OKButton
        // 
        _OKButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _OKButton.DialogResult = DialogResult.OK;
        _OKButton.Location = new Point(144, 232);
        _OKButton.Name = "_OKButton";
        _OKButton.Size = new Size(96, 23);
        _OKButton.TabIndex = 6;
        _OKButton.Text = "&OK";
        // 
        // AbortButton
        // 
        _AbortButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        _AbortButton.DialogResult = DialogResult.Cancel;
        _AbortButton.Location = new Point(248, 232);
        _AbortButton.Name = "_AbortButton";
        _AbortButton.Size = new Size(96, 23);
        _AbortButton.TabIndex = 7;
        _AbortButton.Text = "Abbre&chen";
        // 
        // SetResultImageList
        // 
        _SetResultImageList.ImageStream = (ImageListStreamer)resources.GetObject("SetResultImageList.ImageStream");
        _SetResultImageList.TransparentColor = Color.Transparent;
        _SetResultImageList.Images.SetKeyName(0, "");
        // 
        // SetResultDataGrid
        // 
        _SetResultDataGrid.AllowSorting = false;
        _SetResultDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
        _SetResultDataGrid.CaptionFont = new Font("Lucida Sans Unicode", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        _SetResultDataGrid.CaptionVisible = false;
        _SetResultDataGrid.DataMember = "";
        _SetResultDataGrid.Font = new Font("Lucida Sans Unicode", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
        _SetResultDataGrid.HeaderForeColor = SystemColors.ControlText;
        _SetResultDataGrid.Location = new Point(80, 128);
        _SetResultDataGrid.Name = "_SetResultDataGrid";
        _SetResultDataGrid.PreferredRowHeight = 14;
        _SetResultDataGrid.RowHeadersVisible = false;
        _SetResultDataGrid.RowHeaderWidth = 0;
        _SetResultDataGrid.Size = new Size(187, 96);
        _SetResultDataGrid.TabIndex = 14;
        _SetResultDataGrid.TableStyles.AddRange([_DataGridTableStyle1]);
        // 
        // DataGridTableStyle1
        // 
        _DataGridTableStyle1.ColumnHeadersVisible = false;
        _DataGridTableStyle1.DataGrid = _SetResultDataGrid;
        _DataGridTableStyle1.GridColumnStyles.AddRange([_DataGridTextBoxColumn1, _DataGridTextBoxColumn2]);
        _DataGridTableStyle1.HeaderForeColor = SystemColors.ControlText;
        _DataGridTableStyle1.MappingName = "SetResult";
        _DataGridTableStyle1.PreferredColumnWidth = 186;
        _DataGridTableStyle1.RowHeaderWidth = 0;
        // 
        // DataGridTextBoxColumn1
        // 
        _DataGridTextBoxColumn1.Alignment = HorizontalAlignment.Center;
        _DataGridTextBoxColumn1.Format = "";
        _DataGridTextBoxColumn1.FormatInfo = null;
        _DataGridTextBoxColumn1.HeaderText = "Heim";
        _DataGridTextBoxColumn1.MappingName = "Home";
        _DataGridTextBoxColumn1.Width = 45;
        // 
        // DataGridTextBoxColumn2
        // 
        _DataGridTextBoxColumn2.Alignment = HorizontalAlignment.Center;
        _DataGridTextBoxColumn2.Format = "";
        _DataGridTextBoxColumn2.FormatInfo = null;
        _DataGridTextBoxColumn2.HeaderText = "Gast";
        _DataGridTextBoxColumn2.MappingName = "Guest";
        _DataGridTextBoxColumn2.Width = 45;
        // 
        // VersusLabel
        // 
        _VersusLabel.Anchor = AnchorStyles.Top;
        _VersusLabel.BackColor = Color.Transparent;
        _VersusLabel.Font = new Font("Lucida Sans Unicode", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
        _VersusLabel.Location = new Point(112, 48);
        _VersusLabel.Name = "_VersusLabel";
        _VersusLabel.Size = new Size(120, 23);
        _VersusLabel.TabIndex = 15;
        _VersusLabel.Text = "gegen";
        _VersusLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Team2Label
        // 
        _Team2Label.Anchor = AnchorStyles.Top;
        _Team2Label.BackColor = Color.NavajoWhite;
        _Team2Label.BorderStyle = BorderStyle.Fixed3D;
        _Team2Label.Font = new Font("Lucida Sans Unicode", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
        _Team2Label.Location = new Point(40, 80);
        _Team2Label.Name = "_Team2Label";
        _Team2Label.Size = new Size(303, 32);
        _Team2Label.TabIndex = 17;
        _Team2Label.Text = "[Mannschaft 2]";
        _Team2Label.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Team1Label
        // 
        _Team1Label.Anchor = AnchorStyles.Top;
        _Team1Label.BackColor = Color.NavajoWhite;
        _Team1Label.BorderStyle = BorderStyle.Fixed3D;
        _Team1Label.Font = new Font("Lucida Sans Unicode", 12.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
        _Team1Label.Location = new Point(8, 8);
        _Team1Label.Name = "_Team1Label";
        _Team1Label.Size = new Size(304, 32);
        _Team1Label.TabIndex = 16;
        _Team1Label.Text = "[Mannschaft 1]";
        _Team1Label.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // ResultDialog
        // 
        AcceptButton = _OKButton;
        AutoScaleBaseSize = new Size(8, 24);
        CancelButton = _AbortButton;
        ClientSize = new Size(354, 262);
        ControlBox = false;
        Controls.Add(_VersusLabel);
        Controls.Add(_Team2Label);
        Controls.Add(_Team1Label);
        Controls.Add(_SetResultDataGrid);
        Controls.Add(_AbortButton);
        Controls.Add(_OKButton);
        Font = new Font("Lucida Sans Unicode", 11.25f);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Name = "ResultDialog";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        ((System.ComponentModel.ISupportInitialize)_SetResultDataGrid).EndInit();
        ResumeLayout(false);

    }

    #endregion

    public ResultDialog(Match match) : this()
    {
        Team1Label.Text = match.Team1.Name;
        Team2Label.Text = match.Team2.Name;
        SetResults.AddRange(match.Result.SetResults.Select(s => new SetResultInput(s)));
        SetResultDataGrid.DataSource = SetResults;

        OKButton.Click += (s, e) =>
        {
            match.Result.Clear();
            foreach (var setResultInput in SetResults)
            {
                if (setResultInput.Home.HasValue && setResultInput.Guest.HasValue)
                {
                    match.Result.AddSetResult(new SetResult((int)setResultInput.Home, (int)setResultInput.Guest));
                }
            }
        };
    }

    public BindingList<SetResultInput> SetResults { get; } = [];
}