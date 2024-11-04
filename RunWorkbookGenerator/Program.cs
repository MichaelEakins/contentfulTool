using System;
using System.IO;
using OfficeOpenXml;

public class ExcelGenerator
{
    public void GenerateSampleWorkbookWithFields(string filePath)
    {
        // Set the license context for EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            // Create a worksheet
            var worksheet = package.Workbook.Worksheets.Add("TableSchema");

            // Define specific columns
            worksheet.Cells[1, 1].Value = "DATASET_NAME";
            worksheet.Cells[1, 2].Value = "TABLE_NAME";
            worksheet.Cells[1, 3].Value = "COLUMN_NAME";
            worksheet.Cells[1, 4].Value = "IS_NULLABLE";
            worksheet.Cells[1, 5].Value = "COLUMN_DEFAULT";
            worksheet.Cells[1, 6].Value = "DATATYPE";

            // Add sample data
            worksheet.Cells[2, 1].Value = "TEST1";
            worksheet.Cells[2, 2].Value = "users";
            worksheet.Cells[2, 3].Value = "id";
            worksheet.Cells[2, 4].Value = "NO";
            worksheet.Cells[2, 5].Value = "NULL";
            worksheet.Cells[2, 6].Value = "INT";

            worksheet.Cells[3, 1].Value = "TEST1";
            worksheet.Cells[3, 2].Value = "users";
            worksheet.Cells[3, 3].Value = "username";
            worksheet.Cells[3, 4].Value = "NO";
            worksheet.Cells[3, 5].Value = "NULL";
            worksheet.Cells[3, 6].Value = "VARCHAR(255)";

            worksheet.Cells[4, 1].Value = "TEST2";
            worksheet.Cells[4, 2].Value = "users";
            worksheet.Cells[4, 3].Value = "email";
            worksheet.Cells[4, 4].Value = "YES";
            worksheet.Cells[4, 5].Value = "'example@example.com'";
            worksheet.Cells[4, 6].Value = "VARCHAR(255)";

            worksheet.Cells[5, 1].Value = "TEST1";
            worksheet.Cells[5, 2].Value = "orders";
            worksheet.Cells[5, 3].Value = "order_id";
            worksheet.Cells[5, 4].Value = "NO";
            worksheet.Cells[5, 5].Value = "NULL";
            worksheet.Cells[5, 6].Value = "INT";

            worksheet.Cells[6, 1].Value = "TEST2";
            worksheet.Cells[6, 2].Value = "orders";
            worksheet.Cells[6, 3].Value = "order_date";
            worksheet.Cells[6, 4].Value = "YES";
            worksheet.Cells[6, 5].Value = "CURRENT_TIMESTAMP";
            worksheet.Cells[6, 6].Value = "DATETIME";

            // Format the header row
            using (var range = worksheet.Cells[1, 1, 1, 5])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Save the workbook
            FileInfo fileInfo = new FileInfo(filePath);
            package.SaveAs(fileInfo);
        }

        Console.WriteLine("Sample workbook with specified fields generated successfully!");
    }
}

class Program
{
    static void Main()
    {
        var generator = new ExcelGenerator();
        generator.GenerateSampleWorkbookWithFields("SchemaWorkbook.xlsx");
    }
}
