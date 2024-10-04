using System;
using System.IO;
using SelectPdf;
using System.Configuration;

namespace HTMLToPDF_Converter_using_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get source and destination paths from app.config
            string sourcePath = ConfigurationManager.AppSettings["SourcePath"];
            string destinationPath = ConfigurationManager.AppSettings["DestinationPath"];

            // Ensure the destination folder exists
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Get all HTML files in the source folder
            string[] htmlFiles = Directory.GetFiles(sourcePath, "*.html");

            foreach (var htmlFile in htmlFiles)
            {
                try
                {
                    // Define the destination PDF file name
                    string pdfFileName = Path.GetFileNameWithoutExtension(htmlFile) + ".pdf";
                    string pdfFilePath = Path.Combine(destinationPath, pdfFileName);

                    // Create an instance of HtmlToPdf converter
                    HtmlToPdf converter = new HtmlToPdf();

                    // Read the HTML file content and convert to PDF
                    PdfDocument pdfDoc = converter.ConvertUrl(htmlFile);

                    // Save the PDF file
                    pdfDoc.Save(pdfFilePath);
                    pdfDoc.Close();

                    Console.WriteLine($"Successfully converted: {htmlFile} to {pdfFilePath}");

                    // Delete the original HTML file after conversion
                    File.Delete(htmlFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error converting file {htmlFile}: {ex.Message}");
                }
            }

            Console.WriteLine("Conversion process completed.");
        }
    }
}