using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace QRCodeGenerator2
{
    class Program
    {
        static string text;

        static void Main(string[] args)
        {
            while (true)
            {
                string datafile = "..\\..\\..\\data.txt";
                
                while (!File.Exists(datafile)) ;
                Console.WriteLine("File catched up");
                Thread.Sleep(10000);
                //Console.WriteLine("After 10 second delay");
                if (File.Exists(datafile))
                {
                    try
                    {
                        //text = File.ReadAllText(datafile);
                        using(FileStream fs = new FileStream(datafile,FileMode.OpenOrCreate,FileAccess.Read))
                        {
                            StreamReader sr = new StreamReader(fs);
                            text = sr.ReadLine();
                            Console.WriteLine(text);
                            try
                            {
                                ZXing.QrCode.QRCodeWriter dd = new ZXing.QrCode.QRCodeWriter();
                                String data = text;

                                ZXing.Common.BitMatrix byteIMGNew1 = dd.encode(data, ZXing.BarcodeFormat.QR_CODE, 1, 1);

                                String Imagename = "..\\..\\..\\qrcode" + ".png";
                                if (!File.Exists(Imagename)) File.Create(Imagename).Close();

                                byteIMGNew1.ToBitmap(ZXing.BarcodeFormat.QR_CODE, data).Save(Imagename, System.Drawing.Imaging.ImageFormat.Png);
                                byteIMGNew1.ToBitmap(ZXing.BarcodeFormat.QR_CODE, data).Dispose();

                                byteIMGNew1.clear();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    Thread.Sleep(10000);
                    //Console.WriteLine("After 10 second delay");
                    File.Delete(datafile);
                    Console.WriteLine("File is deleted");
                }
            }
            //Console.ReadKey();
        }
    }
}
