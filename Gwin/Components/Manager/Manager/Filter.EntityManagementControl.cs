using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin
{
    /// <summary>
    /// Ajouter un nouveau Entité 
    /// </summary>
    public partial class ManagerFormControl
    {
        private void BaseFilterControl_RefreshEvent(object sender, EventArgs e)
        {
            this.RefreshData();
        }
        /// <summary>
        /// Afficher ou désafficher le filtre
        /// </summary>
        /// <param name="v"></param>
        private void ShowFilter(bool visible)
        {
            if (visible == true) throw new NotImplementedException("Cette méthode n'est pas implémenter pour la valeur True");

           
            this.Controls.Add(panelDataGrid);
            this.TabGrid.Controls.Clear();
            this.TabGrid.Controls.Add(panelDataGrid);
            
           
           
        }
    }
}
