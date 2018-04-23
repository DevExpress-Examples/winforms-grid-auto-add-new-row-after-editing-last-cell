Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
		End Function

		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			Dim TempGridNewRowHelper As GridNewRowHelper = New GridNewRowHelper(gridView1)
		End Sub
	End Class

	Public Class GridNewRowHelper

		Private ReadOnly _View As GridView
		Public Sub New(ByVal view As GridView)
			_View = view
			AddHandler view.GridControl.EditorKeyDown, AddressOf GridControl_EditorKeyDown
			AddHandler view.GridControl.KeyDown, AddressOf GridControl_KeyDown
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
					_View.UpdateCurrentRow()
				End If
				If _View.IsLastRow Then
					Return AddNewRow()
				End If
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
