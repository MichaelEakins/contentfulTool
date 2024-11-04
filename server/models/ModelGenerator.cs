using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

public class ModelGenerator
{
    public void GenerateModelsFromWorkbook(string filePath, string outputDirectory)
    {
        // Set license context for EPPlus
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Load the Excel file
        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            // List all worksheet names for debugging
            Console.WriteLine("Available worksheets:");
            foreach (var sheet in package.Workbook.Worksheets)
            {
                Console.WriteLine($"- '{sheet.Name}'"); // Use single quotes to detect spaces
            }

            var worksheet = package.Workbook.Worksheets["TableSchema"];
            if (worksheet == null)
            {
                Console.WriteLine("Worksheet 'TableSchema' not found.");
                return;
            }
            var data = new List<(string DatasetName, string ColumnName, string IsNullable, string DataType)>();

            int rowCount = worksheet.Dimension.Rows;
            for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip headers
            {
                string datasetName = worksheet.Cells[row, 1].Text;
                string columnName = worksheet.Cells[row, 3].Text;
                string isNullable = worksheet.Cells[row, 4].Text;
                string dataType = worksheet.Cells[row, 6].Text;

                data.Add((datasetName, columnName, isNullable, dataType));
            }

            // Group by DATASET_NAME and generate a model for each unique dataset
            var datasets = data.GroupBy(d => d.DatasetName);
            foreach (var dataset in datasets)
            {
                GenerateModelFile(dataset.Key, dataset.ToList(), outputDirectory);
            }
        }

        Console.WriteLine("Models generated successfully!");
    }

    private void GenerateModelFile(
        string datasetName,
        List<(string DatasetName, string ColumnName, string IsNullable, string DataType)> columns,
        string outputDirectory)
    {
        // Create the directory if it doesn't exist
        Directory.CreateDirectory(outputDirectory);

        // Format the dataset name to be PascalCase
        string modelName = datasetName.Replace(" ", "").Replace("-", "_");

        // Define the path for the model file
        string filePath = Path.Combine(outputDirectory, $"{modelName}.cs");

        // Generate the model class content
        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("using System;");
            writer.WriteLine();
            writer.WriteLine($"public class {modelName}");
            writer.WriteLine("{");

            foreach (var column in columns)
            {
                string nullable = column.IsNullable == "YES" ? "?" : string.Empty;
                string dataType = MapDataType(column.DataType) + nullable;

                writer.WriteLine($"    public {dataType} {column.ColumnName} {{ get; set; }}");
            }

            writer.WriteLine("}");
        }
    }

    private string MapDataType(string excelType)
    {
        // Map Excel data types to C# types
        return excelType switch
        {
            "INT" => "int",
            "VARCHAR(255)" => "string",
            "DATETIME" => "DateTime",
            _ => "string" // Default to string if the type is unrecognized
        };
    }
}

class ModelGeneratorProgram
{
    static void Main()
    {
        var generator = new ModelGenerator();

        string excelFilePath = "../../RunWorkbookGenerator/SchemaWorkbook.xlsx";
        string outputDirectory = ".";

        generator.GenerateModelsFromWorkbook(excelFilePath, outputDirectory);
    }
}
