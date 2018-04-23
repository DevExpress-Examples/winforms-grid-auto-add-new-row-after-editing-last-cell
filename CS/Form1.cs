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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }

        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            new GridNewRowHelper(gridView1);
        }
    }

    public class GridNewRowHelper
    {

        private readonly GridView _View;
        public GridNewRowHelper(GridView view)
        {
            _View = view;
            _View.HiddenEditor += _View_HiddenEditor;
            view.GridControl.EditorKeyDown += GridControl_EditorKeyDown;
            view.GridControl.KeyDown += new KeyEventHandler(GridControl_KeyDown);
        }

        void _View_HiddenEditor(object sender, EventArgs e) {
        }

        void GridControl_KeyDown(object sender, KeyEventArgs e)
        {
           e.Handled =  OnKeyDown(e.KeyCode, e.Modifiers);
        }

        void GridControl_EditorKeyDown(object sender, KeyEventArgs e)
        {
           e.Handled = OnKeyDown(e.KeyCode, e.Modifiers);
        }
        private bool OnKeyDown(Keys keyCode, Keys modifiers)
        {
            if (modifiers == Keys.None & (keyCode == Keys.Enter || keyCode == Keys.Tab))
            {
                return CheckAddNewRow();
            }
            return false;
        }

        private bool CheckAddNewRow()
        {
            if (_View.FocusedColumn.VisibleIndex == _View.VisibleColumns.Count - 1)
            {
                if (_View.IsNewItemRow(_View.FocusedRowHandle)) {
                    _View.PostEditor();
                    _View.UpdateCurrentRow();
                }
                if (_View.IsLastRow)
                    return AddNewRow();
            }
            return false;
        }

        private bool AddNewRow()
        {
            _View.AddNewRow();
            _View.FocusedColumn = _View.VisibleColumns[0];
            return true;
        }
    }
}
