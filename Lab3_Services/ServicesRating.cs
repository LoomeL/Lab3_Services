using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab3_Services
{
    public class ServicesRating
    {
        private TablePrinter TablePrinter = new(new[] {5, 25, 7, 20});
        List<Teacher> teachers = new();
        Dictionary<string, ConsoleColor> services = new();

        private delegate bool IsCanAddTeacher(string name);

        private IsCanAddTeacher isCanAddTeacher;

        public ServicesRating()
        {
            teachers.Add(new Teacher() {Fio = "Власова Е.Т.", Institute = "ИКИТ", UsingService = "Discord"});
            teachers.Add(new Teacher() {Fio = "Потапова А.И.", Institute = "ИППС", UsingService = "Discord"});
            teachers.Add(new Teacher() {Fio = "Калинина Д.Д.", Institute = "ГИ", UsingService = "Zoom"});
            teachers.Add(new Teacher() {Fio = "Чернова И.Н.", Institute = "ВИИ", UsingService = "WebinarSFU"});

            services.Add("Discord", ConsoleColor.Green);
            services.Add("Zoom", ConsoleColor.Blue);
            services.Add("WebinarSFU", ConsoleColor.Yellow);

            isCanAddTeacher = (name) =>
            {
                foreach (Teacher teacher in teachers)
                {
                    if (teacher.Fio == name) return false;
                }

                return true;
            };
        }

        public void Start()
        {
            bool stop = false;
            while(!stop)
            {
                PrintTeachers();
                PrintTop();

                ConsoleColor bc = Console.BackgroundColor;
                ConsoleColor fc = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Нажмите А что-бы добавить преподователя");
                Console.WriteLine("Нажмите Enter что-бы завершить работу программы");

                while (true)
                {
                    ConsoleKey ck = Console.ReadKey().Key;
                    if (ck == ConsoleKey.A)
                    {
                        AddTeacher();
                        break;
                    }
                    if (ck == ConsoleKey.Enter)
                    {
                        stop = true;
                        break;
                    }
                }
                
                Console.BackgroundColor = bc;
                Console.ForegroundColor = fc;
            }
        }

        public void PrintTeachers()
        {
            TablePrinter.PrintHeader("Преподаватели");
            for (var i = 0; i < teachers.Count; i++)
            {
                TablePrinter.PrintRow(new[]
                {
                    (i + 1).ToString(),
                    teachers[i].Fio,
                    teachers[i].Institute,
                    teachers[i].UsingService
                }, services[teachers[i].UsingService]);
            }

            TablePrinter.PrintFooter();
        }

        public void PrintTop()
        {
            Console.WriteLine();
            TablePrinter.PrintHeader("Топ 3");
            var x = teachers.GroupBy(teacher => teacher.UsingService,
                (s, enumerable) => new
                {
                    Key = s,
                    Count = enumerable.Count()
                }).OrderBy(arg => arg.Count).Reverse().Take(3).ToList();
            for (var i = 0; i < x.Count; i++)
            {
                Console.Write($"{i + 1} - ");
                Console.ForegroundColor = services[x[i].Key];
                Console.Write($"[{x[i].Key}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Количество использований: {x[i].Count}");
            }
            TablePrinter.PrintFooter();
        }

        public void AddTeacher()
        {
            Console.WriteLine("\nВведите ФИО:");
            string fio;
            while (true)
            {
                fio = Console.ReadLine();
                if (Regex.IsMatch(fio, "[А-Я][а-я]{1,20}\\s[А-Я][а-я]{1,20}\\s[А-Я][а-я]{1,20}"))
                {
                    // Фамилия Имя Отчество
                    string[] fioSplit = fio.Split(" ");
                    fio = fioSplit[0] + " " + fioSplit[1].Substring(0, 1) + "." + fioSplit[2].Substring(0, 1) + ".";
                    break;
                }

                // Фамилия И.О.
                if (!Regex.IsMatch(fio, "[А-Я][а-я]{1,20}\\s[А-Я]\\.[А-Я]\\.")) 
                    Console.WriteLine(
                    "Введите ФИО или Фамилию инициалы, согласно стандарту: \"Иванов Иван Иванович\" \"Иванов И.И.\"");

                if (!isCanAddTeacher(fio))
                {
                    Console.WriteLine("Преподаватель с данным ФИО уже есть в списке, введите другое ФИО");
                }
                else
                    break;
            }

            Console.WriteLine("Введити название института (сокращенно)");
            string inst = Console.ReadLine();

            Console.WriteLine("Выберете сервис которым пользуется преподаватель:");
            for (var i = 0; i < services.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {services.ElementAt(i).Key}");
            }

            string service = services.ElementAt(Program.ReadLineOfRange(1, services.Count) - 1).Key;

            teachers.Add(new Teacher() {Fio = fio, Institute = inst, UsingService = service});
        }
    }

    public class Teacher
    {
        public string Fio { get; set; }

        public string Institute { get; set; }

        public string UsingService { get; set; }
    }
}