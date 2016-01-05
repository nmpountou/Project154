using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Assemblies needed!
using System.Diagnostics;
using System.IO;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;


namespace Sunrise.MultipleChoice
{
    /// <summary>
    /// Interaction logic for Export_to_Pdf.xaml
    /// </summary>
    public partial class Export_to_Pdf : Window
    {
        PdfDocument document;
        PdfPage page;
        XGraphics gfx;
        XFont font;

        public Export_to_Pdf()
        {
            InitializeComponent();
            document = new PdfDocument();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            font = new XFont("Verdana", 20, XFontStyle.Bold);
        }

        private void Export_to_pdf_button_submit_Click(object sender, RoutedEventArgs e)
        {
            gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(0, 0, page.Width, page.Height),XStringFormats.Center);
            string filename = "HelloWorld.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
        }
    }
}
