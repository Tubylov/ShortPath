using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortPath
{
    //Класс списка путей
    class ListPath
    {
        //Спсок путей
        List<ClassPath> lstPuti = new List<ClassPath>();

        //Добавление пути
        public void Add(ClassPath p)
        {
            //Проверка. Если подобный путь уже был, то не добавлять
            for (int i = 0; i < lstPuti.Count; i++)
            {
                ClassPath cr = lstPuti[i];
                if ((cr.pA == p.pA && cr.pB == p.pB) ||
                   (cr.pB == p.pA && cr.pA == p.pB))
                {
                    return;
                }
            }

            lstPuti.Add(p);
        }

        //Заведомо большое число. Должно быть гарантированно больше длины любого пути
        public static int MaxPath = 100000;

        //Запрос длины пути от  А до B
        public int GetLeng(int ta, int tb)
        {
            int lR = MaxPath;
            int i;

            //Цикл поиска пути. 
            for (i = 0; (i < lstPuti.Count && lR == MaxPath); i++)
            {
                ClassPath cr= lstPuti[i];
                //Если путь использован, то пропускается 
                if (!cr.used)
                {
                    //Ищутся пути  как из A в B так и из B в A
                    if ((cr.pA == ta && cr.pB == tb) ||
                        (cr.pB == ta && cr.pA == tb))
                    {
                        //Путь найден
                        lR = cr.GetLeng();
                        //Путь использован
                        cr.used = true;
                    }
                }
            }
            //Возврат найденного пути
            return lR;
        }

        //Возврат количества путей
        public int Count()
        {
            return lstPuti.Count;
        }

        //Возврат запрошенного пути
        public ClassPath GetPut(int i)
        {
            return lstPuti[i];
        }

        //Сброс использования путей для всех путей
        //Для проведения повторных вычислений.
        public void ClearPath()
        {
            for (int i = 0; i < lstPuti.Count; i++)
            {
                ClassPath cr = lstPuti[i];
                cr.used = false;
            }
        }

        //Установка пробки на пути от  А до B
        public void SetProbca(int ta, int tb, int sila)
        {
            //Цикл поиска пути. 
            for (int i = 0; i < lstPuti.Count; i++)
            {
                ClassPath cr = lstPuti[i];
                //Ищутся пути  как из A в B так и из B в A
                if ((cr.pA == ta && cr.pB == tb) ||
                    (cr.pB == ta && cr.pA == tb))
                {
                    cr.Probka = sila;
                }
            }
        }

        //Сброс пробок
        public void ResetProbci()
        {
            //Цикл поиска пути. 
            for (int i = 0; i < lstPuti.Count; i++)
            {
                ClassPath cr = lstPuti[i];
                cr.Probka = 1;
            }
        }


    }
}
