using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;

namespace Excel
{
    /// <summary>
    /// npoi
    /// </summary>
    public class NpoiHelper
    {
        /// <summary>
        /// 导出用户资料
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableTitle"></param>
        /// <returns></returns>
        public static byte[] UserOutput(DataTable dataTable, string[] tableTitle)
        {
            NPOI.SS.UserModel.IWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow rows = null;
            for (int i = 1; i <= dataTable.Rows.Count + 1; i++)
            {
                //创建表头
                if (i - 1 == 0)
                {
                    Title = sheet.CreateRow(0);
                    for (int k = 1; k < tableTitle.Length + 1; k++)
                    {
                        Title.CreateCell(0).SetCellValue("序号");
                        Title.CreateCell(k).SetCellValue(tableTitle[k - 1]);
                    }
                    continue;
                }
                else
                {
                    rows = sheet.CreateRow(i - 1);

                    rows.CreateCell(0).SetCellValue(i - 1);
                    rows.CreateCell(1).SetCellValue(dataTable.Rows[i - 2][0].ToString());

                    rows.CreateCell(2).SetCellValue(dataTable.Rows[i - 2][9].ToString());

                    rows.CreateCell(3).SetCellValue(dataTable.Rows[i - 2][4].ToString());

                    rows.CreateCell(4).SetCellValue(Convert.ToInt32(dataTable.Rows[i - 2][5]) == 0 ? "女" : Convert.ToInt32(dataTable.Rows[i - 2][5]) == 1 ? "男" : "未知");

                    rows.CreateCell(5).SetCellValue(dataTable.Rows[i - 2][6].ToString());

                    rows.CreateCell(6).SetCellValue(dataTable.Rows[i - 2][7].ToString());

                    rows.CreateCell(7).SetCellValue(dataTable.Rows[i - 2][8].ToString());

                    rows.CreateCell(8).SetCellValue(dataTable.Rows[i - 2][12].ToString());

                    rows.CreateCell(9).SetCellValue(dataTable.Rows[i - 2][13].ToString());

                    rows.CreateCell(10).SetCellValue(dataTable.Rows[i - 2][14].ToString());

                    rows.CreateCell(11).SetCellValue(dataTable.Rows[i - 2][17].ToString());


                    //for (int j = 1; j <= dataTable.Columns.Count; j++)
                    //{
                    //    rows.CreateCell(0).SetCellValue(i - 1);
                    //    rows.CreateCell(j).SetCellValue(dataTable.Rows[i - 1][j - 1].ToString());
                    //}
                }
            }

            byte[] buffer = new byte[1024 * 5];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.GetBuffer();
                ms.Close();
            }
            return buffer;
        }
    }
}
