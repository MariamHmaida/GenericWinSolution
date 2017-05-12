using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace App.Gwin.Fields
{
    /// <summary>
    /// Base Field 
    /// </summary>
    public partial class BaseField : UserControl, IBaseField
    {

        #region Events
        /// <summary>
        /// Value is changed
        /// </summary>
        public event EventHandler ValueChanged;
        protected void onValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(sender, e);
        }
        /// <summary>
        /// Validating
        /// </summary>
        public event EventHandler<CancelEventArgs> ValidatingField;
        protected void onValidatingField(object sender, CancelEventArgs e)
        {
            if (ValidatingField != null)
                ValidatingField(sender, e);
        }
        #endregion

        #region Properties
        /// <summary>
        /// PropertyInfo
        /// </summary>
        public PropertyInfo PropertyInfo { set; get; }
        /// <summary>
        /// The value of field
        /// </summary>
        public virtual object Value { get; set; }

        /// <summary>
        /// Direction, it is either vertical or horizontal
        /// </summary>
        protected Orientation orientationField;
        public Orientation OrientationField
        {
            set
            {
                if (this.orientationField != value)
                {
                    orientationField = value;
                    this.CallConfigSizeField();
                }
            }
            get
            {
                return orientationField;
            }
        }

        /// <summary>
        /// Text Label
        /// </summary>
        public string Text_Label
        {
            set
            {
                this.labelField.Text = value;
            }
        }

        private Size sizeLabel;
        public Size SizeLabel
        {
            get
            {
                return this.sizeLabel;
            }
            set
            {
                if (this.sizeLabel != value)
                {
                    this.sizeLabel = value;
                    this.CallConfigSizeField();
                }
            }
        }
        private Size sizezControl;
        public Size SizeControl
        {
            get
            {
                return this.sizezControl;
            }
            set
            {
                if (this.sizezControl != value)
                {
                    this.sizezControl = value;
                    this.CallConfigSizeField();
                }
            }
        }

        /// <summary>
        /// Indicate whether the size configuration is automatic
        /// Default is True
        /// </summary>
        public bool AutoSizeConfig { get; set; }

        #endregion

        #region Constructeurs
        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseField()
        {
            InitializeComponent();
            AutoSizeConfig = true;
            this.StopAutoSizeConfig();
            this.SizeLabel = new Size(100, 20);
            this.SizeControl = new Size(100, 20);
            this.StartAutoSizeConfig();
        }
        #endregion

        /// <summary>
        /// Ckeck if the filed is Empty or Not
        /// </summary>
        public virtual Boolean isEmpty
        {
            get {
                return false;
            }
        }

        public void StopAutoSizeConfig()
        {
            this.AutoSizeConfig = false;
        }
        public void StartAutoSizeConfig()
        {
            this.AutoSizeConfig = true;
        }

        /// <summary>
        /// Call the methode : ConfigSizeField if AutoSizeConfig is true;
        /// </summary>
        protected void CallConfigSizeField()
        {
            if (AutoSizeConfig) this.ConfigSizeField();
        }

        /// <summary>
        /// Initialisation des paramétres
        /// </summary>
        public virtual void ConfigSizeField()
        {
            // splitContainer Size
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.TabStop = false;

            if (OrientationField == Orientation.Vertical)
            {
                this.splitContainer.Orientation = Orientation.Vertical;
                // Filed Size
                int width_field = this.SizeLabel.Width + this.SizeControl.Width;
                int height_field = this.SizeControl.Height;
                this.Size = new Size(width_field, height_field);

                // Containner
                this.splitContainer.SplitterDistance = this.SizeLabel.Width;
            }
            else
            {
                // Filed Size
                int width_field = this.SizeControl.Width;
                int height_field = this.SizeLabel.Height + this.SizeControl.Height;
                this.Size = new Size(width_field, height_field);

                this.splitContainer.SplitterDistance = this.SizeLabel.Height;
                this.splitContainer.Orientation = Orientation.Horizontal;

                // Containner
                this.splitContainer.SplitterDistance = this.SizeLabel.Height;
            }
        }
    }
}
