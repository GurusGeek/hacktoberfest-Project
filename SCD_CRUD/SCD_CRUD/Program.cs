using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCD_CRUD
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //  Application.Run(new StudentForm());
            //Application.Run(new DptForm());
            //Application.Run(new roleForm());
            //Application.Run(new Reporting());
            Application.Run(new OrderForm());

        }
    }
}
