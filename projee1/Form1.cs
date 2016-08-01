using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
namespace projee1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //TOADdan gelenler
                string path = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                string path2 = DateTime.Now.ToString("dd.MM.yyyy");
                string FileName = path + DateTime.Now.ToString("dd.MM.yyyy");
                string NewFile = textBox1.Text;
                string temp = NewFile.Replace("YNAFARK.", "");
                SaveFileDialog a1 = new SaveFileDialog();
                a1.Filter = "Yazı Dosyaları(*txt)|*.txt";
                a1.DefaultExt = "txt";
                
                for (int i = 1; ; i++)
                {
                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path);
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(path2 + "-" + i.ToString() + "*");

                    if (filesInDir.Count() == 0)
                    {
                        FileStream fs1 = new FileStream(FileName + "-" + i + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter yazmaislemi = new StreamWriter(fs1);
                        yazmaislemi.WriteLine(temp);
                        yazmaislemi.Close();
                        if(textBox1.Text == "")
                      {
                          File.Delete(FileName + "-" + i + ".txt");
                          MessageBox.Show("Yazı girilmedi");
                      }
                        else   {  
                        MessageBox.Show("Kaydedilmiştir");
                        textBox1.Clear();
                        }
                        return;
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                throw ex;

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //ENSON ve ENBAS

                string radioButtonChoose = ("");
                if (radioButton1.Checked == true)

                    radioButtonChoose = radioButton1.Text;
                else
                    radioButtonChoose = radioButton2.Text;

               
                string ByWho = textBox3.Text;
                string Final1 = "-----------" + ByWho + "\r\n";
                string delete = textBox2.Text;
                string Final2 = Final1 + delete.Replace("YNATEST.", ""); ;

                string path = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                string path2 = DateTime.Now.ToString("dd.MM.yyyy");
                string FileName = path + DateTime.Now.ToString("dd.MM.yyyy");
                SaveFileDialog a2 = new SaveFileDialog();

                a2.FileName = "";
                a2.Filter = "Yazı Dosyaları(*txt)|*.txt";
                a2.DefaultExt = "txt";

                if(radioButton1.Checked ==false && radioButton2.Checked ==false )
                {

                MessageBox.Show("Seçim yapmadınız");
                
                }
                for (int i = 1; ; i++)
                {
                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path);
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(path2 + "-" + i.ToString() + "*");

                    if (filesInDir.Count() == 0)
                    {
                        FileStream fs1 = new FileStream(FileName + "-" + i + radioButtonChoose + "_" + ByWho + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                        StreamWriter yazmaislemi = new StreamWriter(fs1);
                        yazmaislemi.WriteLine(Final2);
                        yazmaislemi.Close();
                        if (radioButton1.Checked == false && radioButton2.Checked == false)
                        {
                            File.Delete(FileName + "-" + i + radioButtonChoose + "_" + ByWho + ".txt");
                            MessageBox.Show("Seçim yapmadınız");

                        }
                        else if (textBox2.Text == "" || textBox3.Text == "")
                        {
                            File.Delete(FileName + "-" + i + radioButtonChoose + "_" + ByWho + ".txt");
                            MessageBox.Show("Yazı Girilmedi");

                        }
                        else
                        {
                            MessageBox.Show("Kaydedilmiştir");
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //Haftalık ve History klasörüne gönderme

                //ENBAS

                string newpath = @"\\35TKNTFS1\\YNA_SQL_Script\\HaftaBazli\\";
                string path = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                DirectoryInfo merge = new DirectoryInfo(path);
                FileInfo[] filename1 = merge.GetFiles("*ENBAS*").OrderBy(p => p.CreationTime).ToArray();

                SaveFileDialog a1 = new SaveFileDialog();
                a1.Filter = "Yazı Dosyaları(*txt)|*.txt";
                a1.DefaultExt = "txt";
                FileStream fs1 = new FileStream(newpath + DateTime.Now.Year+ "-" + GetWeekNumber(DateTime.Now) + "ENBAS" + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter yazmaislemi = new StreamWriter(fs1);
                
                if (filename1.Count() == 0)
                {
                    yazmaislemi.Close();
                    File.Delete(newpath + DateTime.Now.Year + "-" + GetWeekNumber(DateTime.Now) + "ENBAS" + ".txt");
                }



                else
                {
                    for (int i = 0; i < filename1.Count(); i++)
                    {
                        string linesFinal = "";
                        string[] lines = System.IO.File.ReadAllLines(filename1[i].FullName);
                        for (int j = 0; j < lines.Count(); j++)
                        {
                            linesFinal = linesFinal + lines[j].ToString() + System.Environment.NewLine;

                        }
                        yazmaislemi.WriteLine(linesFinal);


                    }
                    yazmaislemi.Close();
                }
                string from = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                string destination = @"\\35TKNTFS1\\YNA_SQL_Script\\History\\";
                foreach (var file in Directory.GetFiles(from, "*ENBAS*"))
                {
                    string FileName = Path.GetFileName(file);
                    File.Move(file, Path.Combine(destination, FileName));
                }

                //ENSON

                SaveFileDialog a2 = new SaveFileDialog();
                a2.Filter = "Yazı Dosyaları(*txt)|*.txt";
                a2.DefaultExt = "txt";
                string newpath2 = @"\\35TKNTFS1\\YNA_SQL_Script\\HaftaBazli\\";
                string path2 = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                DirectoryInfo merge2 = new DirectoryInfo(path2);
                
                FileInfo[] filename2 = merge2.GetFiles("*ENSON*").OrderBy(p => p.CreationTime).ToArray();
                FileStream fs2 = new FileStream(newpath2 + DateTime.Now.Year + "-" + GetWeekNumber(DateTime.Now) + "ENSON" + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter yazmaislemi2 = new StreamWriter(fs2);

                if (filename2.Count() == 0)
                {
                    yazmaislemi2.Close();
                    File.Delete(newpath + DateTime.Now.Year + "-" + GetWeekNumber(DateTime.Now) + "ENSON" + ".txt");
                }

                else
                {
                    for (int i = 0; i < filename2.Count(); i++)
                    {
                        string linesFinal = "";
                        string[] lines = System.IO.File.ReadAllLines(filename2[i].FullName);
                        for (int j = 0; j < lines.Count(); j++)
                        {
                            linesFinal = linesFinal + lines[j].ToString() + System.Environment.NewLine;

                        }
                        yazmaislemi2.WriteLine(linesFinal);


                    }
                    yazmaislemi2.Close();
                }
                string from2 = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                string destination2 = @"\\35TKNTFS1\\YNA_SQL_Script\\History\\";
                foreach (var file in Directory.GetFiles(from2, "*ENSON*"))
                {
                    string FileName = Path.GetFileName(file);
                    File.Move(file, Path.Combine(destination2, FileName));
                }

                //TOAD

                SaveFileDialog a3 = new SaveFileDialog();
                a3.Filter = "Yazı Dosyaları(*txt)|*.txt";
                a3.DefaultExt = "txt";


                string newpath3 = @"\\35TKNTFS1\\YNA_SQL_Script\\HaftaBazli\\";
                string path3 = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                DirectoryInfo merge3 = new DirectoryInfo(path3);

                FileStream fs3 = new FileStream(newpath3 + DateTime.Now.Year + "-" + GetWeekNumber(DateTime.Now) + ".txt", FileMode.Create, FileAccess.Write);
                StreamWriter yazmaislemi3 = new StreamWriter(fs3);


                FileInfo[] filename3 = merge3.GetFiles("*.txt").OrderBy(p => p.CreationTime).ToArray();
                if (filename3.Count() == 0)
                {
                    yazmaislemi3.Close();
                    File.Delete(newpath + DateTime.Now.Year + "-" + GetWeekNumber(DateTime.Now) + ".txt");
                }

                else
                {

                    for (int i = 0; i < filename3.Count(); i++)
                    {
                        string linesFinal = "";
                        string[] lines = System.IO.File.ReadAllLines(filename3[i].FullName);
                        for (int j = 0; j < lines.Count(); j++)
                        {
                            linesFinal = linesFinal + lines[j].ToString() + System.Environment.NewLine;

                        }
                        yazmaislemi3.WriteLine(linesFinal);


                    }
                    yazmaislemi3.Close();
                }
                string from3 = @"\\35TKNTFS1\\YNA_SQL_Script\\";
                string destination3 = @"\\35TKNTFS1\\YNA_SQL_Script\\History\\";
                foreach (var file in Directory.GetFiles(from3, "*.txt"))
                {
                    string FileName = Path.GetFileName(file);
                    File.Move(file, Path.Combine(destination3, FileName));

                }
                MessageBox.Show("Kaydedilmiştir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw ex;

            }


        }

        //tarihin yılın kaçıncı haftasında olduğunu bulan method
        public static int GetWeekNumber(DateTime dtPassed)
        {
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dtPassed, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNum;
        }

    }
}

