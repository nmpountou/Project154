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
        int questionaire;
        public Export_to_Pdf()
        {
            InitializeComponent();
            //create object for pdf.
            document = new PdfDocument();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            //Set by default values.
            font = new XFont("Arial", 12, XFontStyle.Regular);
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Portrait;
        }
        public Export_to_Pdf(int questionaire): this()
        {
            this.questionaire = questionaire;
        }

        private void Export_to_pdf_button_submit_Click(object sender, RoutedEventArgs e)
        {
            set_title_subject_author_keyword();
            set_font();
            page_size();
            page_orientation();
            header();
            insert_questions("Hello, World!");
            //gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(5, 5, page.Width, page.Height),XStringFormats.TopLeft);

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
        private void page_size()
        {
            if (pagesize_A0.IsSelected)
            {
                page.Size = PageSize.A0;
            }
            else if (pagesize_A1.IsSelected)
            {
                page.Size = PageSize.A1;
            }
            else if (pagesize_A2.IsSelected)
            {
                page.Size = PageSize.A2;
            }
            else if (pagesize_A3.IsSelected)
            {
                page.Size = PageSize.A3;
            }
            else if (pagesize_A4.IsSelected)
            {
                page.Size = PageSize.A4;
            }
            else if (pagesize_A5.IsSelected)
            {
                page.Size = PageSize.A5;
            }
        }
        private void page_orientation()
        {
            if (Convert.ToBoolean(export_to_pdf_Radiobutton_Portrait.IsChecked))
            {
                page.Orientation = PageOrientation.Portrait;
            }
            else
            {
                page.Orientation = PageOrientation.Landscape;
            }
        }
        private void header()
        {
            // Draw a line below the text
            gfx.DrawLine(XPens.Black, 0, 10, page.Width, page.Height);

            // Draw a line above the text
            gfx.DrawLine(XPens.Black, 0, 20, page.Width, page.Height);
        }
        private void insert_questions(String question)
        {
            string text1 = "asdfasdfasdfasdfasdfsadfasdfasdfasdfasdfasdfasdfasdfasdfasdfsdfsadf";
            string text2 = "sdfafasdfasdfasdfdasfffffffffffffffffffffffasdfasdfasdf";
            //XRect xrt = new XRect(5, 5, page.Width, page.Height);
            double y = 20;
            XRect xrt = new XRect(20, 0, page.Width, page.Height);
            for (int i=0;i<50;i++)
            {
                gfx.DrawString(" "+i.ToString(), font, XBrushes.Black, xrt, XStringFormats.TopLeft);
                xrt.Y += y;
                if (i%30==0&& i==0)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    xrt.Y = 0;
                }
            }
        }
    }
}
