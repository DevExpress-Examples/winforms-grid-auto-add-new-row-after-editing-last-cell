// Developer Express Code Central Example:
// How to automatically append a new row when a user presses the Enter or Key tab in the last cell
// 
// This example illustrates how to force GridView to automatically append a new row
// when an end-user finishes editing the last cell in GridView.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E3810

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}