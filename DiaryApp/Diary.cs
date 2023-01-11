using System.Text.RegularExpressions;

namespace DiaryApp;

public class Diary
{
	public bool Run;
	public MenuController MenuController;
	internal Diary()
	{
		Run = true;
		MenuController = new MenuController();
	}
	public void Loop()
	{
		ShowWeek();

		while (Run)
		{
			// MenuController.MainMenu();
			// switch (GetInput())
			// {
			// 	case "a": case "add":
			// 		// AddRecord();
			// 		break;
			// 	case "e": case "edit":
			// 		// EditRecord();
			// 		break;
			// 	case "d": case "delete":
			// 		// DeleteRecord();
			// 		break;
			// 	case "l": case "leave":
			// 		Run = false;
			// 		break;
			// }
		}
	}

	private void ShowWeek()
	{
		List<DateTime> days = new List<DateTime>();

		// while (true)
		// {
		// 	if (DateTime.Today)
		// 	{
		// 		
		// 	}
		// }
		DateTime startDay = DateTime.Today.AddDays(1);
		 
		
		
		
		for (int x = 1; x < 7; x++)
		{
			days.Add(DateTime.Today.AddDays(x));
			// Console.WriteLine(days[x]);
		}
		
		// Console.Write(startOfWeek);
	}
	
	private string GetInput(int maxLetters = 0, string type = "letter")
	{
		string input;
		{
			do
			{
				input = Console.ReadLine()!;
				input = input.ToLower();

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
			} while (input == "");
			
			Console.Clear();
			return input.ToLower();
		}
	}
}