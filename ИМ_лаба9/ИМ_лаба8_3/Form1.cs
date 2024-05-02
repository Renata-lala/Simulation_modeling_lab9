using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ИМ_лаба8_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            double[] probabilities = new double[5]; // Создаем массив с элементами

            double M_kryshka = 0;
            double M = 0;
            double D_kryshka = 0;
            double D = 0;
            double otnosit_pogresh_M = 0; 
            double otnosit_pogresh_D = 0;

            double chi = 9.488;

            probabilities[0] = (double)numericUpDown1.Value;
            probabilities[1] = (double)numericUpDown2.Value;
            probabilities[2] = (double)numericUpDown3.Value;
            probabilities[3] = (double)numericUpDown4.Value;

            double prob5 = 1 - probabilities[0] - probabilities[1] - probabilities[2] - probabilities[3];
            probabilities[4] = prob5;

            double N = (double)numericUpDown6.Value;
            int[] statistics = new int[5];
            double a;

            Random rnd = new Random(); // переместил создание объекта Random вне цикла

            for (int i = 0; i < N; i++)
            {
                a = rnd.NextDouble();
                double sum = 0;
                for (int k = 0; k < 5; k++)
                {
                    sum += probabilities[k];
                    if (a <= sum)
                    {
                        statistics[k]++;
                        break;
                    }
                }

            }
            

            double[] frequency = new double[5];
            Series s = new Series();
            s.ChartType = SeriesChartType.Column;

            for (int jj = 0; jj < 5; jj++)
            {
                frequency[jj] = (double)statistics[jj] / (double)N;
            }
            for (int jj = 0; jj < 5; jj++)
            {
                s.Points.AddXY(jj, frequency[jj]);
            }
           
            //ищем всякие показатели
            for (int jj = 0; jj < 5; jj++)
            {
                M_kryshka += probabilities[jj] * statistics[jj];
                M += statistics[jj] * frequency[jj];
                D_kryshka += (statistics[jj]* statistics[jj] * probabilities[jj]);
                D += (statistics[jj] * statistics[jj] * frequency[jj]);

            }
            D_kryshka -= M_kryshka * M_kryshka;
            D -= M * M;

            otnosit_pogresh_M = Math.Abs(M - M_kryshka) / Math.Abs(M);
            otnosit_pogresh_D = Math.Abs(D - D_kryshka) / Math.Abs(D);

            double X = 0;

            for (int i = 0; i < 5; i++)
            {
                X += (statistics[i] * statistics[i]) / (N * probabilities[i]);
            }
            X -= N;
            textBox1.Text = ""+X;
            if (X < chi)
            {
                label11.Text = "Истина";
            }
            else
            {
                label11.Text = "Ложь";
            }

            numericUpDown7.Value = (decimal)frequency[0];
            numericUpDown8.Value = (decimal)frequency[1];
            numericUpDown9.Value = (decimal)frequency[2];
            numericUpDown10.Value = (decimal)frequency[3];
            numericUpDown11.Value = (decimal)frequency[4];

            numericUpDown12.Value = (decimal)M;
            numericUpDown13.Value = (decimal)D;

            numericUpDown14.Value = (decimal)M_kryshka;
            numericUpDown15.Value = (decimal)D_kryshka;

            numericUpDown16.Value = (decimal)otnosit_pogresh_M;
            numericUpDown15.Value = (decimal)otnosit_pogresh_D;

            numericUpDown5.Value = (decimal)probabilities[4];


            chart1.Series.Add(s);

            numericUpDown13.Value = (decimal)D;
        }


    }
}
