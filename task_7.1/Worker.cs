using System;


namespace task_7._1
{
    struct Worker
    {
        public ulong id { get; private set; } // ID

        public DateTime creationTime { get; private set; } // Дата и время создания записи

        public string fullName { get; set; } // Ф.И.О.

        public DateTime dateOfBirth { get; set; } // Дата рождения сотрудника

        public int age { get; set; } // Возраст сотрудника

        public int height { get; set; } // Рост сотрудника

        public string nativeTown { get; set; } // Место рождения сотрудника

        public Worker(string fullName, DateTime dateOfBirth, 
                      int age, int height, string nativeTown)
        {
            creationTime = DateTime.Now;
            string stringID = (creationTime.Year % 100).ToString() + creationTime.Month.ToString("00") +
                              creationTime.Day.ToString("00") + creationTime.Hour.ToString("00") +
                              creationTime.Minute.ToString("00") + creationTime.Second.ToString("00");
            id = ulong.Parse(stringID);

            creationTime = DateTime.Now;
            this.fullName = fullName;
            this.dateOfBirth = dateOfBirth;
            this.age = age;
            this.height = height;
            this.nativeTown = nativeTown;
        }


        /// <summary>
        /// Выводит информацию о конкретном сотруднике на экран
        /// </summary>
        public void PrintWorker()
        {
            Console.WriteLine($"ID сотрудника: {id}\nДата и время добавления записи: {creationTime}\n" +
                                 $"Ф.И.О: {fullName}\nВозраст: {age}\n" +
                                 $"Рост: {height} см\nДата рождения: {dateOfBirth:dd.MM.yyyy}\n" +
                                 $"Место рождения: {nativeTown}\n");
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
        public void AddWorkerFromFile(ulong id, DateTime creationTime, string fullName,
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
