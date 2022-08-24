using System;
using System.IO;

namespace task_7._1
{
    class Repository
    {
        private Worker[] allWorkers; // Массив сотрудников
        public Worker[] AllWorkers { get { return allWorkers; } set { allWorkers = value; } }

        private string fileName; // Имя файла
        private int position; // Количество сотрудников в массиве
        private string[] workerInfo; // Массив для записи информации о сотруднике в файл


        public Repository(string fileName)
        {
            this.fileName = fileName;
            workerInfo = new string[7];

            if (File.Exists(fileName) && File.ReadAllLines(fileName).Length != 0)
            {
                allWorkers = new Worker[File.ReadAllLines(fileName).Length];
                allWorkers = GetAllWorkers();
            }
            else
            {
                position = 0;
                allWorkers = new Worker[1];
            }
        }

        /// <summary>
        /// Преобразовывает данные из файла в массив структур
        /// </summary>
        /// <returns>Массив структур с информацией о сотрудниках</returns>
        public Worker[] GetAllWorkers()
        {
            position = 0;
            if (File.Exists(fileName))
            {
                using StreamReader stream = new StreamReader(fileName);
                while (!stream.EndOfStream)
                {
                    workerInfo = stream.ReadLine().Split('#');
                    allWorkers[position++].AddWorkerFromFile(int.Parse(workerInfo[0]),
                                                             DateTime.Parse(workerInfo[1]),
                                                             workerInfo[2],
                                                             DateTime.Parse(workerInfo[5]),
                                                             int.Parse(workerInfo[3]),
                                                             int.Parse(workerInfo[4]), 
                                                             workerInfo[6]);
                }
            }
            return allWorkers;
        }

        /// <summary>
        /// Выводит информацию о сотрудниках
        /// </summary>
        public void PrintAllWorkers()
        {
            for (int i = 0; i < position; i++)
            {
                allWorkers[i].PrintWorker();
            }
            if (position == 0) Console.WriteLine("Записей о сотрудниках нет.");

        }

        /// <summary>
        /// Находит сотрудника с указанным ID
        /// </summary>
        /// <param name="findID"></param>
        /// <returns>Искомого сотрудника</returns>
        public Worker FindWorkerByID(int findID)
        {
            for (int i = 0; i < position; i++)
            {
                if (allWorkers[i].id == findID)
                {
                    return allWorkers[i];
                }
            }

            Worker errorWorker = new Worker();
            return errorWorker;
        }

        /// <summary>
        /// Находит сотрудника с указанным ID
        /// </summary>
        /// <param name="findID"></param>
        /// <returns>Искомое ID</returns>
        public int FindPositionByID(int findID)
        {
            for (int i = 0; i < position; i++)
            {
                if (allWorkers[i].id == findID)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Находит и показывает записи о сотрудниках, которые были созданы в указанный период
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        public void GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            bool noteExists = false;
            for (int i = 0; i < position; i++)
            {
                if (allWorkers[i].creationTime <= dateTo && allWorkers[i].creationTime >= dateFrom)
                {
                    noteExists = true;
                    allWorkers[i].PrintWorker();
                }
            }

            if (!noteExists)
            {
                Console.WriteLine($"Нет записей в период с {dateFrom} по {dateTo}."); ;
            }
        }

        /// <summary>
        /// Увеличивает размер массива allWorkers в два раза + 1, если для записи новой информации нет места
        /// </summary>
        /// <param name="indexOutOfRange"></param>
        private void Resize(bool indexOutOfRange)
        {
            if (indexOutOfRange)
            {
                Array.Resize(ref allWorkers, allWorkers.Length * 2 + 1);
            }
        }

        /// <summary>
        /// Добавляет информацию о сотруднике в массив allWorkers
        /// </summary>
        /// <param name="newWorker"></param>
        private void AddWorkerToArray(Worker newWorker)
        {
            Resize(position >= allWorkers.Length);
            allWorkers[position++] = newWorker;
        }

        /// <summary>
        /// Записывает информацию о сотруднике в файл
        /// </summary>
        public void AddWorker()
        {
            using (StreamWriter stream = new StreamWriter(fileName, true))
            {
                Worker newWorker = new Worker();
                newWorker.GetNewWorkerInfo();
                AddWorkerToArray(newWorker);
                stream.WriteLine(newWorker.FromStructToString(workerInfo));
            }
        }

        /// <summary>
        /// Удаляет выбранного сотрудника
        /// </summary>
        /// <param name="deleteID"></param>
        public void DeleteWorker(int deleteID)
        {
            int indexToDelete = FindPositionByID(deleteID);
            if (indexToDelete != -1)
            {
                Worker[] newAllWorkers = new Worker[position - 1];

                for (int i = 0; i < indexToDelete; i++)
                {
                    newAllWorkers[i] = allWorkers[i];
                }
                for (int i = indexToDelete + 1; i < position; i++)
                {
                    newAllWorkers[i - 1] = allWorkers[i];
                }
                position--;
                RewriteFile(newAllWorkers);
                allWorkers = newAllWorkers;
            }
            else Console.WriteLine("Работника с введенным ID не существует.");
        }

        /// <summary>
        /// Перезаписывает файл на основании информации из нового массива
        /// </summary>
        /// <param name="newArray"></param>
        public void RewriteFile(Worker[] newArray)
        {
            using (StreamWriter stream = new StreamWriter(fileName, false))
            {
                foreach (Worker worker in newArray)
                {
                    stream.WriteLine(worker.FromStructToString(workerInfo));
                }
            }
        }
    }
}
