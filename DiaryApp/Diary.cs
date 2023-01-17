using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiaryApp;

public class Diary
{
	public bool Run;
	public int DaysDiff; // days difference between today and some date
	public MenuController MenuController;
	private const string Format = "MM/dd/yyyy";
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
			switch (GetInput())
			{
				case "n": case "next":
					DaysDiff += 7;
					break;
				case "p": case "prev":
					DaysDiff -= 7;
					break;
				case "a": case "add":
					AddTask();
					break;
				case "e": case "edit":
					// EditTask();
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

	private void DeleteTask()
	{
		ShowAllTasks();
		
		MenuController.SelectDelete();
		string number = GetInput(0, "number");
	}
	
	// PRINT ALL TASKS IN JSONEK
	private void ShowAllTasks()
	{
		int number = 0;
		JObject? tasks = Task.ReadTasks();

		if (tasks != null)
			foreach (var day in tasks)
			{
				Console.Write(day.Key);

				foreach (JToken task in day.Value!)
				{
					Console.Write(
						"   - " + task["Title"] + " (" + task["Description"] + ") [" + number + "]\n"
					);
					number++;
				}
			}
	}
	
	// PRINT ALL WEEK DAYS WITH TASKS
	private void ShowWeek()
	{
		DateTime startDay;
		JObject? tasks = Task.ReadTasks();

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
			Console.Write(startDay.AddDays(x).ToString(Format) + " [" +  startDay.AddDays(x).DayOfWeek + "]");
			if (startDay.AddDays(x) == DateTime.Today)
			{
				Console.Write(" -> TODAY");
			}
			Console.Write("\n");

			if (tasks?[startDay.AddDays(x).ToString(Format)] != null)
				foreach (JToken task in tasks[startDay.AddDays(x).ToString(Format)]!)
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