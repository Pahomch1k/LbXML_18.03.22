using System;
using static System.Console;
using System.Xml;
using System.IO;
using System.Text;


namespace Lb_XML
{
    class Program
    {
        static void Main(string[] args)
        {
            XML xml = new XML();

            int choise = 0;
            int flag = 0;

            while (flag == 0)
            {
                WriteLine("1. Запись в файл\n2. Чтение из файла\n3. Выход");
                choise = Convert.ToInt32(ReadLine());

                switch (choise)
                {
                    case 1: xml.WriteXml();
                        break;
                    case 2: xml.ReadXml(); break;
                    case 3: flag++;
                        break;
                    default: WriteLine("try again");
                        break;
                }
            } 
        }
    }

    class XML
    {
        string x { get; set; }
        public void WriteXml()
        { 
            XmlTextWriter xmlwriter = new XmlTextWriter("../../Clubs.xml", Encoding.UTF8);
            xmlwriter.WriteStartDocument(); 
            // Formatting определяет способ форматирования выходных данных
            xmlwriter.Formatting = Formatting.Indented; //Форматирует отступы в дочерних элементах в соответствии с параметрами настройки IndentChar и Indentation
            xmlwriter.IndentChar = '\t'; // Возвращает или задает знак для отступа
            xmlwriter.Indentation = 1; // Возвращает или задает количество записываемых IndentChars для каждого уровня в иерархии


            xmlwriter.WriteStartElement("football"); // Записывает указанный открывающий тег
            xmlwriter.WriteComment("Футбольный клуб"); // Создает комментарий
            Write("club? ");
            x = ReadLine();
            xmlwriter.WriteStartElement(x);

            xmlwriter.WriteStartAttribute("city", null); // Записывает атрибут с заданным именем 
            Write("city? ");
            x = ReadLine();  
            xmlwriter.WriteString(x); // Записывает заданное текстовое содержимое атрибута
            xmlwriter.WriteEndAttribute(); // Закрывает атрибут
             
            xmlwriter.WriteStartAttribute("country", null);
            Write("country? ");
            x = ReadLine();
            xmlwriter.WriteString(x);
            xmlwriter.WriteEndAttribute();

            Write("name? ");
            x = ReadLine();
            xmlwriter.WriteString(x); // Записывает заданное текстовое содержимое элемента

            xmlwriter.WriteEndElement(); // Закрывает один элемент
            xmlwriter.WriteEndElement();

            WriteLine("Данные сохранены в XML-файл");
            xmlwriter.Close();
        }

        public void ReadXml()
        { 
            XmlTextReader reader = new XmlTextReader("../../Clubs.xml");
            string str = null;
            while (reader.Read()) // Считывает следующий узел из потока
            {
                if (reader.NodeType == XmlNodeType.Text)
                    str += reader.Value + "\n";

                // NodeType возвращает тип текущего узла
                if (reader.NodeType == XmlNodeType.Element)
                    if (reader.HasAttributes)// имеются ли атрибуты?
                        while (reader.MoveToNextAttribute()) // Перемещается к следующему атрибуту
                            str += reader.Value + "\n";  
            }
            WriteLine(str);
            reader.Close();
        }
    } 
} 
