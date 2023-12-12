using ProjectB_TaskManager.Classes.General;
using ProjectB_TaskManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectB_TaskManager.Classes.MyTasks
{
    public class MyUniversityTask : MyTask
    {
        private string courseName;

        public MyUniversityTask()
            : this(0, "No Course", "No Description", DateTime.MinValue, MyTaskStatus.NotStarted) { }

        public MyUniversityTask(int id, string courseName, string description, DateTime deadline, MyTaskStatus status)
            : base(id, description, deadline, status)
        {
            this.courseName = courseName;
        }

        public string CourseName
        {
            get
            {
                return courseName;
            }
            set
            {
                if (value.Length > 11)
                {
                    throw new ArgumentException(nameof(value), "The course name length must be from 1 to 11 characters!");
                }

                if (string.IsNullOrEmpty(value)) 
                {
                    courseName = "No Course";
                }
                else
                {
                    courseName = value;
                }
            }
        }

        public override string Description
        {
            get 
            {
                return description; 
            }
            set 
            {
                if (string.IsNullOrEmpty(value))
                {
                    description = "No Description";
                    return;
                }

                if (value.Length > 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The description length must be from 1 to 100 characters!");
                }

                description = value;                
            }
        }

        public override void MarkAsCompleted()
        {
            Status = MyTaskStatus.Completed;
        }

        public override int GetHashCode()
        {
            int hash = 4;

            hash += id.GetHashCode();
            hash += courseName.GetHashCode();

            return hash;
        }

        public override string GetTableFooter()
        {
            // Id Max Length --> 2
            // Course Name Max Length --> 11
            // Task Description Max Length --> 21
            // Task Status Max Length --> 11
            // Deadline Max Length --> 10
            // All Borders Max Length --> 16

            return new String('-', 71);
        }
        public override string GetTableHeader()
        {
            StringBuilder tableHeader = new StringBuilder("| ");
            StringFormatter formatter = new StringFormatter();

            tableHeader.Append(formatter.FormatToLength("Id", 2) + " | ");
            tableHeader.Append(formatter.FormatToLength("Course Name", 11) + " | ");
            tableHeader.Append(formatter.FormatToLength("Task Description", 21) + " | ");
            tableHeader.Append(formatter.FormatToLength("Task Status", 11) + " | ");
            tableHeader.Append(formatter.FormatToLength("Deadline", 10) + " |");

            return tableHeader.ToString();
        }
        public override string GetTableRow()
        {
            StringBuilder tableRow = new StringBuilder("| ");
            StringFormatter formatter = new StringFormatter();

            tableRow.Append(formatter.FormatToLength(id.ToString(), 2) + " | ");
            tableRow.Append(formatter.FormatToLength(courseName, 11) + " | ");

            string[] description = formatter.FormatToLength(this.description, 21).Split('\n');
            tableRow.Append(description[0] + " | ");

            tableRow.Append(formatter.FormatToLength(status.ToString(), 11) + " | ");
            tableRow.Append(deadline.ToString("dd.MM.yyyy") + " |");

            for (int i = 1; i < description.Length; i++)
            {
                tableRow.Append("\n| ");
                tableRow.Append(formatter.FormatToLength(string.Empty, 2) + " | ");
                tableRow.Append(formatter.FormatToLength(string.Empty, 11) + " | ");

                if (i == description.Length - 1) 
                    tableRow.Append(formatter.FormatToLength(description[i], 21) + " | ");
                else
                    tableRow.Append(description[i] + " | ");
                                    
                tableRow.Append(formatter.FormatToLength(string.Empty, 11) + " | ");
                tableRow.Append(formatter.FormatToLength(string.Empty, 10) + " | ");
            }

            return tableRow.ToString();
        }
    }
}
