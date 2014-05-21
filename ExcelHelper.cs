namespace client
{
    using Excel;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ExcelHelper
    {
        private _Workbook _Workbook_0;
        private Application application_0;
        public string filename = "";
        public int indxe;
        public bool isisCreate = false;
        private object object_0 = Type.Missing;
        public string path = "";
        public _Worksheet worksheet;

        public ExcelHelper()
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择存放的文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;
                    this.path = selectedPath + @"\";
                    if (File.Exists(this.path + this.filename))
                    {
                        File.Delete(this.path + this.filename);
                    }
                    this.application_0 = new ApplicationClass();
                    this.application_0.Visible = false;
                    this.application_0.DisplayAlerts = false;
                    if (!(this.application_0.Version != "11.0") && (this.application_0 != null))
                    {
                        this.application_0.Workbooks.Add(true);
                        this._Workbook_0 = this.application_0.Workbooks[1];
                        this._Workbook_0.Activate();
                        this.worksheet = (_Worksheet) this._Workbook_0.Worksheets[1];
                        this.worksheet.Name = "Cells";
                        this.worksheet.Activate();
                    }
                    else
                    {
                        MessageBox.Show("您的 Excel 版本不是 11.0 （Office 2003），操作可能会出现问题。");
                        this.application_0.Quit();
                        this.isisCreate = true;
                    }
                }
                else
                {
                    this.isisCreate = true;
                    this.filename = "";
                }
            }
            catch (Exception exception)
            {
                this.isisCreate = true;
                MessageBox.Show(exception.Message);
            }
        }

        public void addDataFast(List<List<string>> list_0)
        {
            for (int i = 0; i < list_0.Count; i++)
            {
                List<string> list = list_0[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (!string.IsNullOrEmpty(list[j]))
                    {
                        this.worksheet.Cells[i + 2, j + 1] = list[j];
                    }
                }
            }
        }

        public void addDate(List<List<string>> list_0)
        {
            for (int i = 0; i < list_0.Count; i++)
            {
                List<string> list = list_0[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (!string.IsNullOrEmpty(list[j]))
                    {
                        Range range = null;
                        range = (Range) this.worksheet.Cells[i + 2, j + 1];
                        range.WrapText = true;
                        range.EntireRow.AutoFit();
                        range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                        this.worksheet.Cells[i + 2, j + 1] = list[j];
                    }
                }
            }
        }

        public void addRow(List<string> list_0)
        {
            for (int i = 0; i < list_0.Count; i++)
            {
                if (!string.IsNullOrEmpty(list_0[i]))
                {
                    Range range = null;
                    range = (Range) this.worksheet.Cells[this.indxe, i + 1];
                    range.WrapText = true;
                    range.EntireRow.AutoFit();
                    range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                    this.worksheet.Cells[this.indxe, i + 1] = list_0[i];
                }
            }
        }

        public void addRowFast(List<string> list_0)
        {
            for (int i = 0; i < list_0.Count; i++)
            {
                if (!string.IsNullOrEmpty(list_0[i]))
                {
                    this.worksheet.Cells[this.indxe, i + 1] = list_0[i];
                }
            }
        }

        public void Dispose()
        {
            this.method_1(this._Workbook_0);
            this.method_1(this.application_0);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExportExcel(int int_0, int int_1)
        {
            try
            {
                this.worksheet.SaveAs(this.path + this.filename, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, this.object_0, Missing.Value);
                Range range = this.worksheet.get_Range(this.worksheet.Cells[2, 1], this.worksheet.Cells[int_0 + 2, int_1 + 1]);
                range.BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, null);
                range.Borders[XlBordersIndex.xlInsideHorizontal].ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
                range.Borders[XlBordersIndex.xlInsideHorizontal].Weight = XlBorderWeight.xlThin;
                if ((int_1 > 1) && (range != null))
                {
                    range.Borders[XlBordersIndex.xlInsideVertical].ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                }
                this._Workbook_0.Close(this.object_0, this.object_0, this.object_0);
                this.application_0.Quit();
            }
            catch (Exception exception)
            {
                MessageBox.Show("导出出现异常:" + exception.Message);
            }
        }

        public void FreezePanes()
        {
            this.application_0.get_Range("A2", Type.Missing).Select();
            this.application_0.ActiveWindow.FreezePanes = true;
        }

        public string InsertPicture(string string_0, string string_1, bool bool_0)
        {
            string message;
            try
            {
                this.worksheet.get_Range(string_0, this.object_0).Select();
                ((Pictures) this.worksheet.Pictures(this.object_0)).Insert(string_1, this.object_0);
                return "";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }

        public string InsertPicture(string string_0, string string_1, float float_0, float float_1, int int_0)
        {
            string message;
            try
            {
                Range range = this.worksheet.get_Range(string_0, this.object_0);
                range.RowHeight = float_1 + 10f;
                range.ColumnWidth = int_0;
                range.Select();
                this.worksheet.Shapes.AddPicture(string_1, 0, -1, Convert.ToSingle(range.Left) + 10f, Convert.ToSingle(range.Top) + 4f, float_0, float_1);
                return "";
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }

        private void method_0()
        {
            this._Workbook_0.Close(false, this.object_0, this.object_0);
            this.application_0.Quit();
        }

        private void method_1(object object_1)
        {
            try
            {
                Marshal.ReleaseComObject(object_1);
            }
            catch
            {
            }
            finally
            {
                object_1 = null;
            }
        }

        public void SaveFile()
        {
            if (!string.IsNullOrEmpty(this.path + this.filename) && !this.isisCreate)
            {
                try
                {
                    if (string.IsNullOrEmpty(this.filename))
                    {
                        this.filename = "万能搬家软件导出.xls";
                    }
                    this._Workbook_0.SaveAs(this.path + this.filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    this.method_0();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    exception.ToString();
                }
            }
            else
            {
                MessageBox.Show("导出失败！");
            }
        }

        public void showTitle(Dictionary<string, int> dictionary_0)
        {
            Range range = null;
            int num = 1;
            foreach (KeyValuePair<string, int> pair in dictionary_0)
            {
                range = (Range) this.worksheet.Cells[1, num];
                range.HorizontalAlignment = XlVAlign.xlVAlignCenter;
                this.worksheet.Cells[1, num] = pair.Key;
                if (pair.Value == 0)
                {
                    range.ColumnWidth = 12;
                }
                else
                {
                    range.ColumnWidth = pair.Value;
                    range.WrapText = true;
                    range.EntireRow.AutoFit();
                }
                num++;
            }
        }

        public void showTitleFast(Dictionary<string, int> dictionary_0)
        {
            int num = 1;
            foreach (KeyValuePair<string, int> pair in dictionary_0)
            {
                this.worksheet.Cells[1, num] = pair.Key;
                num++;
            }
        }
    }
}

