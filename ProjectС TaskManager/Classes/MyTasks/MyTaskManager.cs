using ProjectB_TaskManager.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjectB_TaskManager.Classes.MyTasks
{
    public class MyTaskManager : ICollection, IEnumerable<MyTask>, IDuplicateCheckable
    {
        private List<MyTask> tasks;

        public MyTaskManager()
        {
            tasks = new List<MyTask>();
        }
        public MyTaskManager(List<MyTask> tasks)
        {
            this.tasks = tasks;
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

        public MyTask this[int index]
        {
            get { return tasks[index]; }
            set { tasks[index] = value; }
        }

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

            if (IsDuplicate(item))
            {
                throw new DuplicateNameException("Task whith this course name and description already exists.");
            }

            tasks.Add(item);
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
    }
}