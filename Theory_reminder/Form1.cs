using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Theory_reminder
{
    public partial class Form1 : Form
    {

        List<string> CONSTANT_list_Answer_Question = new List<string>();

        List<string> UPDATING_list_Question = new List<string>();

        List<string> list_Success_answers = new List<string>();
        List<string> list_Failure_answers = new List<string>();

        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        //Попробоватб замутить меню
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;

        }

 

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = System.IO.Directory.GetCurrentDirectory();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                Console.WriteLine("123");

                string pathWithTopics = folderBrowserDialog1.SelectedPath;

                string pathToCurrent_txt_file = "";

                string[] sections_topics = Directory.GetFiles(pathWithTopics, "*.txt");

                //Добавить сюда выбор (да/нет) для создания папки с текстфайлом
                if (sections_topics.Length == 0) MessageBox.Show("Темы в данном каталоге не обнаружены");

                //string nameVideoFile = "";
                string nameTextFile = "";

                for (int i = 0; i < sections_topics.Length; i++)
                {
                    //nameVideoFile = Path.GetFileName(sections_of_the_selected_topic[i]).ToString();
                    nameTextFile = Path.GetFileName(sections_topics[i]).ToString();

                    //nameTextFile = "v_" + nameVideoFile.Replace("mp4", "txt");

                    pathToCurrent_txt_file = sections_topics[i];

                    bool txtFileExists = File.Exists(pathToCurrent_txt_file);

                    if (txtFileExists)
                    {
                        //pathTo_txt_file = Environment.CurrentDirectory + "\\text_files\\" + "v_" + listBox1.SelectedItem.ToString().Replace("\t      *", "") + ".txt";

                        string[] allLinesTxtFile = File.ReadAllLines(pathToCurrent_txt_file);

                        string fullStringFromFile = File.ReadAllText(pathToCurrent_txt_file);

                        string[] individual_questions = fullStringFromFile.Split('~');

                        string answer = "";
                        string question = "";

                        //Dictionary<int, string> dict_Answer_Question = new Dictionary<int, string>(); 
                        

                        for (int t = 0; t < individual_questions.Length; t++)
                        {
                            question = individual_questions[t].Split('►')[0]; Console.WriteLine("question = " + question);
                            answer = individual_questions[t].Split('►')[1];   Console.WriteLine("answer = " + answer);

                            if(!CONSTANT_list_Answer_Question.Contains(question + "►" + answer)) CONSTANT_list_Answer_Question.Add(question + "►" + answer);

                            if (!UPDATING_list_Question.Contains(question + "►" + answer)) UPDATING_list_Question.Add(question + "►" + answer);

                            
                            //listBox1.Items.Add(question);


                        }
                        textBox2.Text = "0/" + individual_questions.Length;

                        
                        int rnd = random.Next(0, CONSTANT_list_Answer_Question.Count);
                        textBox3.Text = UPDATING_list_Question.ElementAt(rnd).ToString().Split('►')[0];
                        textBox4.Text = UPDATING_list_Question.ElementAt(rnd).ToString().Split('►')[1];

                        //MessageBox.Show("Загруженных вопросов : " + individual_questions.Length);

                    }
                    //else

                        //listBox1.Items.Add(Path.GetFileName(sections_of_the_selected_topic[i]).Replace(".mp4", ""));
                }

            }



        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null)
            {

   
                if(UPDATING_list_Question.Count != 0)
                {
                    if (textBox4.Text.Contains(textBox1.Text))
                    {
                        //MessageBox.Show("Верно");
                        textBox1.ForeColor = Color.Green;
                        if (!list_Success_answers.Contains(textBox3.Text)) list_Success_answers.Add(textBox3.Text);

                        if (UPDATING_list_Question.Count != 0) UPDATING_list_Question.Remove(textBox3.Text + '►' + textBox4.Text);

                    }
                    else
                    {
                        //MessageBox.Show("Не верно");
                        textBox1.ForeColor = Color.Red;

                        if (UPDATING_list_Question.Count != 0) UPDATING_list_Question.Remove(textBox3.Text + '►' + textBox4.Text);
                        if (!list_Failure_answers.Contains(textBox3.Text)) list_Failure_answers.Add(textBox3.Text);
                    }

                }





            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            if(UPDATING_list_Question.Count != 0)
            {
                int rnd = random.Next(0, UPDATING_list_Question.Count);
                textBox3.Text = UPDATING_list_Question.ElementAt(rnd).ToString().Split('►')[0];
                textBox4.Text = UPDATING_list_Question.ElementAt(rnd).ToString().Split('►')[1];
                textBox1.ForeColor = Color.Black;
            }
            else if (UPDATING_list_Question.Count == 0 && list_Success_answers.Count != 0 || list_Failure_answers.Count != 0)
            {
                MessageBox.Show("Все вопросы пройдены");


                string SuccessResult = "Верно отвечено на : " + '\r' + '\n';;
                for (int i = 0; i < list_Success_answers.Count; i++)
                {
                    SuccessResult += list_Success_answers[i] + '\r' + '\n';
                }

                //textBox4.Text = result;


                //string FailureResult = "Нужно поизучать :" + '\r' + '\n';

                SuccessResult += "Нужно поизучать :" + '\r' + '\n'; ;


                for (int i = 0; i < list_Failure_answers.Count; i++)
                {
                    SuccessResult += list_Failure_answers[i] + '\r' + '\n';
                }

                textBox4.Text = SuccessResult;



            }

        }
    }
}
