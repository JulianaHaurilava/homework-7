using System;
using System.IO;

namespace task_7._1
{
    class Repository
    {
        private Worker[] allWorkers; // Массив сотрудников
        public Worker[] AllWorkers { get { return allWorkers; } private set { allWorkers = value; } }

        private string fileName; // Имя файла
        private int position; // Количество сотрудников в массиве
        private string[] workerInfo; // Массив для записи информации о сотруднике в файл


        public Repository(string fileName)
        {
            this.fileName = fileName;
            workerInfo = new string[7];
            

            if (File.Exists(fileName))
            {
                string[] allWorkersArray = File.ReadAllLines(fileName);
                int numberOfWorkers = allWorkersArray.Length;
                allWorkers = new Worker[numberOfWorkers + 1];
                GetAllWorkers(allWorkersArray);
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
        public Worker[] GetAllWorkers(string[] allWorkersArray)
        {
            position = 0;

            foreach (string worker in allWorkersArray)
            {
                workerInfo = worker.Split('#');
                allWorkers[position++].AddWorkerFromFile(ulong.Parse(workerInfo[0]),
                                                         DateTime.Parse(workerInfo[1]),
                                                         workerInfo[2],
                                                         DateTime.Parse(workerInfo[5]),
                                                         int.Parse(workerInfo[3]),
                                                         int.Parse(workerInfo[4]),
                                                         workerInfo[6].Substring(6));
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
        public Worker FindWorkerByID(ulong findID)
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
        public int FindPositionByID(ulong findID)
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
        /// Находит и возвращает массив сотрудников, записи о которых были созданы в указанный период
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns>Массив сотрудников</returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            Worker[] workersBetweenTwoDates = new Worker[position];
            int newPosition = 0;
            for (int i = 0; i < position; i++)
            {
                if (allWorkers[i].creationTime <= dateTo && allWorkers[i].creationTime >= dateFrom)
                {
                    workersBetweenTwoDates[newPosition++] = allWorkers[i];
                }
            }
            return workersBetweenTwoDates;
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
        /// <param name="newWorker"></param>
        public void AddWorker(Worker newWorker)
        {
            using (StreamWriter stream = new StreamWriter(fileName, true))
            {
                AddWorkerToArray(newWorker);
                stream.WriteLine(newWorker.FromStructToString(workerInfo));
            }
        }

        /// <summary>
        /// Удаляет выбранного сотрудника
        /// </summary>
        /// <param name="deleteID"></param>
        public void DeleteWorker(ulong deleteID)
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
                RewriteFileAndArray(newAllWorkers);
                
            }
            else Console.WriteLine("Работника с введенным ID не существует.");
        }

        /// <summary>
        /// Перезаписывает файл на основании информации из нового массива
        /// </summary>
        /// <param name="newArray"></param>
        public void RewriteFileAndArray(Worker[] newArray)
        {
            using (StreamWriter stream = new StreamWriter(fileName, false))
            {
                foreach (Worker worker in newArray)
                {
                    stream.WriteLine(worker.FromStructToString(workerInfo));
                }
            }
            allWorkers = newArray;
        }
    }
}
