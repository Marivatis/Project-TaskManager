using ProjectС_TaskManager.Classes.MyTasks;

namespace ProjectС_TaskManager.Interfaces
{
    public interface IDuplicateCheckable
    {
        bool IsDuplicate(MyTask item);
    }
}