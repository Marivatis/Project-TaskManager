using ProjectB_TaskManager.Classes.MyTasks;
using ProjectB_TaskManager.Enums;

namespace TestProject.Tests
{
    [TestClass]
    public class MyTaskTast
    {
        private const string category_basic = "Basic";

        [TestMethod]
        [TestCategory(category_basic)]
        public void Id_ValidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act 
            task.Id = 1;

            // Assert
            Assert.AreEqual(1, task.Id);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Id_BelowZeroInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(()  => task.Id = -1);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Id_OverflowInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => task.Id = 100);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Description_ValidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            task.Description = "Some description";

            // Assert
            Assert.AreEqual("Some description", task.Description);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Description_NullInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            task.Description = null;

            // Arrange
            Assert.AreEqual("No Description", task.Description);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Description_EmptyStringInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            task.Description = string.Empty;

            // Assert
            Assert.AreEqual("No Description", task.Description);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Description_OverflowInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            string str = new string('|', 1000);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => task.Description = str);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Status_ValidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            task.Status = MyTaskStatus.InProgress;

            // Assert
            Assert.AreEqual(MyTaskStatus.InProgress, task.Status);
        }
        
        [TestMethod]
        [TestCategory(category_basic)]
        public void Status_InvalidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => task.Status = (MyTaskStatus)5 );   
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Deadline_ValidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            DateTime deadline = DateTime.UtcNow.AddDays(1);
            task.Deadline = deadline;

            // Assert
            Assert.AreEqual(deadline, task.Deadline);
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void Deadline_InvalidInput()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => task.Deadline = new DateTime(2000, 12, 12));
        }

        [TestMethod]
        [TestCategory(category_basic)]
        public void MarkAsCompleted_Valid()
        {
            // Arrange
            MyTask task = new MyGeneralTask();

            // Act
            task.MarkAsCompleted();

            // Assert
            Assert.AreEqual(MyTaskStatus.Completed, task.Status);
        }
    }
}
