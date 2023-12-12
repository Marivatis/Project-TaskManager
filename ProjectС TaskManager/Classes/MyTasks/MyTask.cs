using ProjectB_TaskManager.Enums;
using System;

namespace ProjectB_TaskManager.Classes.MyTasks
{
    public abstract class MyTask : ITablePrintable
    {
        protected int id;
        protected string description;
        protected DateTime deadline;
        protected MyTaskStatus status;

        protected MyTask(int id,string description, DateTime deadline, MyTaskStatus status) 
        {
            this.id = id;
            this.description = description;
            this.deadline = deadline;
            this.status = status;
        }

        public int Id
        {
            get 
            {
                return id; 
            }
            set
            {
                if (value > 99 || value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Id must be from 1 to 99.");
                }

                id = value;
            }
        }

        public abstract string Description { get; set; }      
        
        public virtual DateTime Deadline 
        {
            get
            {
                return deadline;
            }
            set
            {
                if (value < DateTime.UtcNow)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Deadline cannot be less than the current date.");
                }

                deadline = value;
            }
        }
        public virtual MyTaskStatus Status 
        {
            get
            {
                return status;
            }
            set
            {
                if (!Enum.IsDefined(typeof(MyTaskStatus), value))
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Value does not match enum MyTaskStatus.");
                }

                status = value;
            }
        }

        public TimeSpan RemainingTime => Deadline - DateTime.UtcNow;

        public abstract void MarkAsCompleted();

        public virtual bool IsOverdue()
        {
            return RemainingTime <= TimeSpan.Zero;
        }

        public abstract override int GetHashCode();

        public abstract string GetTableHeader();
        public abstract string GetTableRow();
        public abstract string GetTableFooter();
    }
}