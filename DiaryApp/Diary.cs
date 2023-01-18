using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace DiaryApp;

public class Diary
{
	public bool Run;
	public int DaysDiff; // days difference between today and some date
	public MenuController MenuController;
	private const string Format = "yyyy-MM-dd";
	internal Diary()
	{
		Run = true;
		DaysDiff = 0;
		MenuController = new MenuController();
	}
	public void Loop()
	{
		
		while (Run)
		{
			Console.WriteLine("-----------DIAREK----------\n");
			ShowWeek();
			MenuController.MainMenu();
			switch (GetInput(0, "text"))
			{
				case "n": case "next":
					DaysDiff += 7;
					break;
				case "p": case "prev":
					DaysDiff -= 7;
					break;
				case "s": case "show all tasks":
					ShowAllTasks();
					MenuController.ShowAllTasks();
					Console.ReadLine();
					Console.Clear();
					break;
				case "a": case "add":
					AddTask();
					break;
				case "e": case "edit":
					EditTask();
					break;
				case "d": case "delete":
					DeleteTask();
					break;
				case "l": case "leave":
					Run = false;
					break;
			}
		}
	}
	
	// ADD TASK TO JSON FILE
	private void AddTask()
	{
		Task task = new Task();

		MenuController.SelectDate();
		task.Date = GetInput(0, "date");
		MenuController.SelectTitle();
		task.Title = GetInput(50, "text");
		MenuController.SelectDescription();
		task.Description = GetInput(300, "text");
		
		task.SaveTasks();
	}

	// PRINT EDIT WINDOW
	private void EditTask()
	{
		if (ShowAllTasks())
		{
			Task task = new Task();
			JArray tasks = Task.ReadTasks()!;
			int index = -1;
			
			do
			{
				
				Console.Clear();
				ShowAllTasks();
				MenuController.SelectEdit();
				if (index != -1)
				{
					Console.Write("\nUndefined INDEX\n");
				}
				index = Int32.Parse(GetInput(0, "number"));
			} while (index >= tasks.Count());
			
			MenuController.SelectDate(tasks[index]["Date"]!.ToString());
			task.Date = GetInput(0, "date");
			MenuController.SelectTitle(tasks[index]["Title"]!.ToString());
			task.Title = GetInput(50, "text");
			MenuController.SelectDescription(tasks[index]["Description"]!.ToString());
			task.Description = GetInput(300, "text");
			
			task.EditTask(index);
		}		
	}

	// PRINT DELETE WINDOW
	private void DeleteTask()
	{
		if (ShowAllTasks())
		{
			MenuController.SelectDelete();
			Task.RemoveFromTasks(Int32.Parse(GetInput(0, "number")));	
		}
	}
	
	// PRINT ALL TASKS IN JSONEK
	private bool ShowAllTasks()
	{
		int number = 0;
		JArray? tasks = Task.ReadTasks();

		if (tasks!.Count > 0)
			foreach (JToken task in tasks)
			{
				Console.Write(
					task["Date"] + "  -  " + task["Title"] + " (" + task["Description"] + ") [" + number + "]\n"
				);
				number++;
			}
		else
		{
			Console.Write("YOU DONT HAVE ANY TASK");
			Thread.Sleep(2000);
			Console.Clear();
			return false;
		}

		return true;
	}
	
	// PRINT ALL WEEK DAYS WITH TASKS
	private void ShowWeek()
	{
		DateTime startDay;
		JArray? tasks = Task.ReadTasks();

		if (DaysDiff == 0)
		{
			while (true)
			{
				if (DateTime.Today.AddDays(DaysDiff).DayOfWeek.ToString() == "Monday")
				{
					startDay = DateTime.Today.AddDays(DaysDiff);
					break;
				}
				DaysDiff--;
			}
		}
		else
		{
			startDay = DateTime.Today.AddDays(DaysDiff);
		}

		for (int x = 0; x < 7; x++)
		{
			string dateString = startDay.AddDays(x).ToString(Format);
			Console.Write(dateString + " [" +  startDay.AddDays(x).DayOfWeek + "]");
			if (startDay.AddDays(x) == DateTime.Today)
			{
				Console.Write(" -> TODAY");
			}
			Console.Write("\n");

			IEnumerable<JToken> thisDayTasks = tasks?.Where(task => task["Date"]!.ToString() == dateString);
			
			foreach (JToken task in thisDayTasks)
			{
				Console.Write(
					"   - " + task["Title"] + " (" + task["Description"] + ")\n"
				);
			}
		}
	}
	
	// USER INPUT FUNCTION
	// maxLetters [int] - max allowed chars
	// type [string]	- letter (only letters without spaces)
	//					- text (letters, numbers, spaces)
	//					- number (only numbers)
	//					- date (only date)
	private string GetInput(int maxLetters = 0, string type = "letter")
	{
		string input;
		{
			do
			{
				input = Console.ReadLine()!;
				input = input.ToLower();

				// Letters
				if (type == "letter")
				{
					if (Regex.IsMatch(input, @"^[a-zA-Z]+$"))
					{
						if (!(maxLetters >= input.Length) && maxLetters != 0)
						{
							Console.WriteLine("Enter only " + maxLetters + " letter");
							input = "";
						}
						else if (input.Length == 0)
						{
							Console.WriteLine("Write something PLS");
						}    
					}
					else
					{
						Console.WriteLine("Only letters PLS");
						input = "";
					}
				}
				// Number
				else if (type == "number")
				{
					if (Regex.IsMatch(input, @"^[1234567890]+$"))
					{
						if (!(maxLetters >= input.Length) && maxLetters != 0)
						{
							Console.WriteLine("Enter only " + maxLetters + " letter");
							input = "";
						}
						else if (input.Length == 0)
						{
							Console.WriteLine("Write something PLS");
						}
					}
					else
					{
						Console.WriteLine("Only numbers PLS");
						input = "";
					}
				}
				// Date
				else if (type == "date")
				{
					if (DateTime.TryParse(input, out DateTime parsed))
					{
						Console.WriteLine("Enter only " + maxLetters + " letter");
						input = parsed.ToString(Format);
					}
					else
					{
						Console.WriteLine("WRITE DATE PLS");
						input = "";
					}
				}
				// Text
				else if (type == "text")
				{
					if (Regex.IsMatch(input, @"^[a-zA-Z0123456789 ]+$"))
					{
						if (!(maxLetters >= input.Length) && maxLetters != 0)
						{
							Console.WriteLine("Enter only " + maxLetters + " letter");
							input = "";
						}
						else if (input.Length == 0)
						{
							Console.WriteLine("Write something PLS");
						}    
					}
					else
					{
						Console.WriteLine("Only letters without special characters PLS");
						input = "";
					}
				}
			} while (input == "");
			
			Console.Clear();
			return input.ToLower();
		}
	}
}