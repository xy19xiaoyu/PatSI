using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Diagnostics;
namespace ExcelLib
{
    public class ExcelHelper
    {
        public string OutPutPath = @"C:\OutPut\";
        public int PageSize;
        public int ExpandedColumnCount = 10;
        public int ExpandedRowCount = 10;
        public string sheetName = "Sheet1";
        private string head = "";
        private string foot = "</Workbook>";
        public string Author;
        public string Created;
        public string LastSaved;
        public ExcelHelper()
        {
            Author = "cprs-chenxiaoyu";
            Created = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ");
            LastSaved = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ssZ");
            string filename = string.Format("{0}\\{1}", Environment.CurrentDirectory.ToString(), @"\\temple\head.xls.txt");
            head = File.ReadAllText(filename, Encoding.ASCII).Replace("<!--Author-->", Author).Replace("<!--Created-->", Created).Replace("<!--LastSaved-->", LastSaved);

        }


        /// <summary>
        /// 计算WorkSheet数量
        /// </summary>
        /// <param name="rowCount">记录总行数</param>
        /// <param name="rows">每WorkSheet行数</param>
        public int GetSheetCount(int rowCount, int rows)
        {
            int n = rowCount % rows;		//余数

            if (n == 0)
                return rowCount / rows;
            else
                return Convert.ToInt32(rowCount / rows) + 1;
        }
        public string formatTableHead(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Table ss:ExpandedColumnCount=\"" + dt.Columns.Count.ToString() + "\" ss:ExpandedRowCount=\"" + (dt.Rows.Count + 1).ToString() + "\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultColumnWidth=\"54\" ss:DefaultRowHeight=\"14.25\">");
            sb.Append("<Row ss:Height=\"18.75\">");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(string.Format("<Cell ss:StyleID=\"shead\"><Data ss:Type=\"String\">{0}</Data></Cell>", dt.Columns[i].ColumnName));
            }
            sb.Append("</Row>");
            return sb.ToString();

        }
        public string formatTableRow(DataRow row)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<Row>");
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                sb.Append(string.Format("<Cell ss:StyleID=\"srow\"><Data ss:Type=\"String\">{0}</Data></Cell>{1}", row[i].ToString().Trim().Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine));
            }
            sb.Append("</Row>");
            return sb.ToString();
        }
        public string formatTableRows(List<DataRow> rows, List<int> MergeColumnIds)
        {

            StringBuilder sb = new StringBuilder();

            int rowcount = rows.Count;
            if (rowcount == 1)
            {
                sb.Append("<Row>");
                DataRow row = rows[0];
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {

                    sb.Append(string.Format("<Cell ss:StyleID=\"srow\"><Data ss:Type=\"String\">{0}</Data></Cell>{1}", row[i].ToString().Trim().Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine));


                }
                sb.Append("</Row>");
            }
            else
            {
                rowcount = rowcount - 1;
                for (int x = 0; x < rows.Count; x++)
                {
                    DataRow row = rows[x];

                    sb.Append("<Row>");
                    if (x == 0)
                    {

                        for (int i = 0; i < row.Table.Columns.Count; i++)
                        {
                            if (MergeColumnIds.Contains(i))
                            {
                                sb.Append(string.Format("<Cell ss:StyleID=\"cm\" ss:MergeDown=\"{2}\"><Data ss:Type=\"String\">{0}</Data></Cell>{1}", row[i].ToString().Trim().Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine, rowcount));
                            }
                            else
                            {
                                sb.Append(string.Format("<Cell ss:StyleID=\"srow\"><Data ss:Type=\"String\">{0}</Data></Cell>{1}", row[i].ToString().Trim().Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine));
                            }

                        }
                    }
                    else
                    {

                        for (int i = 0; i < row.Table.Columns.Count; i++)
                        {
                            if (MergeColumnIds.Contains(i))
                            {
                                continue;
                            }
                            else
                            {
                                sb.Append(string.Format("<Cell ss:Index=\"{2}\"  ss:StyleID=\"srow\"><Data ss:Type=\"String\">{0}</Data></Cell>{1}", row[i].ToString().Trim().Replace("<", "&lt;").Replace(">", "&gt;"), Environment.NewLine, i + 1));
                            }

                        }

                    }
                    sb.Append("</Row>");
                }
            }


            return sb.ToString();
        }

        public bool DataTable2ExcelFile(DataTable dt, string FileName)
        {
            Console.WriteLine(dt.TableName);

            if (dt.TableName != "")
            {
                sheetName = dt.TableName;
            }
            sheetName = "统计结果";

            string FilePath = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            if (File.Exists(FileName)) File.Delete(FileName);


            using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.UTF8) { AutoFlush = true })
            {
                #region 写文件头
                sw.Write(head);
                #endregion
                #region 写sheet头
                sw.Write(string.Format("<Worksheet ss:Name=\"{0}\">", sheetName));
                #endregion

                #region "写表头"
                sw.Write(formatTableHead(dt));
                #endregion
                foreach (DataRow row in dt.Rows)
                {
                    sw.Write(formatTableRow(row));
                }
                #region "写表尾"
                sw.Write("</Table>");
                #endregion

                #region 写sheet尾
                sw.Write("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\"><PageSetup><Header x:Margin=\"0.511111111111111\"/><Footer x:Margin=\"0.511111111111111\"/></PageSetup><Selected/><TopRowVisible>0</TopRowVisible><LeftColumnVisible>0</LeftColumnVisible><PageBreakZoom>100</PageBreakZoom><Panes><Pane><Number>3</Number><ActiveRow>7</ActiveRow><ActiveCol>4</ActiveCol><RangeSelection>R8C5</RangeSelection></Pane></Panes><ProtectObjects>False</ProtectObjects><ProtectScenarios>False</ProtectScenarios></WorksheetOptions></Worksheet>");
                #endregion
                #region 写文件尾
                sw.Write(foot);
                #endregion
            }
            return true;
        }

        public bool DataTable2ExcelFileAndMerge(DataTable dt, string FileName, List<int> MergeColumnIds)
        {
            Console.WriteLine(dt.TableName);

            if (dt.TableName != "")
            {
                sheetName = dt.TableName;
            }
            sheetName = "著录项目信息";

            string FilePath = Path.GetDirectoryName(FileName);
            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
            }
            if (File.Exists(FileName)) File.Delete(FileName);


            using (StreamWriter sw = new StreamWriter(FileName, false, Encoding.UTF8) { AutoFlush = true })
            {
                #region 写文件头
                sw.Write(head);
                #endregion
                #region 写sheet头
                sw.Write(string.Format("<Worksheet ss:Name=\"{0}\">", sheetName));
                #endregion

                #region "写表头"
                sw.Write(formatTableHead(dt));
                #endregion
                List<DataRow> rows = new List<DataRow>();

                var x = from y in dt.AsEnumerable()
                        group y by y[0] into g
                        select g;
                foreach (var xy in x)
                {
                    sw.Write(formatTableRows(xy.ToList<DataRow>(), MergeColumnIds));
                }

                #region "写表尾"
                sw.Write("</Table>");
                #endregion

                #region 写sheet尾
                sw.Write("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\"><PageSetup><Header x:Margin=\"0.511111111111111\"/><Footer x:Margin=\"0.511111111111111\"/></PageSetup><Selected/><TopRowVisible>0</TopRowVisible><LeftColumnVisible>0</LeftColumnVisible><PageBreakZoom>100</PageBreakZoom><Panes><Pane><Number>3</Number><ActiveRow>7</ActiveRow><ActiveCol>4</ActiveCol><RangeSelection>R8C5</RangeSelection></Pane></Panes><ProtectObjects>False</ProtectObjects><ProtectScenarios>False</ProtectScenarios></WorksheetOptions></Worksheet>");
                #endregion
                #region 写文件尾
                sw.Write(foot);
                #endregion
            }
            return true;
        }
    }

}

