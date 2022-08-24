using System;
using System.Linq;
using task_7._1;

namespace task_6._1
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
        static void SortWorkers(Repository r)
        {
            Worker[] workers = r.GetAllWorkers();
            if (workers[0].id != 0)
            {
                Console.WriteLine("По какому полю вы хотите отсортировать?" +
                                              "Выберите соответствующее число.\n" +
                                              "1 - ID\n2 - дата и время добавления записи\n" +
                                              "3 - Ф.И.О.\n4 - возраст\n5 - рост\n" +
                                              "Чтобы выйти из сортировщика нажмите любой другой символ.\n");
                switch (Console.ReadKey(true).KeyChar)
                {
                    case '1': r.AllWorkers = workers.OrderBy(worker => worker.id).ToArray(); break;
                    case '2': r.AllWorkers = workers.OrderBy(worker => worker.creationTime).ToArray(); break;
                    case '3': r.AllWorkers = workers.OrderBy(worker => worker.fullName).ToArray(); break;
                    case '4': r.AllWorkers = workers.OrderBy(worker => worker.age).ToArray(); break;
                    case '5': r.AllWorkers = workers.OrderBy(worker => worker.height).ToArray(); break;
                    default:
                        return;
                }
                r.RewriteFile(r.AllWorkers);
            }
            else Console.WriteLine("Записей о сотрудниках нет.");
        }

        static void Main(string[] args)
        {
            string fileName = @"Сотрудники.txt";
            Repository r = new Repository(fileName);

            while (true)
            {
                Console.WriteLine("Что вы хотите сделать?" +
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
                            int id = int.Parse(Console.ReadLine());
                            Worker worker = r.FindWorkerByID(id);
                            if (worker.id != 0)
                            {
                                worker.PrintWorker();
                            }
                            else Console.WriteLine("Сотрудника со введенным ID не существует.");
                            break;
                        }
                    case '3':
                        {
                            Console.Clear();
                            r.AddWorker();
                            break;
                        }
                    case '4':
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID сотрудника, которого хотите удалить.");
                            int id = int.Parse(Console.ReadLine());
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
                            r.GetWorkersBetweenTwoDates(dateFrom, dateTo);
                            break;
                        }
                    case '6':
                        {
                            Console.Clear();
                            SortWorkers(r);
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

