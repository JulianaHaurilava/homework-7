using System;


namespace task_7._1
{
    struct Worker
    {
        public int id { get; set; } // ID

        public DateTime creationTime { get; set; } // Дата и время создания записи

        public string fullName { get; set; } // Ф.И.О.

        public DateTime dateOfBirth { get; set; } // Дата рождения сотрудника

        public int age { get; set; } // Возраст сотрудника

        public int height { get; set; } // Рост сотрудника

        public string nativeTown { get; set; } // Место рождения сотрудника

        /// <summary>
        /// Рассчитывает возраст сотрудника
        /// </summary>
        /// <param name="dateOfBirth"></param>
        /// <returns>Возраст сотрудника</returns>
        public static int GetAge(DateTime dateOfBirth)
        {
            return DateTime.Today.Year - dateOfBirth.Year -
                ((DateTime.Today.Month > dateOfBirth.Month ||
                  DateTime.Today.Month == dateOfBirth.Month && DateTime.Today.Day >= dateOfBirth.Day) ? 0 : 1);
        }

        /// <summary>
        /// Запрашивает информацию о новом сотруднике
        /// </summary>
        public void GetNewWorkerInfo()
        {
            Console.WriteLine("Введите информацию о сотруднике.\n");
            creationTime = DateTime.Now;
            Console.WriteLine("Введите ID");
            id = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите Ф.И.О.");
            fullName = Console.ReadLine();
            Console.WriteLine("Введите рост");
            height = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату рождения");
            dateOfBirth = DateTime.Parse(Console.ReadLine());
            age = GetAge(dateOfBirth);
            Console.WriteLine("Введите место рождения");
            nativeTown = Console.ReadLine();
        }

        /// <summary>
        /// Выводит информацию о конкретном сотруднике на экран
        /// </summary>
        public void PrintWorker()
        {
            Console.WriteLine($"ID сотрудника: {id}\nДата и время добавления записи: {creationTime}\n" +
                                 $"Ф.И.О. сотрудника: {fullName}\nВозраст сотрудника: {age}\n" +
                                 $"Рост сотрудника: {height} см\nДата рождения: {dateOfBirth:dd.MM.yyyy}\n" +
                                 $"Место рождения сотрудника: {nativeTown}\n");
        }

        /// <summary>
        /// Считывает информацию о сотруднике из файла и инициализирует поля структуры
        /// </summary>
        /// <param name="id"></param>
        /// <param name="creationTime"></param>
        /// <param name="fullName"></param>
        /// <param name="dateOfBirth"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="nativeTown"></param>
        public void AddWorkerFromFile(int id, DateTime creationTime, string fullName,
                      DateTime dateOfBirth, int age, int height, string nativeTown)
        {
            this.id = id;
            this.creationTime = creationTime;
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.age = age;
            this.height = height;
            this.nativeTown = nativeTown;
        }

        /// <summary>
        /// Конвертирует структуру в строку
        /// </summary>
        /// <param name="workerInfo"></param>
        /// <returns>Строка для записи в файл</returns>
        public string FromStructToString(string[] workerInfo)
        {
            workerInfo[0] = Convert.ToString(id);
            workerInfo[1] = Convert.ToString(creationTime);
            workerInfo[2] = fullName;
            workerInfo[3] = Convert.ToString(age);
            workerInfo[4] = Convert.ToString(height);
            workerInfo[5] = Convert.ToString($"{dateOfBirth:dd.MM.yyyy}");
            workerInfo[6] = "город " + $"{nativeTown}";

            return String.Join('#', workerInfo);
        }
    }
}
