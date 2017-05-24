using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using System.IO;

namespace App.Gwin.Components.Manager.Actions
{
    public partial class ActionsComponent : MetroUserControl
    {
        private ManagerFormControl managerFormControl;

        private List<BaseDataAction> ListDataActions { set; get; }

        public void AddDataAction(BaseDataAction dataAction)
        {
            this.ListDataActions.Add(dataAction);
            flowLayoutPanel1.Controls.Add(dataAction);

        }


        public ActionsComponent(ManagerFormControl managerFormControl)
        {
            InitializeComponent();

            // Direction
            if (GwinApp.isRightToLeft)
                flowLayoutPanel1.FlowDirection = FlowDirection.RightToLeft;
            else
                flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;

            // Init List DataActions
            this.ListDataActions = new List<BaseDataAction>();

            // Create ExportExcel Action
            ExportExcelDataAction ExportExcelData = 
                new ExportExcelDataAction(
                    managerFormControl.BLO_Instance, 
                    managerFormControl.Filter_Instance.GetFilterValues());
            this.AddDataAction(ExportExcelData);

             

        }

        
    }
}
