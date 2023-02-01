using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            FileProg fileProg = new FileProg();
            
            fileProg.MenuProg();
           


        }
    }

    class FileProg
    {
        public string path;
        public int lineNumber = 1;
        string[] buffer;

        public void OpenFileProg(string name)
        {
            path = name;
        }


        public void CreateFile(string name, string type)
        {
            path = name;
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(type);
                    
                }
                       
            }
            
        }

        public void AddWord(string key, List<string> value)
        {
            
            int ind = 1;
            if (SearchForWord(key) == -1)
            {

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine();
                    sw.WriteLine(key + " :");
                    foreach (var e in value)
                    {
                        if(ind != value.Count)
                        {
                            sw.Write(e + ',');
                            ind++;
                        }
                        else
                        {
                            sw.Write(e);
                        }
                    }

                }
            }
            else
            {
                
                using (StreamReader sw = new StreamReader(path, true))
                {

                    string[] filetext = File.ReadAllLines(path);
                    
                        foreach (var e in value)
                            {
                                string[] cont = filetext[lineNumber+1].Split(',');
                                int povtor = 0;
                                foreach (var c in cont)
                                    {
                                        if (c == e) povtor++;
                                    }
                                if (povtor == 0) filetext[lineNumber + 1] = filetext[lineNumber + 1] + e + ",";


                        
                            }

                    buffer = filetext;
                 }
                DeleteTextFile();
            }


        }

        int SearchForWord(string searchTerm)
        {
            lineNumber = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(searchTerm))
                    {
                        return lineNumber;
                    }
                    lineNumber++;
                }
            }
            return -1;
        }

        void ReadWord(string key)
        {
            using (StreamReader sw = new StreamReader(path, true))
            {

                string[] filetext = File.ReadAllLines(path);
                Console.WriteLine("Ищем перевод слова:" + filetext[SearchForWord(key)]);
                Console.WriteLine("Найдены следующие переводы: " + filetext[++lineNumber]);
                    
                             

            }
        }

        void DeleteTextFile()
        {

            
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                foreach (var e in buffer)
                {
                    sw.WriteLine(e);
                }
            }
        }

        public void DeleteWordFile(string key, string slovo)

        {
            
            string[] filetext = File.ReadAllLines(path);
            buffer = filetext;
            if (key==slovo)
            {
                buffer[SearchForWord(key) + 1] = null;
                buffer[SearchForWord(key)] = null;
                DeleteTextFile();
                return;
            }
            string[] cont = filetext[SearchForWord(key)+1].Split(',');
            if (cont.Count()<=1)
            {
                Console.WriteLine("Невозможно удалить последнее слово в переводе");
                return;
            }
            buffer[SearchForWord(key)+1] = null;
            foreach (var e in cont)

            {
                if (slovo != e) buffer[lineNumber+1] = buffer[lineNumber+1] + e + ",";
                
            }
            
            DeleteTextFile();

        }

        public void RenameWord(string key, string slovo, string slovo2)
        {
            string[] filetext = File.ReadAllLines(path);
            buffer = filetext;
            string[] cont = filetext[SearchForWord(key) + 1].Split(',');
            buffer[SearchForWord(key) + 1] = null;
            foreach (var e in cont)

            {
                if (slovo != e) 
                    buffer[lineNumber + 1] = buffer[lineNumber + 1] + e + ",";
                else
                {
                    buffer[lineNumber + 1] = buffer[lineNumber + 1] + slovo2 + ",";
                }

            }

            DeleteTextFile();
        }

        public void MenuProg()
        {
            Console.WriteLine("1. Создать словарь");
            Console.WriteLine("2. Добавить слово");
            Console.WriteLine("3. Удалить слово");
            Console.WriteLine("4. Изменить слово");
            Console.WriteLine("5. Найти слово");
            Console.WriteLine("6. Открыть словарь");

            string x, y, f;
            string kl = Console.ReadLine();
            switch (Convert.ToInt32(kl))
            {
                case 1:
                    Console.WriteLine("Введите название файла: Name.txt");
                    x = Console.ReadLine();
                    Console.WriteLine("Введите тип словаря");
                    y = Console.ReadLine();
                    CreateFile(x, y);
                    break;
                case 2:
                    Console.WriteLine("Введите переводимое слово");
                    x = Console.ReadLine();
                    Console.WriteLine("Введите слово или слова-переводы через запятую БЕЗ ПРОБЕЛОВ");
                    y = Console.ReadLine();
                    List<string> y1 = new List<string>(y.Split(','));
                    AddWord(x, y1);
                    break;
                case 3:
                    Console.WriteLine("Введите слово, перевод которого надо удалить");
                    x = Console.ReadLine();
                    Console.WriteLine("Введите слово, которое надо удалить");
                    y = Console.ReadLine();
                    DeleteWordFile(x, y);
                    break;
                case 4:
                    Console.WriteLine("Введите слово, перевод которого надо изменить");
                    x = Console.ReadLine();
                    Console.WriteLine("Введите слово, которое надо изменить");
                    y = Console.ReadLine();
                    Console.WriteLine("Введите слово, на которое надо изменить");
                    f = Console.ReadLine();
                    RenameWord(x, y, f);
                    break;
                case 5:
                    Console.WriteLine("Введите слово, перевод которого необходимо найти");
                    x = Console.ReadLine();
                    ReadWord(x);
                    break;
                case 6:
                    Console.WriteLine("Введите название файла: Name.txt");
                    x = Console.ReadLine();
                    OpenFileProg(x);
                    break;
                 
            }

            MenuProg();
        }



    }
          
        

    
        
}
