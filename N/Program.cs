using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N //Сформировать массив случайных целых чисел (размер  задается пользователем). Вычислить сумму чисел массива и максимальное число в массиве.  Реализовать  решение  задачи  с  использованием  механизма  задач продолжения. (Поскольку обе задачи (поиск суммы и поиск максимального) могут работать, только получив на вход массив чисел, то обе эти задачи должны быть запущены как продолжение задачи, формирующей массив (task1). Еще бы я совместила методы формирования и вывода на экран массива в один метод.)
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(GetSum);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(GetMax);
            Task<int> task3 = task1.ContinueWith<int>(func3);

           



            task1.Start();
            Console.ReadKey();

        }

        static int[] GetArray(object a)    //формирование массива
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
            return array;
        }
        static int GetSum(Task<int[]> task)   //получение суммы
        {
            int sum = 0;
            int[] array = task.Result;
            for (int i = 0; i < array.Length; i++)
            {
                
                 sum += array[i];
             
            }
            Console.WriteLine();
            return sum;
        }
        static int GetMax(Task<int[]> task)   //получение максимального числа
        {

            int[] array = task.Result;
            for (int i = 0; i < array.Count() - 1; i++)
            {
                for (int j = i + 1; j < array.Count(); j++)
                {
                    if (array[i] > array[j])
                    {
                        int t = array[i];
                        array[i] = array[j];
                        array[j] = t;
                    }
                }

            }
            Console.WriteLine(array[0]);
            return array[0];
        }
        //static void PrintArray(Task<int[]> task) //вывод на экран
        //{
        //    int[] array = task.Result;
        //    for (int i = 0; i < array.Count(); i++)
        //    {

        //        Console.Write($"{array[i]} {array[0]}");
        //    }
        //}

    }
}
