using System;

namespace soalsatuBNI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool loop = false;
            do { 
                try { 
                    Console.Write($"Masukkan nomor sembarang :");
                    int angka = int.Parse(Console.ReadLine());
                    for (int i=1; i<= angka; i++)
                    {
                        for (int j = 1; j <= 9; j++)
                        {
                            if (j == i + 1 || j== i+2)
                            {
                                Console.Write('*');
                            }
                            else
                            {

                            Console.Write(j);
                            }
                        }
                        Console.WriteLine();
                    }
                    loop = false;
                }
                catch
                {
                    Console.WriteLine("Anda harus memasukkan angka");
                     loop = true;
                }
            }while(loop);


        }
    }
}
