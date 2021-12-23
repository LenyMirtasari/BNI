using System;

namespace SoalDuaBNI
{
    class Program
    {
        static void Main(string[] args)
        {
            getData();
        }

        public static void getData()
        {
            int[] varArray = { 2, 8, 4, 5 };
            int[] arr2 = new int[100];
            for (int i = 0; i< varArray.Length; i++)
            {
                int hasil = 1;
                for (int j = 0; j<varArray.Length; j++) { 
                if(i!= j)
                {    
                    hasil *= varArray[j];
                }
                arr2[i] = hasil;                    
                }
                Console.WriteLine($"Elemen dengan indeks {i} = {arr2[i]}");                                 
            }
        }
    }
}
