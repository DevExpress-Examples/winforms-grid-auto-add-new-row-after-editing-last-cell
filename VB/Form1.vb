' Developer Express Code Central Example:
' How to automatically append a new row when a user presses the Enter or Key tab in the last cell
' 
' This example illustrates how to force GridView to automatically append a new row
' when an end-user finishes editing the last cell in GridView.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E3810
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsApplication1

    Public Partial Class Form1
        Inherits Form

        Private Function CreateTable(ByVal RowCount As Integer) As DataTable
            Dim tbl As DataTable = New DataTable()
            tbl.Columns.Add("Name", GetType(String))
            tbl.Columns.Add("ID", GetType(Integer))
            tbl.Columns.Add("Number", GetType(Integer))
            tbl.Columns.Add("Date", GetType(Date))
            For i As Integer = 0 To RowCount - 1
                tbl.Rows.Add(New Object() {String.Format("Name{0}", i), i, 3 - i, Date.Now.AddDays(i)})
            Next

            Return tbl
        End Function

        Public Sub New()
            InitializeComponent()
            gridControl1.DataSource = CreateTable(20)
            Dim tmp_GridNewRowHelper = New GridNewRowHelper(gridView1)
        End Sub
    End Class

    Public Class GridNewRowHelper

        Private ReadOnly _View As GridView

        Public Sub New(ByVal view As GridView)
            _View = view
            AddHandler _View.HiddenEditor, AddressOf _View_HiddenEditor
            AddHandler view.GridControl.EditorKeyDown, AddressOf GridControl_EditorKeyDown
            AddHandler view.GridControl.KeyDown, New KeyEventHandler(AddressOf GridControl_KeyDown)
        End Sub

        Private Sub _View_HiddenEditor(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Private Sub GridControl_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            e.Handled = OnKeyDown(e.KeyCode, e.Modifiers)
        End Sub

        Private Sub GridControl_EditorKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
            e.Handled = OnKeyDown(e.KeyCode, e.Modifiers)
        End Sub

        Private Function OnKeyDown(ByVal keyCode As Keys, ByVal modifiers As Keys) As Boolean
            If modifiers = Keys.None And (keyCode = Keys.Enter OrElse keyCode = Keys.Tab) Then
                Return CheckAddNewRow()
            End If

            Return False
        End Function

        Private Function CheckAddNewRow() As Boolean
            If _View.FocusedColumn.VisibleIndex = _View.VisibleColumns.Count - 1 Then
                If _View.IsNewItemRow(_View.FocusedRowHandle) Then
                    _View.PostEditor()
                    _View.UpdateCurrentRow()
                End If

                If _View.IsLastRow Then Return AddNewRow()
            End If

            Return False
        End Function

        Private Function AddNewRow() As Boolean
            _View.AddNewRow()
            _View.FocusedColumn = _View.VisibleColumns(0)
            Return True
        End Function
    End Class
End Namespace
