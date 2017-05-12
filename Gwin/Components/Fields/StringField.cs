using System;
using System.Drawing;

namespace App.Gwin.Fields
{
    public partial class StringField : App.Gwin.Fields.BaseField
    {

        #region  Constant
        public static int DEFAULT_NUMBER_OF_MULTI_LINE = 5;
        #endregion

        #region Properties
        /// <summary>
        /// get de value of the TexteBox  
        /// </summary>
        public override object Value
        {
            get
            {
                return textBoxField.Text;
            }
            set
            {
                if(value != null)
                textBoxField.Text = value.ToString();
            }
        }

        /// <summary>
        /// Check if Field is Empty or not
        /// </summary>
        public override bool isEmpty
        {
            get
            {
                if (textBoxField.Text == string.Empty) return true;
                else return false;
            }
        }

        private bool isMultiline;
        /// <summary>
        /// set is the TexteBow is multiline
        /// </summary>
        public bool IsMultiline {
            get { return isMultiline; }
            set
            {
                if(isMultiline != value)
                {
                    isMultiline = value;
                    CallConfigSizeField();
                }

            }
        }
        /// <summary>
        /// Number of ligne if the texteBox is Multiline
        /// </summary>
        private int nombreLigne { set; get; }
        public int NombreLigne
        {
            get { return nombreLigne; }
            set
            {
                if (nombreLigne != value)
                {
                    nombreLigne = value;
                    CallConfigSizeField();
                }

            }
        }


        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public StringField() :base()
        {
            InitializeComponent();
            this.NombreLigne = StringField.DEFAULT_NUMBER_OF_MULTI_LINE;
        }

        
        private void textBoxField_TextChanged(object sender, EventArgs e)
        {
            onValueChanged(this, e);
        }
        /// <summary>
        /// Initialisation spécifique à zone de texte
        /// Exécuter aprés l'initialisation de du Size Field
        /// </summary>
        public override void ConfigSizeField()
        {
            base.ConfigSizeField();

            if (this.IsMultiline)
            {
                this.textBoxField.Multiline = true;
                this.textBoxField.Size = new Size(this.SizeControl.Width, 10 * this.NombreLigne);
                // Modification de Size de Field
                // [Bug] est ce que il faut augmenter aussi la taille Layout ?
                this.Size = new Size(this.Size.Width, this.Size.Height + 10 * (this.NombreLigne));
            }
        }

        

       
    }
}
