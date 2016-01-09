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
using Quastionnaire.Model;
using System.Runtime.Serialization.Formatters.Binary;

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
        XFont header_font;
        string filename;
        Quastionnaire.Model.Questionaire questionaire;
        //Where the header finishes re-initialize after.
        double x=0;
        double y=0;
        public Export_to_Pdf()
        {
            InitializeComponent();
            //create object for pdf.
            document = new PdfDocument();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            //Set by default values.
            font = new XFont("Arial", 12, XFontStyle.Regular);
            header_font = new XFont("Arial",12, XFontStyle.Regular);
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Portrait;

            //Later need to be a comment!
            List<Question> question_list = new List<Question>();
            List<Answer> answer_list = new List<Answer>();
            Answer ans = new Answer();
            ans.Answer_descr = "einai to dsfgdsfgsd";
            answer_list.Add(ans);
            answer_list.Add(ans);
            answer_list.Add(ans);
            Question quest = new Question(){ AnswerList = answer_list };
            quest.Question_descr = "H ekfoonisi tis erotisis aytis einai blablbalbablbalblab";
            for (int i = 0; i < 10; i++)
            {
                question_list.Add(quest);
            }

            //for (int i = 0; i < questionaire.QuestionList.Count; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        questionaire.QuestionList.ElementAt(i).AnswerList.Add(ans);
            //    }
            //}
            questionaire = new Quastionnaire.Model.Questionaire() { Questionaire_descr = "sdfsakdfjdflshsadflkjashflasdfkh", QuestionList = question_list };
        }
        public Export_to_Pdf(Quastionnaire.Model.Questionaire question): this()
        {
            this.questionaire = question;            
        }
     

        private void Export_to_pdf_button_submit_Click(object sender, RoutedEventArgs e)
        {
            set_title_subject_author_keyword();
            set_font();
            set_header_font();
            page_size();
            page_orientation();
            header();
            //initialize_question(questionaire);
            insert_questions(questionaire);
            //gfx.DrawString("Hello, World!", font, XBrushes.Black,new XRect(5, 5, page.Width, page.Height),XStringFormats.TopLeft);
            initialize_parameters_file();

            document.Save(Export_to_pdf__select_folder_save.Text + filename);

            // ...and start a viewer.
            Process.Start(Export_to_pdf__select_folder_save.Text+filename);
            //in case we want to calculate the pdf again.
            //When process start call it disposes the object. 
            dispose_objects_load();

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
        private void set_header_font()
        {
            if (header_fontstyle_bold.IsSelected)
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.Bold);
            }
            else if (header_fontstyle_bolditalic.IsSelected) 
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.BoldItalic);
            }
            else if (header_fontstyle_italic.IsSelected)
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.Italic);
            }
            else if (header_fontstyle_regural.IsSelected)
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.Regular);
            }
            else if (header_fontstyle_strikeout.IsSelected)
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.Strikeout);
            }
            else if (header_fontstyle_underline.IsSelected)
            {
                header_font = new XFont(Export_to_pdf_Header_combobox_font.Text, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), XFontStyle.Underline);
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

            int x_margin = 40;
            int y_margin = 40;
            int y_next_step =0;
            // Draw a line above the text
            gfx.DrawLine(XPens.Black, x_margin, y_margin, page.Width-x_margin, y_margin);
            //Write title.
            XRect xrt = new XRect(x_margin, y_next_step=y_margin+10, page.Width-x_margin, Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text));
            gfx.DrawString(Export_to_pdf_Header_textbox.Text, header_font, XBrushes.Black, xrt, XStringFormats.Center);
            //Draw a line below text
            gfx.DrawLine(XPens.Black, x_margin, y_next_step + Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text), page.Width-x_margin, y_next_step + Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text));
            //Name of the proffessor
            xrt.X = x_margin;
            xrt.Y = y_next_step + 10 + Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text);
            //xrt.Width = page.Width - x_margin;
            xrt.Width = page.Width*0.8;
            xrt.Height = 10+ Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text) ;
            gfx.DrawString(Export_to_pdf_Header_textbox_nameproffesor.Text, font, XBrushes.Blue, xrt, XStringFormats.TopLeft);

            xrt.X = page.Width*0.80;
            xrt.Width = page.Width*0.20;
            xrt.Height = 10 + Convert.ToDouble(Export_to_pdf_comobox_fontsize.Text);
            gfx.DrawString(Export_to_pdf_datepicker.Text,font, XBrushes.Black, xrt, XStringFormats.TopLeft);

            //take the values to know where the questions should start printing.
            x = x_margin;
            y = y_next_step + 30 + Convert.ToDouble(Export_to_pdf_Header_comobox_fontsize.Text); ;

        }
        private void insert_questions(Quastionnaire.Model.Questionaire questionaire)
        {
            //double yh = 20;//Space between question
            //String question = "sdfdasfasdfdasfdsfdasfdasfsfasf";
            //bool first_page=false;
            //XRect xrt = new XRect(x, y, page.Width, page.Height);

            //for (int i=0;i<100;i++)
            //{
            //    gfx.DrawString(" "+i.ToString(), font, XBrushes.Black, xrt, XStringFormats.TopLeft);
            //    xrt.Y += yh;
            //    if (i%35==0 && i!=0 && first_page==false)
            //    {
            //        page = document.AddPage();
            //        gfx = XGraphics.FromPdfPage(page);
            //        //xrt.Y = 0;
            //        xrt.X = 40;xrt.Y = 40;
            //        first_page = true;
            //    }
            //    else if (first_page == true && i % 75 == 0 &&i!=35)
            //    {
            //        page = document.AddPage();
            //        gfx = XGraphics.FromPdfPage(page);
            //        xrt.X = 40; xrt.Y = 40;
            //    }
            //}

            double yh = 20;
            XRect xrt = new XRect(x, y, page.Width, page.Height);
            for (int i=0;i<questionaire.QuestionList.Count;i++)
            {
               gfx.DrawString((i+1).ToString()+ ". "+ questionaire.QuestionList.ElementAt(i).Question_descr , font, XBrushes.Black, xrt, XStringFormats.TopLeft);
               xrt.Y += yh;
                for (int j=0;j< questionaire.QuestionList.ElementAt(i).AnswerList.Count;j++)
                {
                    gfx.DrawString((j + 1).ToString() + ". " + questionaire.QuestionList.ElementAt(i).AnswerList.ElementAt(j).Answer_descr, font, XBrushes.Black, xrt, XStringFormats.TopLeft);
                    xrt.Y += yh;
                }
            }
        }
        private void dispose_objects_load()
        {
            document = new PdfDocument();
            page = document.AddPage();
            gfx = XGraphics.FromPdfPage(page);
            //Set by default values.
            font = new XFont("Arial", 12, XFontStyle.Regular);
            header_font = new XFont("Arial", 12, XFontStyle.Regular);
            page.Size = PageSize.A4;
            page.Orientation = PageOrientation.Portrait;
        }
        private void initialize_parameters_file()
        {
            if (Export_to_pdf__textbox_filename.Text == "")
            {
                filename = "HelloWorld.pdf";
            }
            else
            {
                filename = Export_to_pdf__textbox_filename.Text + ".pdf";
            }
        }
        private void initialize_question(Quastionnaire.Model.Questionaire questionaire)
        {
            int number_of_erotiseis = 20;
            int number_of_answer = 4;
            //Dimiourgoume tin lista ton apantiseon.
            List<Answer> answs = new List<Answer>();
            Question quest = new Question() { AnswerList = answs};
            quest.Question_descr = "H ekfoonisi tis erotisis aytis einai blablbalbablbalblab";
            for (int i = 0; i < number_of_erotiseis; i++)
            {
                questionaire.QuestionList.Add(quest);
            }
            //MessageBox.Show(lst.Count().ToString());
            //Put the question in questionaire
            //questionaire.QuestionList.AddRange(lst);
            //List<int> deepCopy = new List<int>(originalList);
            MessageBox.Show(questionaire.QuestionList.Count.ToString());
            //List<Answer> answs = new List<Answer>();

            Answer ans = new Answer();
            ans.Answer_descr = "einai to dsfgdsfgsd";

            for (int i=0;i< questionaire.QuestionList.Count;i++)
            {
                for (int j = 0; j < number_of_answer; j++)
                {
                    questionaire.QuestionList.ElementAt(i).AnswerList.Add(ans);
                }
            }
        }
        public List<Question> DeepCopy_Question()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);

            ms.Position = 0;
            return (List<Question>)bf.Deserialize(ms);
        }
        public List<Answer> DeepCopy_Answer()
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, this);

            ms.Position = 0;
            return (List<Answer>)bf.Deserialize(ms);
        }

    }
}
