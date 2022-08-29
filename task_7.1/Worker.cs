using System;


namespace task_7._1
{
    struct Worker
    {
        public ulong ID { get; private set; } // ID

        public DateTime CreationTime { get; private set; } // Дата и время создания записи

        public string FullName { get; set; } // Ф.И.О.

        public DateTime DateOfBirth { get; set; } // Дата рождения сотрудника

        public int Age { get; set; } // Возраст сотрудника

        public int Height { get; set; } // Рост сотрудника

        public string NativeTown { get; set; } // Место рождения сотрудника

        public Worker(string FullName, DateTime DateOfBirth, 
                      int Age, int Height, string NativeTown)
        {
            CreationTime = DateTime.Now;
            string stringID = (CreationTime.Year % 100).ToString() + CreationTime.Month.ToString("00") +
                              CreationTime.Day.ToString("00") + CreationTime.Hour.ToString("00") +
                              CreationTime.Minute.ToString("00") + CreationTime.Second.ToString("00");
            ID = ulong.Parse(stringID);

            CreationTime = DateTime.Now;
            this.FullName = FullName;
            this.DateOfBirth = DateOfBirth;
            this.Age = Age;
            this.Height = Height;
            this.NativeTown = NativeTown;
        }


        /// <summary>
        /// Выводит информацию о конкретном сотруднике на экран
        /// </summary>
        public void PrintWorker()
        {
            Console.WriteLine($"ID сотрудника: {ID}\nДата и время добавления записи: {CreationTime}\n" +
                                 $"Ф.И.О: {FullName}\nВозраст: {Age}\n" +
                                 $"Рост: {Height} см\nДата рождения: {DateOfBirth:dd.MM.yyyy}\n" +
                                 $"Место рождения: {NativeTown}\n");
        }

        /// <summary>
        /// Считывает информацию о сотруднике из файла и инициализирует поля структуры
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CreationTime"></param>
        /// <param name="FullName"></param>
        /// <param name="DateOfBirth"></param>
        /// <param name="Age"></param>
        /// <param name="Height"></param>
        /// <param name="NativeTown"></param>
        public void AddWorkerFromFile(ulong ID, DateTime CreationTime, string FullName,
                      DateTime DateOfBirth, int Age, int Height, string NativeTown)
        {
            this.ID = ID;
            this.CreationTime = CreationTime;
            this.FullName = FullName;
            this.DateOfBirth = DateOfBirth;
            this.Age = Age;
            this.Height = Height;
            this.NativeTown = NativeTown;
        }

        /// <summary>
        /// Конвертирует структуру в строку
        /// </summary>
        /// <param name="workerInfo"></param>
        /// <returns>Строка для записи в файл</returns>
        public string FromStructToString(string[] workerInfo)
        {
            workerInfo[0] = Convert.ToString(ID);
            workerInfo[1] = Convert.ToString(CreationTime);
            workerInfo[2] = FullName;
            workerInfo[3] = Convert.ToString(Age);
            workerInfo[4] = Convert.ToString(Height);
            workerInfo[5] = Convert.ToString($"{DateOfBirth:dd.MM.yyyy}");
            workerInfo[6] = "город " + $"{NativeTown}";

            return String.Join('#', workerInfo);
        }
    }
}
