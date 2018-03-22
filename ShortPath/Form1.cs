using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShortPath
{

    public partial class Form1 : Form
    {
        //Количество узлов = пунктов
        protected int N = 10;

        //Список путей
        ListPath Puti = new ListPath();
        //Список пунктов
        public ClassPunkt[] Punkts; 

        public Form1()
        {
            InitializeComponent();
        }

        //Обработка изменения размеров окна
        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            //Переинициализация окна для рисования картинки
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            DrawGraf();
        }

        //Начальная инициализация формы
        private void Form1_Load(object sender, EventArgs e)
        {
            //Очистка текстовых полей
            label1.Text = "";
            label2.Text = "";
            label5.Text = "";
            //Инициализация картинки
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            /*
             * Тестовый пример с известным решением
                        Puti.Add(new ClassPath(1, 2, 10));
                        Puti.Add(new ClassPath(1, 4, 30));
                        Puti.Add(new ClassPath(1, 5, 100));
                        Puti.Add(new ClassPath(2, 3, 50));
                        Puti.Add(new ClassPath(3, 5, 10));
                        Puti.Add(new ClassPath(4, 3, 20));
                        Puti.Add(new ClassPath(4, 5, 60));
            */
            /*
             * Тестовый пример с известным решением
            Puti.Add(new ClassPath(1, 2, 20));
            Puti.Add(new ClassPath(1, 3, 10));
            Puti.Add(new ClassPath(1, 4, 40));
            Puti.Add(new ClassPath(2, 4, 70));
            Puti.Add(new ClassPath(2, 5, 25));
            Puti.Add(new ClassPath(3, 4, 50));
            Puti.Add(new ClassPath(3, 5, 100));
            Puti.Add(new ClassPath(3, 6, 40));
            Puti.Add(new ClassPath(4, 6, 50));
            Puti.Add(new ClassPath(5, 6, 40));
            */
            //Создание объекта для генерации чисел
            Random rnd = new Random();
            int putey;
            int rndTo;
            int rndLeng;
            //Генерация путей из каждой точки
            for (int i=1; i<=N; i++)
            {
                //Получить случайное число 
                putey = rnd.Next(2, N/3); //Количество путей из точки
                for(int j=0;j<putey;j++)
                {
                    //Случайный выбор куда
                    rndTo= rnd.Next(1, N);
                    //Если генератор сгенерировал путь в себя, не брать этот путь.
                    if (rndTo!=i)
                    {
                        rndLeng= rnd.Next(1, 100);//Генерация длины пути
                        Puti.Add(new ClassPath(i, rndTo, rndLeng)); //Добавление пути
                    }
                }

            }

            //Создание масива пунктов
            Punkts = new ClassPunkt[N];
            for(int i=1;i<=N; i++)
            {
                Punkts[i-1] = new ClassPunkt();
            }
            //Вывзов процедуры рисования графа
            DrawGraf();
        }

        //Процедура рисования графа.
        private void DrawGraf()
        { 
            //Инициализация рисования
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            //Рисование белого фона
            g.FillRectangle(Brushes.White , 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            //Рисование рамки поля
            g.DrawRectangle(Pens.Black, 0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            //Вычисление диаметра круга для рисования графа. Зависит от размеров картинки
            //Выбирается минимальный размер картинки
            int DGr = pictureBox1.Height;
            if (DGr > pictureBox1.Width) DGr = pictureBox1.Width;
            int Pole = 30; //Поле для картинки графа
            DGr -= Pole * 2; //Поправка диаметра круга поля 
            double Rgr = DGr / 2.0; //Вычисление радиуса круга
            double StepFi = (2.0 * Math.PI) / N; //Вчисление угловой разницы между пунктами
            double UgolFi; 
            int DPnt = 10; //Диаметр пункта
            float PolDp = (float)(DPnt / 2.0); //Половина размера пункта
            String strnm;
            Font ft = new Font("Arial", 20); // Создание фонта для надписей точек
            Brush br = new SolidBrush(Color.Tomato); //Создание кисти
            //Рисование точек на круге
            for (int i = 0; i < N; i++)
            {
                UgolFi = StepFi * i; //Вычисление угла
                //Вычисление координат, через угол и радиус круга
                Punkts[i].X = (float)(Rgr * Math.Sin(UgolFi)+Rgr + Pole);
                Punkts[i].Y = (float)(-Rgr * Math.Cos(UgolFi)+ Rgr +Pole);
                //Рисование точки 
                g.FillEllipse(Brushes.Green, Punkts[i].X- PolDp, Punkts[i].Y- PolDp, DPnt, DPnt);
                //Текст номера точки
                strnm=(i+1).ToString();
                //Вывод номера точки
                g.DrawString(strnm, ft,br, Punkts[i].X, Punkts[i].Y);
            }
            br = new SolidBrush(Color.Blue); //Кисть для длин путей
            ft = new Font("Arial", 10); // Фонт для длин путей
            Pen Pnpath; //Перо для рисования линии
            //Рисование путей иих длин
            for (int i = 0; i < Puti.Count(); i++)
            {
                //Выбор пути
                ClassPath cr = Puti.GetPut(i);

                Pnpath = Pens.DarkGray; //Серое перо для линии пути
                if (cr.Probka > 1) Pnpath = Pens.Yellow; //Если на линии пробка, то линия жёлтая
                //Рисование линии пути от точки до точки
                g.DrawLine(Pnpath, Punkts[cr.pA-1].X, Punkts[cr.pA-1].Y, Punkts[cr.pB-1].X, Punkts[cr.pB-1].Y);
                //Строка - длина пути
                strnm = cr.Lpath.ToString();
                //Рисование пути от точки до точки
                g.DrawString(strnm, ft, br, (float)((Punkts[cr.pA - 1].X + Punkts[cr.pB - 1].X) /2.0), (float)((Punkts[cr.pA - 1].Y + Punkts[cr.pB - 1].Y) / 2.0));
            }


        }

        //Поиск пути  обработчике нажатия кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            //Тексты с номерами путей не должны быть пустыми
            if (textBoxPnktFrom.Text.Length <= 0) return;
            if (textBoxPnktTo.Text.Length <= 0) return;
            //Конвертирвоание номеров пунктов 
            int StrtP = 1;
            int FinPnt = 1;
            try
            {
                StrtP = Int32.Parse(textBoxPnktFrom.Text);
                FinPnt = Int32.Parse(textBoxPnktTo.Text);
            }
            catch(FormatException Fex)
            {
                //Если ошибка конвертирования, то выход
                return;
            }

            //Сброс прежних пробок
            Puti.ResetProbci();

            //Ввод пробки
            if (textBoxProbOt.Text.Length >0 &&
                textBoxProbDo.Text.Length > 0 &&
                textBoxProbSila.Text.Length > 0)
            {
                try
                {
                    //ВВод данных о пробке
                    int ProbOt= Int32.Parse(textBoxProbOt.Text);
                    int ProbDo = Int32.Parse(textBoxProbDo.Text);
                    int ProbSila = Int32.Parse(textBoxProbSila.Text);
                    if(ProbSila>1)
                    {
                        //Установка пробки. Если данные неверны, то просто не найдётся такой путь, и пробки не будет
                        Puti.SetProbca(ProbOt, ProbDo, ProbSila);
                    }
                }
                catch (FormatException Fex)
                {
                    //Если ошибка конвертирования, то пробки нет
                }
            }
            //Проверка введённых данных на корректность
            if (StrtP > N) return;
            if (FinPnt > N) return;
            if (StrtP < 1) return;
            if (FinPnt < 1) return;
            //Очиста от предыдущих вычислений
            //Перерисование графа. Чтобы стереть прежний путь
            DrawGraf();
            Puti.ClearPath(); //Сброс данных у всех путей 

            int[] Dp = new int[N + 1]; //Массив длин путей
            int[] Pth = new int[N + 1]; //МАссив переходов
            List<int> LstUsel= new List<int>(); // Список обработанных вершин
            int i,tmp;
            //Инициализация массивов
            for (i = 0; i <= N; i++)
            {
                Pth[i] = 0;
                Dp[i] = ListPath.MaxPath; //Заведомо большое значение длины
            }
            //Начальные значения для алгоритма
            int T = StrtP;
            Pth[T] = T;
            Dp[T] = 0;
            int PrevStep = 0;
            LstUsel.Add(T);
            // Алгоритм дейстера. 
            //Работает, пока все вершины не будут обработаны 
            while (LstUsel.Count < N)
            {
                //Параметры для поиска минимальной длины пути на шаге алгоритма
                int imin = 0;
                int LMin = ListPath.MaxPath;

                //Вычисление длин путей от точки Т до всех остальных 
                for (i = 1; i <= N; i++)
                {
                    //Не учитывать точки, которые уже обработаны
                    if (!LstUsel.Contains(i))
                    {
                        //Вычисление длины пути - из массива, + длина от предыдущего узла.
                        tmp = Puti.GetLeng(T, i) + PrevStep;
                        if (tmp < Dp[i])
                        {
                            //Если длина пути в вершину стала меньше, то запомнить новую длину и вершину
                            Pth[i] = T;
                            Dp[i] = tmp;
                        }
                        //Поиск минимального пути на шаге.
                        if (Dp[i] < LMin)
                        {
                            //Если это меньше предущего значения, то запомнить
                            LMin = Dp[i];
                            imin = i;
                        }
                    }
                }
                //Добавиить обработанный узел в список обработанных узлов.
                LstUsel.Add(imin);
                //Запомнить длину нового предыдущего пути и вершину
                PrevStep = Dp[imin];
                T = imin;
            };

            /*
             * Тестовый вывод данных
            String Rasst = "";
            String Ptths = "";
            for (i=1; i<=N; i++)
            {
                Rasst +=i +":"+ Dp[i] +", ";
                Ptths += i + ":" + Pth[i] + ", ";
            }
            label1.Text = Rasst;
            label2.Text = Ptths;
        */
            //Вывод длины найденного пути
            label5.Text = "Длина пути = " + Dp[FinPnt];
            //Рисование найденного пути с конца
            int crP = FinPnt;
            int tp=1; //Контрольная переменна на случай ошибок
            //Инициализация рисования.
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            while (crP!= StrtP && tp>0)
            {
                tp = Pth[crP]; //Вершина откуда приходит в последнюю точку
                //Контроль и рисование отреза найденного пути
                if(tp>0)
                  g.DrawLine(Pens.Red, Punkts[crP - 1].X, Punkts[crP - 1].Y, Punkts[tp - 1].X, Punkts[tp - 1].Y);
                //Сдвиг по пути в направлении начала.
                crP = tp;
            }
            //Вызов перерисования окна графа
            pictureBox1.Invalidate();
        }

        //ВЫход из программы.
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
