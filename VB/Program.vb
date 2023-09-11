' Developer Express Code Central Example:
' How to automatically append a new row when a user presses the Enter or Key tab in the last cell
' 
' This example illustrates how to force GridView to automatically append a new row
' when an end-user finishes editing the last cell in GridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3810
Imports System
Imports System.Windows.Forms

Namespace WindowsApplication1

    Friend Module Program

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread>
        Sub Main()
            Call Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Call Application.Run(New Form1())
        End Sub
    End Module
End Namespace
