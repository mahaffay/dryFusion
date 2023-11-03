using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DryFusion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path ="";
            int lineSkip = 0;
            int endLength = 0;
            int firstRow = 0;
            int secondRow = 0;
            int thirdRow = 0;

            switch (comboBox1.SelectedItem.ToString())
            {
                case "Weather":
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"weather.dat");
                    lineSkip = 2;
                    endLength = 1;
                    firstRow = 0;
                    secondRow = 1;
                    thirdRow = 2;
                    break;
                case "Soccer":
                    path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"football.dat");
                    lineSkip = 1;
                    firstRow = 1;
                    secondRow = 6;
                    thirdRow = 7;
                    break;
            }


            List<DataEval> dataEval = new List<DataEval>();
            string[] files = File.ReadAllLines(path);

            for (int m = lineSkip; m < files.Length - endLength; m++)
            {
                string[] row = files[m].Replace("*", "").Replace("-", "").Trim().Split(' ');
                row = row.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                if (row.Length != 0)
                {
                    dataEval.Add(new DataEval()
                    {
                        col1 = (row[firstRow]),
                        num1 = Convert.ToInt16(row[secondRow]),
                        num2 = Convert.ToInt16(row[thirdRow]),
                        difference = Convert.ToInt16(row[secondRow]) - Convert.ToInt16(row[thirdRow])
                        
                    });
                }

                           
            }

            var result = dataEval.OrderBy(a => a.difference);

            var smallestDiff = result.Select(x => x.col1).First();

            MessageBox.Show("Column: " + smallestDiff.ToString() + " has the smallest difference");
        }

    }
}
