using ProjectB_TaskManager.Classes.MyTasks;

namespace ProjectB_TaskManager.Interfaces
{
    public interface IDuplicateCheckable
    {
        bool IsDuplicate(MyTask item);
    }
}