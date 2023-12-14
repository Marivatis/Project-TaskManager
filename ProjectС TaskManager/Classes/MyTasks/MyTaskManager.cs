using ProjectС_TaskManager.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjectС_TaskManager.Classes.MyTasks
{
    public class MyTaskManager : ICollection, IEnumerable<MyTask>, IDuplicateCheckable
    {
        private Action<MyTask> checkIsDuplicate;

        private List<MyTask> tasks;

        /// <summary>
        /// Initializes a new instance of the <see cref="MyTaskManager"/> class.
        /// </summary>
        public MyTaskManager() : this(new List<MyTask>()) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MyTaskManager"/> class 
        /// and copy all <see cref="MyTask"/> tasks from <paramref name="tasks"/>.
        /// </summary>
        /// <param name="tasks"></param>
        public MyTaskManager(List<MyTask> tasks)
        {
            this.tasks = tasks;

            checkIsDuplicate = (item) =>
            {
                if (IsDuplicate(item)) throw new DuplicateNameException("Task with this course name and description already exists.");
            };

            SubscribeTasksToOverdueHandler(tasks);
        }        

        /// <summary>
        /// Gets or sets task <see cref="MyTask"/> at the index <paramref name="index"/>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MyTask this[int index]
        {
            get { return tasks[index]; }
            set { tasks[index] = value; }
        }

        /// <summary>
        /// Gets the number of elements contained in the task manager list.
        /// </summary>
        public int Count => tasks.Count;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the task manager list.
        /// </summary>
        public object SyncRoot => ((ICollection)tasks).SyncRoot;
        /// <summary>
        /// Gets a value indicating whether access to the task manager list is synchronized.
        /// </summary>
        public bool IsSynchronized => ((ICollection)tasks).IsSynchronized;

        /// <summary>
        /// Adds task to task manager list.
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="DuplicateNameException"></exception>
        public void Add(MyTask item)
        {
            if (item is null)
            {
                throw new ArgumentNullException("The task has an null object reference.");
            }

            checkIsDuplicate.Invoke(item);

            tasks.Add(item);

            item.TaskOverdue += TaskOverdueHandler;
        }
        /// <summary>
        /// Removes all tasks in task manager list.
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }
        /// <summary>
        /// Сhecks whether such a task exists in task manager list.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Returns <see langword="true"/> if this task exists, otherwise <see langword="false"/>.
        /// </returns>
        public bool Contains(MyTask item)
        {
            return tasks.Contains(item);
        }
        /// <summary>
        /// Removes the first occurrence of a transmitted task in task manager list.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Returns <see langword="true"/> if transmitted task has been removed, otherwise <see langword="false"/>.
        /// </returns>
        public bool Remove(MyTask item)
        {
            return tasks.Remove(item);
        }
        /// <summary>
        /// Removes the element at the specified index of the task manager list.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            tasks.RemoveAt(index);
        }
        /// <summary>
        /// Copies all tasks to List<MyTask> list and returns it.
        /// </summary>
        public List<MyTask> ToList()
        {
            return new List<MyTask>(tasks);
        }
        /// <summary>
        /// Filters task manager list and returns <see cref="MyTaskManager"/> collection containing filtred tasks
        /// </summary>
        /// <param name="condition"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>
        /// Returns <see cref="MyTaskManager"/> collection containing filtred tasks by condition <paramref name="condition"/>
        /// </returns>
        public MyTaskManager Filter(Func<MyTask, bool> condition)
        {
            return new MyTaskManager(tasks.Where(condition).ToList());
        }

        /// <summary>
        /// Sorts tasks in task manager list in order by remaining date
        /// </summary>
        public void SortTasksByRemainingDate()
        {
            tasks = tasks.OrderBy(t => t.RemainingTime).ToList();
        }
        /// <summary>
        /// Marks task with id <paramref name="id"/> as completed
        /// </summary>
        /// <param name="id"></param>
        public void MarkAsCompleted(int id)
        {
            foreach (MyTask task in tasks)
            {
                if (task.Id == id)
                {
                    task.MarkAsCompleted();
                    return;
                }
            }
        }

        /// <summary>
        /// Copies all tasks to MyTask[] array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        public void CopyTo(Array array, int index)
        {
            tasks.CopyTo((MyTask[])array, index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<MyTask> GetEnumerator()
        {
            return ((IEnumerable<MyTask>)tasks).GetEnumerator();
        }
        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)tasks).GetEnumerator();
        }

        /// <summary>
        /// Сhecks whether such a task with same hash code exists in task manager list.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// Returns <see langword="true"/> if this task exists, otherwise <see langword="false"/>.
        /// </returns>
        public bool IsDuplicate(MyTask item)
        {
            foreach (MyTask task in tasks)
            {
                if (item.GetHashCode() == task.GetHashCode())
                    return true;
            }

            return false;
        }

        private void SubscribeTasksToOverdueHandler(List<MyTask> tasks)
        {
            foreach (MyTask task in tasks)
            {
                task.TaskOverdue += TaskOverdueHandler;
            }
        }

        private void TaskOverdueHandler(object sender, int e)
        {
            Console.WriteLine($"Task with id {e} is overdue!");
        }
    }
}