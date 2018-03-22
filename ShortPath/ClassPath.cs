using System;

namespace ShortPath
{
    //Класс пути
    public class ClassPath
    {
        //Номера граничных пунктов
        public int pA; 
        public int pB;
        //Длина пути
        public int Lpath;
        //Сила пробки
        public int Probka;
        //Признак использованности пути
        public bool used;
        //Конструктор
        public ClassPath(int ta, int tb, int lp)
        {
            pA = ta;
            pB = tb;
            Lpath = lp;
            used = false; //Изначально путь не использован.
            Probka = 1;  //Изначально пробки нет
        }
        //Запрос длины пути с учётом пробки
        public int GetLeng()
        {
            return Lpath * Probka;
        }
    }

}