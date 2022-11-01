using System;
using System.IO;

namespace SharpConsoleDemoOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string mySig = "Signature.txt";
            string scaleHtz = "A4-440.txt";
            string scaleNotes = "Scale Notes.txt";
            //displayFile(mySig);
            string[,] scaleArray = inputScale(scaleNotes, scaleHtz);
            //displayArray(scaleArray);

            //System.Console.Beep(329, BPMtoMS(100));
            //System.Console.Beep(329, 1000);
            //scale is E4 thru F5, F5 is sharp.
            double[] scale = new double[9] { 329.63, 349.23, 392.00, 440.00, 493.88, 523.25, 587.33, 659.25, 739.99 }; 


            //test song info
            string songA = "|6q6t6t6t7q7q|8q6q7h|6q6t6t6t7q7q|8q6q8h|";
            int bpm = 100;

            //foreach (char c in songA)
            //{
            //    if(c == '|') { }
            //        //do nothing
            //    else if (c > 0 && c < 10 )
            //    {
            //        egbdf[c];
            //    }
            //}

            for (int i = 0; i < songA.Length; i++)
            {                
                if (char.IsDigit( songA[i] ))
                {
                    int beat;
                    char time = (char)songA[i+1];
                    switch(time)
                    {
                        case 'w': //whole note, 4 beats
                            beat = bpm * 4;
                            break;
                        case 'h': //half note, 2 beats
                            beat = bpm * 2;
                            break;
                        case 'q': //quarter, 1 beat
                            beat = bpm;
                            break;
                        case 'e': //eighth  beat / 2
                            beat = bpm / 2;
                            break;
                        case 't': //tripplet beat / 3
                            beat = bpm / 3;
                            break;
                        case 's': //sixteenth beat / 4
                            beat = bpm / 4;
                            break;
                        default:
                            beat = bpm;
                            break;
                    }
                    int ivar = Convert.ToInt32((songA[i]).ToString());
                    //int hz = Convert.ToInt32(scale[songA[i]]);
                    int hz = Convert.ToInt32(scale[ivar]);
                    Console.Beep(hz, BPMtoMS(beat));                    
                }
                else if (songA[i] == '|')
                {
                    Console.WriteLine("Messure");
                }
            }

  

        }

        static void displayFile(string filePath)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                StreamReader reader = new StreamReader(filePath);
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                    Thread.Sleep(50);
                }
                //Console.WriteLine(reader.ReadToEnd());
                //reader.Close();
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            finally
            {
                Console.ResetColor();
                //Spinny(50, 100);
                Console.WriteLine("Press any key to Continue...");

                //await Spinny(50, 100);
                Console.ReadKey();
                Console.ResetColor();
            }
        }

        static void displayArray(string[,] arrayString)
        {
            for (int i = 0; i < arrayString.GetLength(1); i++)
            {
                for (int j = 0; j < arrayString.GetLength(0); j++)
                {
                    Console.Write(arrayString[j, i] + "\t");
                    //Thread.Sleep(50);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to Continue...");
            Console.ReadKey();
        }

        static string[,] inputScale(string filepath, string filepath2)
        {
            string[,] scaleArray = new string[2, 108];
            try
            {
                StreamReader readerNotes = new StreamReader(filepath);
                int counterA = 0;
                while (!readerNotes.EndOfStream)
                {
                    scaleArray[0, counterA] = readerNotes.ReadLine().Trim();
                    counterA++;
                }
                readerNotes.Close();
                Console.WriteLine($"{filepath} read.");

                StreamReader readerHtz = new StreamReader(filepath2);
                int counterB = 0;                
                while (!readerHtz.EndOfStream)
                {
                    scaleArray[1, counterB] = readerHtz.ReadLine().Trim();
                    counterB++;
                }
                readerHtz.Close();
                Console.WriteLine($"{filepath2} read.");

                //while (!reader.EndOfStream)
                //{
                //    if (reader.Peek() == ' ' || reader.Peek() == '\t')
                //    {
                //        counterA++;
                //    }
                //    else if (reader.Peek() == '\n')
                //    {
                //        counterB++;
                //    }
                //    else
                //    {
                //        scaleArray[counterA, counterB] = reader.Read();
                //    }
                //scaleArray[0, counterA] = reader.ReadLine();
                //Console.Write((char)reader.Read());
                // Thread.Sleep(50);
                //counterA++;
                //}
                //Console.WriteLine(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Beep();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                Console.ResetColor();
            }
            finally
            {
                Console.WriteLine("Press any key to Continue...");
                Console.ReadKey();                
            }
            return scaleArray;
        }

        static Task Spinny(int rate, int dur)//spinny icon, rate=time in mili a tick, dur=number of spins
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < dur; i++)
                {
                    Console.Write("|\r");
                    Thread.Sleep(rate);
                    Console.Write("/\r");
                    Thread.Sleep(rate);
                    Console.Write("-\r");
                    Thread.Sleep(rate);
                    Console.Write("\\\r");
                    Thread.Sleep(rate);
                }
            });

        }

        //converts BPM to MS or MS to BPM
        static int BPMtoMS(int bpm)
        {
            if(bpm == 0)
                return 0;
            else
                return (60000 / bpm);
        }        

        public class Song
        {
            int _bpm;
            string _file;
            int _beat;

        }


    }
}