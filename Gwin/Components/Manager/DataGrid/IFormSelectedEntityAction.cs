using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gwin.Entities;

namespace App.Gwin.Components.Manager.DataGrid
{
    /// <summary>
    /// Interface of Form : to Implement SelectedEntityAction in DataGrid
    /// </summary>
    public interface IFormSelectedEntityAction
    {
        /// <summary>
        /// Set Entity Instance
        /// </summary>
        /// <param name="obj"></param>
        void SetEntity(BaseEntity obj);
    }
}
