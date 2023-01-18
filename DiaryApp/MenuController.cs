namespace DiaryApp;

public class MenuController
{
	public void MainMenu()
	{
		Console.Write(
			"\n[P]REV WEEK  |  [N]EXT WEEK\n\n" +
			"----------ACTIONS----------\n" +
			"[S]HOW ALL TASKS\n" +
			"[A]DD\n" +
			"[D]ELETE\n" +
			"[E]DIT\n" +
			"[L]EAVE\n" +
			"---------------------------\n"
		);
	}
	public void SelectDate(string date = "")
	{
		Console.Write(
			"--------SELECT DATE--------\n" +
			"FORMAT: year-month-day (2000-12-1)\n"
		);
		if (date != "")
		{
			Console.Write("CURRENT: " + date + "\n");
		}
		Console.Write("---------------------------\n");
	}
	public void SelectTitle(string title = "")
	{
		Console.Write(
			"--------SELECT TITLE-------\n" +
			"MAX 50 CHARS\n"
		);
		if (title != "")
		{
			Console.Write("CURRENT: " + title + "\n");
		}
		Console.Write("---------------------------\n");
	}
	public void SelectDescription(string description = "")
	{
		Console.Write(
			"-----SELECT DESCRIPTION----\n" +
			"MAX 300 CHARS\n"
		);
		if (description != "")
		{
			Console.Write("CURRENT: " + description + "\n");
		}
		Console.Write("---------------------------\n");
	}
	public void SelectDelete()
	{
		Console.Write(
			"\n---SELECT TASK TO DELETE---\n" +
			"SELECT ONLY TASK NUMBER\n" +
			"---------------------------\n"
		);
	}
	public void SelectEdit()
	{
		Console.Write(
			"\n---SELECT TASK TO EDIT---\n" +
			"SELECT ONLY TASK NUMBER\n" +
			"---------------------------\n"
		);
	}

	public void ShowAllTasks()
	{
		Console.Write(
			"\n---------------------------\n" +
			"PRESS ENTER TO MAIN MENU\n"
		);
	}
}