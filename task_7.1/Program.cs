using System;
using System.IO;
using System.Linq;

namespace task_7._1
{
    class Program
    {
        /// <summary>
    /// Проверяет корректность введенной даты
    /// </summary>
    /// <param name="employeeInfo"></param>
    /// <returns>Корректная дата</returns>
        static DateTime CheckAndGetDate()
        {
            DateTime date;

            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Ошибка! Введите корректную дату.");
            }
            return date;
        }

        /// <summary>
        /// Сортирует сотрудников по выбранному полю
        /// </summary>
        /// <param name="r"></param>
        /// <param name="fileName"></param>
        static void SortWorkers(Repository r, string fileName)
        {
            if (File.Exists(fileName))
            {
                string[] allWorkersArray = File.ReadAllLines(fileName);
                Worker[] workers = r.GetAllWorkers(allWorkersArray);
                if (workers.Length != 0)
                {

                    Console.WriteLine("По какому полю вы хотите отсортировать?" +
                                                  "Выберите соответствующее число.\n" +
                                                  "1 - ID\n2 - дата и время добавления записи\n" +
                                                  "3 - Ф.И.О.\n4 - возраст\n5 - рост\n" +
                                                  "Чтобы выйти из сортировщика нажмите любой другой символ.\n");
                    switch (Console.ReadKey(true).KeyChar)
                    {
                        case '1': workers = workers.OrderBy(worker => worker.ID).ToArray(); break;
                        case '2': workers = workers.OrderBy(worker => worker.CreationTime).ToArray(); break;
                        case '3': workers = workers.OrderBy(worker => worker.FullName).ToArray(); break;
                        case '4': workers = workers.OrderBy(worker => worker.Age).ToArray(); break;
                        case '5': workers = workers.OrderBy(worker => worker.Height).ToArray(); break;
                        default:
                            return;
                    }

                    r.RewriteFileAndArray(workers);
                    return;

                }
            }
            Console.WriteLine("Записей о сотрудниках нет.");
        }

        /// <summary>
        /// Рассчитывает возраст сотрудника
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Возраст сотрудника</returns>
        private static int GetAge(DateTime dateOfBirth)
        {
            return DateTime.Today.Year - dateOfBirth.Year -
                ((DateTime.Today.Month > dateOfBirth.Month ||
                  DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day >= dateOfBirth.Day) ? 0 : 1);
        }

        /// <summary>
        /// Создает нового сотрудника на основании введенных в консоль данных
        /// </summary>
        /// <returns>Нового сотрудника</returns>
        static private Worker CreateWorkerFromConsole()
        {
            Console.WriteLine("Введите информацию о сотруднике.\n");

            Console.WriteLine("Введите Ф.И.О.");
            string fullName = Console.ReadLine();
            Console.WriteLine("Введите рост");
            int height = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату рождения");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());
            int age = GetAge(dateOfBirth);
            Console.WriteLine("Введите место рождения");
            string nativeTown = Console.ReadLine();
            Worker newWorker = new Worker(fullName, dateOfBirth, age, height, nativeTown);
            return newWorker;
        }

        static void Main(string[] args)
        {
            string fileName = @"Сотрудники.txt";
            Repository r = new Repository(fileName);

            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?\n" +
                                  "Выберите соответствующее число.\n" +
                                  "1 - вывести данные на экран;\n" +
                                  "2 - найти сотрудника по ID\n" +
                                  "3 - добавить новую запись о сотруднике\n" +
                                  "4 - удалить запись о сотруднике\n" +
                                  "5 - загрузить записи в выбранном диапазоне дат\n" +
                                  "6 - отсортировать записи по выбранному полю\n" +
                                  "Для того, чтобы завершить работу, введите любой другой символ.");

                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1':
                        {
                            Console.Clear();
                            r.PrintAllWorkers();
                            break;
                        }
                    case '2':
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID сотрудника, которого хотите найти.");
                            ulong id = ulong.Parse(Console.ReadLine());
                            Worker worker = r.FindWorkerByID(id);
                            if (worker.ID != 0)
                            {
                                worker.PrintWorker();
                            }
                            else Console.WriteLine("Сотрудника со введенным ID не существует.");
                            break;
                        }
                    case '3':
                        {
                            Console.Clear();
                            Worker newWorker = CreateWorkerFromConsole();
                            r.AddWorker(newWorker);
                            break;
                        }
                    case '4':
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID сотрудника, которого хотите удалить.");
                            ulong id = ulong.Parse(Console.ReadLine());
                            r.DeleteWorker(id);
                            break;
                        }
                    case '5':
                        {
                            Console.Clear();
                            DateTime dateTo, dateFrom;
                            Console.Write("Введите период, по которому вы хотите найти записи.\nC ");
                            dateFrom = CheckAndGetDate();
                            Console.Write("По ");
                            dateTo = CheckAndGetDate();
                            Console.WriteLine();
                            Worker[] workers = r.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                            if (workers.Length != 0 && workers[0].ID != 0)
                            {
                                for (int i = 0; i < workers.Length; i++)
                                {
                                    workers[i].PrintWorker();
                                }
                            }
                            else Console.WriteLine($"Нет записей в период с {dateFrom.ToShortDateString()} по {dateTo.ToShortDateString()}.");
                            break;
                        }
                    case '6':
                        {
                            Console.Clear();
                            SortWorkers(r, fileName);
                            break;
                        }
                    default: return;
                }

                Console.WriteLine("\nДля того, чтобы выйти в главное меню, нажмите любую клавишу...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}

