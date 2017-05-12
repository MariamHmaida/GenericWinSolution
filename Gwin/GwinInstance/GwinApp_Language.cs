
using App.Gwin.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Gwin
{
    public partial class GwinApp
    {
        /// <summary>
        /// Application Culutre Info 
        /// </summary>
        public CultureInfo CultureInfo { set; get; }

        /// <summary>
        /// Languages of the GwinApp
        /// </summary>
        public enum Languages
        {
            en,
            fr,
            ar
        }

        /// <summary>
        /// Change Application Language
        /// </summary>
        /// <param name="cultureInfo">CultureInfo Instance</param>
        public static void ChangeLanguage(CultureInfo cultureInfo)
        {
            GwinApp.instance.user.Language = Convert_CultureInfo_Language(cultureInfo);
            SetLanguage(cultureInfo);

            //[Role] Restart must be after Language change, for Set Application Name Title after 
            // Initialize Form
            GwinApp.Restart();
        }

        /// <summary>
        /// Set Language at the first Time, first GwinApp start 
        /// </summary>
        /// <param name="cultureInfo">CultureInfo Instance</param>
        public static void SetLanguage(CultureInfo cultureInfo)
        {


            GwinApp.TestIf_Gwin_isStart();





            // Must Delete All Entity Configuration, bacause it demande with language
            ConfigEntity.Despose();

            // Set Date as Latin Date
            cultureInfo.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();

            // Change Gwin CultureInfo Instance
            GwinApp.Instance.CultureInfo = cultureInfo;

            // Change Thread CultureInfo Instance
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            // ReLoad Applicaton Interface
            instance.FormApplication.Reload();
        }

        /// <summary>
        /// Convert CultureInfo instance to Language Enum
        /// </summary>
        /// <param name="CultureInfo"></param>
        /// <returns></returns>
        private static Languages Convert_CultureInfo_Language(CultureInfo CultureInfo)
        {
            switch (CultureInfo.TwoLetterISOLanguageName.ToUpper())
            {
                case "AR": return Languages.ar;
                case "FR": return Languages.fr;
                case "EN": return Languages.en;
                default: return Languages.en;

            }
        }

        /// <summary>
        /// Ckeck if Contols must be showen as RightToLeft
        /// </summary>
        public static bool isRightToLeft
        {
            get
            {

                if (GwinApp.instance.user.Language == Languages.ar)
                    return true;
                else
                    return false;
            }
        }

    }
}
