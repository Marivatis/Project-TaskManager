using ProjectB_TaskManager.Classes.MyTasks;
using ProjectB_TaskManager.Enums;
using System.Data;

namespace TestProject.Tests
{
    [TestClass]
    public class MyTaskManagerTest
    {
        private const string category_iCollection = "ICollection";
        private const string category_basic = "Basic";

        [TestMethod]
        [TestCategory(category_iCollection)]
        public void Count_ValidReturn() 
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager()
            {
                new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(3, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(4, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted)
            };

            // Act
            int result = tasks.Count;

            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        [TestCategory(category_iCollection)]
        public void Count_ZeroReturn()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            // Act
            int result = tasks.Count;

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Add_ValidInput()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            MyTask task = new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);

            // Act
            tasks.Add(task);

            // Assert
            CollectionAssert.Contains(tasks, task);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Add_NullInput()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => tasks.Add(null));
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Add_DuplicateInput()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            // Act
            tasks.Add(new MyGeneralTask());

            // Act & Assert
            Assert.ThrowsException<DuplicateNameException>(()  => tasks.Add(new MyGeneralTask()));
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Clear_Clears()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager()
            {
                new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(3, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(4, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted)
            };

            // Act
            tasks.Clear();

            // Assert
            Assert.AreEqual(0, tasks.Count);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Contains_ExistTaskInput()
        {
            // Arrange
            MyGeneralTask task1 = new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);
            MyGeneralTask task2 = new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);

            MyTaskManager tasks = new MyTaskManager() { task1, task2 };

            // Act
            bool result = tasks.Contains(task1);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.Contains(tasks, task1);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Contains_NotExistentTaskInput()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager()
            {
                new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(3, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(4, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted)
            };

            // Act
            bool result = tasks.Contains(new MyGeneralTask());

            // Assert
            Assert.IsFalse(result);
            CollectionAssert.DoesNotContain(tasks, new MyGeneralTask());
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Remove_ExistTaskInput()
        {
            // Arrange
            MyGeneralTask task1 = new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);
            MyGeneralTask task2 = new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);

            MyTaskManager tasks = new MyTaskManager() { task1, task2 };

            // Act
            bool result = tasks.Remove(task1);

            // Assert
            Assert.IsTrue(result);
            CollectionAssert.DoesNotContain(tasks, task1);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Remove_NotExistenTaskInput()
        {
            // Arrange
            MyGeneralTask task1 = new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);
            MyGeneralTask task2 = new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);
            MyGeneralTask task3 = new MyGeneralTask(3, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted);

            MyTaskManager tasks = new MyTaskManager() { task1, task2 };

            // Act
            bool result = tasks.Remove(task3);

            // Assert
            Assert.IsFalse(result);
            CollectionAssert.DoesNotContain(tasks, task3);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void ToList_FullManager()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager()
            {
                new MyGeneralTask(1, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyGeneralTask(2, "No title", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(3, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted),
                new MyUniversityTask(4, "No course", "No Description", new DateTime(2008, 9, 17), MyTaskStatus.NotStarted)
            };

            // Act
            List<MyTask> list = tasks.ToList();

            // Assert
            CollectionAssert.AreEquivalent(tasks, list);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void ToList_EmptyManager()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            // Act
            List<MyTask> list = tasks.ToList();

            // Assert
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void SortTasksByRemainingDate_SortsCorrectly()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            MyGeneralTask task1 = new MyGeneralTask(1, "No title", "No Description", DateTime.UtcNow.AddDays(1), MyTaskStatus.NotStarted);
            MyGeneralTask task2 = new MyGeneralTask(2, "No title", "No Description", DateTime.UtcNow.AddDays(2), MyTaskStatus.NotStarted);
            MyGeneralTask task3 = new MyGeneralTask(3, "No title", "No Description", DateTime.UtcNow.AddDays(3), MyTaskStatus.NotStarted);
            MyGeneralTask task4 = new MyGeneralTask(4, "No title", "No Description", DateTime.UtcNow.AddDays(4), MyTaskStatus.NotStarted);

            tasks.Add(task4);
            tasks.Add(task2);
            tasks.Add(task3);
            tasks.Add(task1);

            // Act
            tasks.SortTasksByRemainingDate();

            // Assert
            CollectionAssert.AreEqual(new MyTaskManager { task1, task2, task3, task4 }, tasks);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void SortTasksByReamingDate_EmptyManager()
        {
            // Arrange
            MyTaskManager tasks = new MyTaskManager();

            // Act
            tasks.SortTasksByRemainingDate();

            // Assert
            CollectionAssert.AreEqual(new MyTaskManager(), tasks);
        }
    }
}