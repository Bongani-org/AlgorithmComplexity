using Complexity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmComplexity
{
    public partial class Form1 : Form
    {
        private float xW;
        private float yH;
        public Form1()
        {
            InitializeComponent();
            xW = panel1.Width;
            yH = panel1.Height;
            DrawGraph();
        }
        private void DrawGraph()
        {
            panel1.CreateGraphics().Clear(Color.White);
            float h = yH;
            float w = xW;
            panel1.CreateGraphics().DrawLine(new Pen(Brushes.Black, 4), 20, 0, 20, h - 20);
            panel1.CreateGraphics().DrawLine(new Pen(Brushes.Black, 4), 20, h - 20, w, h - 20);
        }
        private int[] GenerateArray(int n, bool sorted)
        {
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 1001);
            }
            if (sorted)
            {
                Array.Sort(array);
            }
            return array;
        }
        public class Points {
            public float x;
            public float y;
            public Points(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
        }
        private void DrawPoints(Points[] points)
        {
            float maxY = 0;
            float maxX = points.Length;
            Points current = null;
            foreach (var item in points)
            {
                if (item.y > maxY)
                {
                    maxY = item.y;
                }
            }
            foreach (var item in points)
            {
                panel1.CreateGraphics().FillEllipse(Brushes.Blue, 20 + (panel1.Width - 30) * (item.x / maxX), (panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY), 5, 5);
                if (current == null)
                {
                    current = new Points((20 + (panel1.Width - 30) * (item.x / maxX)), ((panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY)));
                    panel1.CreateGraphics().FillEllipse(Brushes.Blue, 20 + (panel1.Width - 30) * (item.x / maxX), (panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY), 5, 5);
                }
                else
                {
                    panel1.CreateGraphics().FillEllipse(Brushes.Blue, 20 + (panel1.Width - 30) * (item.x / maxX), (panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY), 5, 5);
                    panel1.CreateGraphics().DrawLine(Pens.Black, current.x, current.y, 20 + (panel1.Width - 30) * (item.x / maxX), (panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY));
                    current = new Points((20 + (panel1.Width - 30) * (item.x / maxX)), ((panel1.Height - 20) - (panel1.Height - 25) * (item.y / maxY)));
                }
            }
        }
        //<Button1>
        private void button1_Click(object sender, EventArgs e)
        {
            SearchElementByIndex();
        }
        private void SearchElementByIndex()
        {
            DrawGraph();
            CTimer timer = new CTimer(1);
            int[] n = new int[] { 50000, 100000, 500000, 1000000, 2000000, 4000000, 5000000 };
            Points[] points = new Points[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                int[] array = GenerateArray(n[i], false);
                timer.Start();
                int r = FindIndex(array, n[i] / 2 - 1);//array[n[i] / 2 - 1];

                timer.Stop();
                int j = i + 1;
                float t = (float)timer.msDuration;
                points[i] = new Points(j, t);

            }
            DrawPoints(points);
        }
        private int FindIndex(int[] array, int index)
        {
            int n = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (i == index)
                {
                    n = i;
                }
            }
            return n;
        }
        //</Button1>
        //</Button2>
        private void button2_Click(object sender, EventArgs e)
        {
            FindIndexofElement();
        }
        private void FindIndexofElement()
        {
            DrawGraph();
            CTimer timer = new CTimer(1);
            int[] n = new int[] { 50000, 100000, 500000, 1000000, 2000000, 4000000, 5000000 };
            Points[] points = new Points[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                int[] array = GenerateArray(n[i], false);
                timer.Start();
                int r = FindElement(array, /*new Random().Next(0, 1001)*/900);
                timer.Stop();
                int j = i + 1;
                float t = (float)timer.msDuration;
                points[i] = new Points(j, t);

            }
            DrawPoints(points);
        }
        private int FindElement(int[] array, int element)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return i;
                }
            }
            return -1;
        }
        //</Button2>
        //</Button3>
        private void button3_Click(object sender, EventArgs e)
        {
            FindIndexUsingBinary();
        }
        private void FindIndexUsingBinary()
        {
            DrawGraph();
            CTimer timer = new CTimer(1);
            int[] n = new int[] { 50000, 100000, 500000, 1000000, 2000000, 4000000, 5000000 };
            Points[] points = new Points[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                int[] array = GenerateArray(n[i], true);
                timer.Start();
                int r = BinarySearch(array, new Random().Next(0, 1001));
                timer.Stop();
                int j = i + 1;
                float t = (float)timer.msDuration;
                points[i] = new Points(j, t);

            }
            DrawPoints(points);
        }
        private int BinarySearch(int[] arr, int key)
        {
            int minNum = 0;
            int maxNum = arr.Length - 1;

            while (minNum <= maxNum)
            {
                int mid = (minNum + maxNum) / 2;
                if (key == arr[mid])
                {
                    return ++mid;
                }
                else if (key < arr[mid])
                {
                    maxNum = mid - 1;
                }
                else
                {
                    minNum = mid + 1;
                }
            }
            return -1;

        }
        //</Button3>
        //</Button4>
        private void button4_Click(object sender, EventArgs e)
        {
            FindMode();
        }
        private void FindMode()
        {
            DrawGraph();
            CTimer timer = new CTimer(1);
            int[] n = new int[] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
            Points[] points = new Points[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                int[] array = GenerateArray(n[i], false);
                timer.Start();
                int r = Mode(array);
                timer.Stop();
                int j = i + 1;
                float t = (float)timer.msDuration;
                points[i] = new Points(j, t);

            }
            DrawPoints(points);
        }
        private int Mode(int[] array)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            for (int i = 0; i < 1001; i++)
            {
                dict.Add(i, 0);
            }
            foreach (var item in array)
            {
                dict[item] += 1;
            }
            int max = 0;
            int key = 0;
            foreach (var item in dict.Keys)
            {
                if (max < dict[item])
                {
                    max = dict[item];
                    key = item;
                }
            }
            return key;
        }
        //</Button4>
        //</Button5>
        private void button5_Click(object sender, EventArgs e)
        {
            FindSortedMode();
        }        
        private void FindSortedMode()
        {
            DrawGraph();
            CTimer timer = new CTimer(1);
            int[] n = new int[] { 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
            Points[] points = new Points[n.Length];
            for (int i = 0; i < n.Length; i++)
            {
                int[] array = GenerateArray(n[i], true);
                timer.Start();
                int r = SortedMode(array);
                timer.Stop();
                int j = i + 1;
                float t = (float)timer.msDuration;
                points[i] = new Points(j, t);

            }
            DrawPoints(points); ;
        }
        private int SortedMode(int[] array)
        {
            int[] arr = new int[1001];
            foreach (var item in array)
            {
                arr[item] += 1;
            }
            return Array.IndexOf(arr, arr.Max());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //</Button5>

    }
}
