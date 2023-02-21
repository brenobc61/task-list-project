﻿using System.Text;
using TaskListProject.Entities.CustomExceptions;
using TaskListProject.Entities.TaskRelated;
using Task = TaskListProject.Entities.TaskRelated.Task;

namespace TaskListProject.Entities.Utility
{
    internal static class WindowPrinter
    {
        public static void PrintMenu(TaskList taskList)
        {
            Console.Clear();
            Console.WriteLine("O===========================================================O");
            Console.WriteLine("|                      | MY TASK LIST |                     |");
            Console.WriteLine("O===========================================================O");
            Console.WriteLine("O- TASKS ---------------------------------------------------O");
            Console.WriteLine("O===========================================================O");
            Console.WriteLine("|                                                           |");

            foreach (Task task in taskList.GetTasks())
            {
                Console.Write("|");
                PrintAlternateTextColorsIfFinishedTask(task);
                Console.Write(task + PrintRemainingEmptySpaces(task));
                TextColorChanger.ChangeTextColorToWhite();
                Console.WriteLine(" |");
            }

            Console.WriteLine("|                                                           |");
            Console.WriteLine("O===========================================================O");
            Console.WriteLine("O- MENU ----------------------------------------------------O");
            Console.WriteLine("O===========================================================O");
            Console.WriteLine("|            [1] VIEW TASK         [4] ADD TASK             |");
            Console.WriteLine("|            [2] EDIT TASK         [5] CLEAR TASK           |");
            Console.WriteLine("|            [3] FINISH TASK       [6] EXIT                 |");
            Console.WriteLine("O===========================================================O");
            Console.WriteLine();
            Console.WriteLine("[ ENTER MENU OPTION ]");
            Console.Write("--> ");
        }

        public static void PrintAlternateTextColorsIfFinishedTask(Task task)
        {
            if (task.IsFinished)
            {
                TextColorChanger.ChangeTextColorToCyan();
            }
            else
            {
                TextColorChanger.ChangeTextColorToRed();
            }
        }

        public static string PrintRemainingEmptySpaces(Task task)
        {
            if (task == null)
            {
                return "                                                      ";
            }

            int remaningEmptySpaces = Task.TitleMaximumLength - task.Title.Length;
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < remaningEmptySpaces; i++)
            {
                stringBuilder.Append(" ");
            }

            return stringBuilder.ToString();
        }

        public static void PrintAskTaskNumberToViewWindow(TaskList taskList)
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER TASK NUMBER TO VIEW ]");
            Console.Write("--> ");

            try
            {
                int taskNumber = int.Parse(Console.ReadLine());
                Task task = taskList.SearchTaskByNumber(taskNumber);
                PrintViewTaskWindow(task);
            }
            catch (Exception)
            {
                PrintInvalidInput();
            }
        }

        public static void PrintViewTaskWindow(Task task)
        {
            Console.Clear();
            Console.WriteLine("O========================| VIEW TASK |======================O");
            Console.WriteLine();
            Console.WriteLine(task.Title);
            Console.WriteLine();
            Console.WriteLine(task.Description);
            Console.WriteLine();
            Console.WriteLine("DATE OF CREATION: " + task.CreationDate);
            Console.WriteLine();

            if (task.IsFinished)
            {
                Console.Write("THIS TASK IS ");
                TextColorChanger.ChangeTextColorToCyan();
                Console.WriteLine("FINISHED");
            }
            else
            {
                Console.Write("THIS TASK IS ");
                TextColorChanger.ChangeTextColorToRed();
                Console.WriteLine("UNFINISHED");
            }

            TextColorChanger.ChangeTextColorToWhite();

            Console.WriteLine();
            Console.WriteLine("O===========================================================O");
            Console.ReadKey();
        }

        public static void PrintAskTaskNumberToEditWindow(TaskList taskList)
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER TASK NUMBER TO EDIT ]");
            Console.Write("--> ");

            try
            {
                int taskNumber = int.Parse(Console.ReadLine());
                Task task = taskList.SearchTaskByNumber(taskNumber);
                PrintEditTaskWindow(task);
            }
            catch (Exception)
            {
                PrintInvalidInput();
            }
        }

        public static void PrintEditTaskWindow(Task task)
        {
            string newTitle = "";
            string newDescription = "";

            while (true)
            {
                PrintEditHeaderWindow(task.Title);

                try
                {
                    char answerToChangeTitle = PrintAskToChangeTitleWindow();

                    if (answerToChangeTitle == 'y')
                    {
                        newTitle = PrintGetNewTitleWindow();
                    }
                    else if (answerToChangeTitle == 'n')
                    {
                        // Nothing happens
                    }
                    else
                    {
                        PrintInvalidInput();
                        continue;
                    }

                    char answerToChangeDescription = PrintAskToChangeDescriptionWindow();

                    if (answerToChangeDescription == 'y')
                    {
                        newDescription = PrintGetNewDescriptionWindow();
                    }
                    else if (answerToChangeDescription == 'n')
                    {
                        // Nothing happens
                    }
                    else
                    {
                        PrintInvalidInput();
                        continue;
                    }

                    char answerToApplyChanges = PrintAskToApplyChangesWindow();

                    if (answerToApplyChanges == 'y')
                    {
                        ChangeTaskTitleBasedOnChoices(task, answerToChangeTitle, newTitle);
                        ChangeTaskDescriptionBasedOnChoices(task, answerToChangeDescription, newDescription);
                        PrintTaskChangesAppliedWindow();
                        break;
                    }
                    else if (answerToApplyChanges == 'n')
                    {
                        continue;
                    }
                    else
                    {
                        PrintInvalidInput();
                    }
                }
                catch (DescriptionLengthException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    continue;
                }
                catch (TitleLengthException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    continue;
                }
                catch (Exception)
                {
                    PrintInvalidInput();
                    continue;
                }
            }
        }

        public static void PrintEditHeaderWindow(string taskTitle)
        {
            Console.Clear();
            Console.WriteLine("O========================| EDIT TASK |======================O");
            Console.WriteLine();
            Console.WriteLine("[ TASK TO BE EDITED ]");
            Console.WriteLine("--> " + taskTitle);
        }

        public static char PrintAskToChangeTitleWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ CHANGE TITLE? (y/n) ]");
            Console.Write("--> ");
            char answerToChangeTitle = char.Parse(Console.ReadLine());

            return answerToChangeTitle;
        }

        public static string PrintGetNewTitleWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER NEW TITLE ]");
            Console.Write("--> ");
            string newTitle = Console.ReadLine();

            return newTitle;
        }

        public static char PrintAskToChangeDescriptionWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ CHANGE DESCRIPTION? (y/n) ]");
            Console.Write("--> ");
            char answerToChangeDescription = char.Parse(Console.ReadLine());

            return answerToChangeDescription;
        }

        public static string PrintGetNewDescriptionWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER NEW DESCRIPTION ]");
            Console.Write("--> ");
            string newDescription = Console.ReadLine();

            return newDescription;
        }

        public static char PrintAskToApplyChangesWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ APPLY CHANGES? (y/n) ]");
            Console.Write("--> ");
            char answerToApplyChanges = char.Parse(Console.ReadLine());

            return answerToApplyChanges;
        }

        public static void ChangeTaskTitleBasedOnChoices(Task task, char answerToChangeTitle, string newTitle)
        {
            if (answerToChangeTitle == 'y')
            {
                if (newTitle != null && newTitle != "")
                {
                    task.SetTitle(newTitle);
                }
                else
                {
                    task.SetTitle("No title");
                }
            }
        }

        public static void ChangeTaskDescriptionBasedOnChoices(Task task, char answerToChangeDescription, string newDescription)
        {
            if (answerToChangeDescription == 'y')
            {
                if (newDescription != null && newDescription != "")
                {
                    task.SetDescription(newDescription);
                }
                else
                {
                    task.SetDescription("No description");
                }
            }
        }

        public static void PrintTaskChangesAppliedWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ TASK CHANGES APPLIED ]");
            Console.ReadKey();
            Console.Clear();
        }

        public static void PrintAskTaskNumberToFinishWindow(TaskList taskList)
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER TASK NUMBER ]");
            Console.Write("--> ");

            try
            {
                int taskNumber = int.Parse(Console.ReadLine());
                taskList.FinishTaskByNumber(taskNumber);
                Console.WriteLine();
                Console.WriteLine("[ FINISHED TASK " + taskNumber + " ]");
                Console.ReadKey();
            }
            catch (TaskException ex)
            {
				Console.WriteLine();
				Console.WriteLine(ex.Message);
				Console.ReadKey();
			}
			catch (Exception)
			{
                PrintInvalidInput();
			}
		}

        public static void PrintAddTaskWindow(TaskList taskList)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("O========================| ADD TASK |=======================O");
                Console.WriteLine();
                Console.WriteLine("[ ENTER TASK TITLE ]");
                Console.Write("--> ");
                string? title = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("[ ENTER TASK DESCRIPTION ]");
                Console.Write("--> ");
                string? description = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("[ CONFIRM NEW TASK? (y/n) ]");
                Console.Write("--> ");
                char answerToConfirmNewTask = char.Parse(Console.ReadLine());

                if (answerToConfirmNewTask == 'y')
                {
                    try
                    {
                        taskList.AddTask(title, description);
                        Console.WriteLine();
                        Console.WriteLine("[ TASK ADDED ]");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    catch (TitleLengthException ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                        continue;
                    }
					catch (DescriptionLengthException ex)
					{
						Console.WriteLine();
						Console.WriteLine(ex.Message);
						Console.ReadKey();
						continue;
					}
				}
                else if (answerToConfirmNewTask == 'n')
                {
                    continue;
                }
                else
                {
                    PrintInvalidInput();
                }
            }
        }

        public static void PrintAskTaskNumberToClearWindow(TaskList taskList)
        {
            Console.WriteLine();
            Console.WriteLine("[ ENTER TASK NUMBER TO CLEAR ]");
            Console.Write("--> ");

            try
            {
                int taskNumber = int.Parse(Console.ReadLine());
                taskList.ClearTask(taskNumber);
                Console.WriteLine();
                Console.WriteLine("[ TASK " + taskNumber + " CLEARED ]");
                Console.ReadKey();
                Console.Clear();
            }
            catch (TaskException ex)
            {
                Console.WriteLine();
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
			catch (Exception)
			{
				PrintInvalidInput();
			}
		}

        public static void PrintExitWindow()
        {
            Console.WriteLine();
            Console.WriteLine("[ APPLICATION CLOSED ]");
            Console.ReadKey();
        }

        public static void PrintInvalidInput()
        {
            Console.WriteLine();
            Console.WriteLine("[ INVALID INPUT ]");
            Console.ReadKey();
        }
    }
}