using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SelectPdf;

public class HtmlToPdfConverter
{
    public static void Convert(string htmlFilePath, string pdfFilePath)
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("headless");

        using (IWebDriver driver = new ChromeDriver(options))
        {
            driver.Navigate().GoToUrl("file://" + htmlFilePath);

            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile("screenshot.png", ScreenshotImageFormat.Png);

            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 10;
            converter.Options.MarginTop = 10;
            converter.Options.MarginBottom = 10;

            PdfDocument document = converter.ConvertUrl("file://" + htmlFilePath);
            document.Save(pdfFilePath);
            document.Close();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string htmlFilePath = @"C:\Users\brend\Downloads\CM report.html";
        string pdfFilePath = @"C:\Users\brend\Downloads\CM report1.pdf";

        HtmlToPdfConverter.Convert(htmlFilePath, pdfFilePath);
    }
}
