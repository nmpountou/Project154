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
using Microsoft.WindowsAPICodePack.Dialogs;

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
        string filename;
        public Export_to_Pdf()
        {
            InitializeComponent();
            document = new PdfDocument();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            //Set by default the font object
            font = new XFont("Arial", 12, XFontStyle.Regular);
            
        }

        private void Export_to_pdf_button_submit_Click(object sender, RoutedEventArgs e)
        {
            set_title_subject_author_keyword();
            set_font();            

            gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(5, 5, page.Width, page.Height),XStringFormats.TopLeft);

            if (Export_to_pdf__textbox_filename.Text=="")
            {
                filename = "HelloWorld.pdf";
            }
            else
            {
                filename = Export_to_pdf__textbox_filename.Text+".pdf";
            }

            document.Save(Export_to_pdf__select_folder_save.Text+filename);
            // ...and start a viewer.
           Process.Start(Export_to_pdf__select_folder_save.Text+filename);

        }

        private void Export_to_pdf_Select_Folder_name(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Choose a folder to save the pdf file.";
            dlg.IsFolderPicker = true;
            //dlg.InitialDirectory = currentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            //dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Export_to_pdf__select_folder_save.Text = dlg.FileName;
                // Do something with selected folder string
            }

        }
        private void set_font()
        {
            if (fontstyle_bold.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.Bold);
            }
            else if(fontstyle_bolditalic.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.BoldItalic);
            }
            else if (fontstyle_italic.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.Italic);
            }
            else if (fontstyle_regural.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.Regular);
            }
            else if (fontstyle_strikeout.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.Strikeout);
            }
            else if (fontstyle_underline.IsSelected)
            {
                font = new XFont(Export_to_pdf_combobox_font.Text, Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text), XFontStyle.Underline);
            }
        }
        private void set_title_subject_author_keyword()
        {
            document.Info.Title = Export_to_pdf__textbox_title.Text;
            document.Info.Author = Export_to_pdf__textbox_author.Text;
            document.Info.Subject = Export_to_pdf__textbox_subject.Text;
            document.Info.Keywords = Export_to_pdf__textbox_keyword.Text;
        }
    }
}
