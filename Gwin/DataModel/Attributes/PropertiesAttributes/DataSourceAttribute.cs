using System;
using System.Collections;
using System.Collections.Generic;

namespace App.Gwin.Attributes
{
    /// <summary>
    /// Set DataSource to Fill ComboBox
    /// </summary>
    public class ReferencesDataSourceAttribute : Attribute
    {
        /// <summary>
        /// Type of Object that containt Data
        /// </summary>
        public Type TypeObject { get; set; }

        /// <summary>
        /// Name of methode to obtaine List of object data
        /// </summary>
        public string MethodeName { get; set; }
        /// <summary>
        /// Property to use in Object returned by MathodeName to Knew DisplyName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// First Parameter of Methode
        /// </summary>
        public object Param1 { get; set; }
        public object Param2 { get; set; }


        #region Create Instance

        /// <summary>
        /// Create Instance of DataSource object
        /// </summary>
        /// <returns></returns>
        public object CreateInstance()
        {
            object DataObject = null;
            DataObject = Activator.CreateInstance(this.TypeObject);
            return DataObject;
        }

        /// <summary>
        /// Get Data from DataSource
        /// </summary>
        /// <returns></returns>
        public List<String> GetData()
        {
            object DataObject = this.CreateInstance();
            IList ls_data = null;
            if (Param1 == null)
            {
                ls_data = (IList)this.TypeObject.GetMethod(this.MethodeName).Invoke(DataObject, null);
            }
            else
            {
                if (Param2 == null)
                    ls_data = (IList)this.TypeObject.GetMethod(this.MethodeName).Invoke(DataObject, new object[] { Param1 });
                else
                    ls_data = (IList)this.TypeObject.GetMethod(this.MethodeName).Invoke(DataObject, new object[] { Param1, Param2 });

            }

            // Read by Disply Name
            List<String> returnedList = new List<string>();
            foreach (var item in ls_data)
            {
                returnedList.Add(item.ToString());
            }


            return returnedList;
        }

        #endregion
    }
}