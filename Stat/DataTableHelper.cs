using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Stat
{
    public class DataTableHelper
    {
        public static DataTable AddSumRow(DataTable dt)
        {
            DataRow row = dt.NewRow();
            row[0] = "总计";
            Dictionary<int, double> dirsum = new Dictionary<int, double>();
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                double sum = 0;
                bool haserror = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double tmp = 0;
                    if (Double.TryParse(dt.Rows[i][j].ToString(), out tmp))
                    {
                        sum += tmp;
                    }
                    else
                    {
                        haserror = true;
                    }
                }
                if (!haserror)
                {
                    row[j] = sum;
                }
            }
            dt.Rows.Add(row);
            return dt;
        }
        public static DataTable AddSumColumn(DataTable dt)
        {
            dt.Columns.Add(new DataColumn("合计"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double sum = 0;
                for (int j = 1; j < dt.Columns.Count; j++)
                {


                    double tmp = 0;
                    if (Double.TryParse(dt.Rows[i][j].ToString(), out tmp))
                    {
                        sum += tmp;
                    }

                }
                dt.Rows[i][dt.Columns.Count - 1] = sum;

            }
            return dt;
        }
        public static DataTable ExchangeRowColumn(DataTable dt, List<string> columns)
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(dt.Columns[0].ColumnName);
            foreach (var coluame in columns)
            {
                dtnew.Columns.Add(coluame);
            }

            var res = from y in dt.AsEnumerable()
                      group y by y[0].ToString() into g
                      select g;
            foreach (var x in res)
            {
                DataRow row = dtnew.NewRow();
                row[0] = x.Key;
                foreach (var subrou in x)
                {
                    if (columns.Contains(subrou[1]))
                    {
                        row[subrou[1].ToString()] = subrou[2];
                    }
                }
                for (int i = 1; i < dtnew.Columns.Count; i++)
                {
                    if (row[i].ToString() == "") row[i] = 0;
                }
                dtnew.Rows.Add(row);
            }
            return dtnew;
        }
        public static DataTable FillColumn(DataTable dt, List<string> columns)
        {
            foreach (var column in columns)
            {
                var res = from y in dt.AsEnumerable()
                          where y[0].ToString().Trim() == column
                          select y;
                if (res.Count() <= 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = column;
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        row[i] = 0;
                    }
                    dt.Rows.Add(row);
                }

            }
            return dt;
        }
        public static DataTable FillColumn1(DataTable dt, List<string> columns)
        {
            foreach (var column in columns)
            {
                var res = from y in dt.AsEnumerable()
                          where y[0].ToString().Trim() == column
                          select y;
                if (res.Count() <= 0)
                {
                    DataRow row = dt.NewRow();
                    row[0] = column;
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        row[i] = 0;
                    }
                    dt.Rows.Add(row);
                }

            }
            dt = (from y in dt.AsEnumerable()
                  orderby y[0].ToString() ascending
                  select y).CopyToDataTable();

            return dt;
        }


        public static DataTable ExchangeRowColumn(DataTable dt)
        {
            DataTable dtnew = new DataTable();
            dtnew.Columns.Add(dt.Columns[0].ColumnName);
            var columns = (from y in dt.AsEnumerable()
                           select y[1].ToString()).Distinct<string>().ToList<string>();
            foreach (var coluame in columns)
            {
                if (coluame == "")
                {
                    dtnew.Columns.Add("未知");
                }
                else
                {
                    dtnew.Columns.Add(coluame);
                }
            }
            var res = from y in dt.AsEnumerable()
                      group y by y[0].ToString() into g
                      select g;
            foreach (var x in res)
            {
                DataRow row = dtnew.NewRow();
                row[0] = x.Key;
                foreach (var subrou in x)
                {
                    if (subrou[1].ToString() == "")
                    {
                        subrou[1] = "未知";
                    }
                    row[subrou[1].ToString()] = subrou[2];
                }
                for (int i = 1; i < dtnew.Columns.Count; i++)
                {
                    if (row[i].ToString() == "") row[i] = 0;
                }
                dtnew.Rows.Add(row);
            }
            return dtnew;
        }
        public static DataTable AddZengSu(DataTable dt, List<int> columnids)
        {
            foreach (var x in columnids)
            {
                dt = AddZengSu(dt, x);
            }
            return dt;
        }
        public static DataTable AddZengSu(DataTable dt, int columnid)
        {
            string colname = dt.Columns[columnid].ColumnName;
            if (!dt.Columns.Contains(colname + "-增长率"))
            {
                dt.Columns.Add(colname + "-增长率", typeof(string));
            }
            double pryearnum = Convert.ToDouble(dt.Rows[0][columnid].ToString());
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                double nowyearnum = Convert.ToDouble(dt.Rows[i][columnid].ToString());
                if (pryearnum == 0)
                {
                    dt.Rows[i][colname + "-增长率"] = "";
                    pryearnum = nowyearnum;
                    continue;
                }
                dt.Rows[i][colname + "-增长率"] = ((nowyearnum - pryearnum) / pryearnum).ToString("0.00%");
                pryearnum = nowyearnum;
            }
            return dt;
        }

        public static DataTable ReadDateTable(DataTable dt)
        {
            int pryear = 0;
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][0].ToString().Trim() == string.Empty)
                {
                    dt.Rows.RemoveAt(i);
                }
                else
                {
                    int year = Convert.ToInt32(dt.Rows[i][0].ToString());
                    if (pryear == 0)
                    {
                        pryear = year;
                        continue;
                    }
                    for (int j = pryear - 1; j > year; j--)
                    {
                        DataRow tmprow = dt.NewRow();
                        tmprow[0] = j;
                        for (int m = 1; m < dt.Columns.Count; m++)
                        {
                            tmprow[m] = 0;
                        }
                        dt.Rows.InsertAt(tmprow, i + 1);
                    }
                    pryear = year;
                }
            }
            return dt;
        }

        public static DataTable TopNumRow(DataTable dt, int TOPNum)
        {
            DataRow row = dt.NewRow();
            row[0] = "其它";
            double count = 0;
            for (int j = 1; j < dt.Columns.Count; j++)
            {
                try
                {
                    for (int i = dt.Rows.Count - 1; i >= TOPNum; i--)
                    {
                        count += Convert.ToDouble(dt.Rows[i][j].ToString());

                    }

                    row[j] = count;
                }
                catch (Exception e)
                {
                    row[j] = "";
                }
            }
            for (int i = dt.Rows.Count - 1; i >= TOPNum; i--)
            {

                dt.Rows.RemoveAt(i);
            }
            dt.Rows.Add(row);
            return dt;

        }

        public static DataTable AddPercentColumn(DataTable dt, List<int> columns)
        {
            //新建表结构
            DataTable newdt = new DataTable();
            newdt.Columns.Add(new DataColumn(dt.Columns[0].ColumnName));
            //添加第一列
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow addrow = newdt.NewRow();
                addrow[0] = dt.Rows[j][0].ToString();
                newdt.Rows.Add(addrow);
            }

            for (int i = 1; i < dt.Columns.Count; i++)
            {
                //添加除了第一列之外的列
                double sum = 0;
                DataColumn col = dt.Columns[i];
                DataColumn addcol = new DataColumn(col.ColumnName);
                newdt.Columns.Add(addcol);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    double item = 0;
                    Double.TryParse(dt.Rows[j][col].ToString(), out item);
                    sum += item;
                    newdt.Rows[j][addcol] = dt.Rows[j][col].ToString();
                }
                //如果需要添加比重列
                if (columns.Contains(i))
                {
                    DataColumn addcolbz = new DataColumn(col.ColumnName + "-占比");
                    newdt.Columns.Add(addcolbz);

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        double item = 0;
                        Double.TryParse(dt.Rows[j][col].ToString(), out item);
                        string bz = (item / sum).ToString("00.00%");
                        newdt.Rows[j][addcolbz] = bz;
                    }
                }

            }
            return newdt;
        }
    }
}
