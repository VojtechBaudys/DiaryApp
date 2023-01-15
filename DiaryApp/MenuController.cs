namespace DiaryApp;

public class MenuController
{
	public void MainMenu()
	{
		Console.Write(
			"\n[P]REV WEEK  |  [N]EXT WEEK\n\n" +
			"----------ACTIONS----------\n" +
			"[A]DD\n" +
			"[D]ELETE\n" +
			"[E]DIT\n" +
			"[L]EAVE\n" +
			"---------------------------\n"
		);
	}
	public void SelectDate()
	{
		Console.Write(
			"--------SELECT DATE--------\n" +
			"FORMAT: month/day/year (12/1/2000)\n" +
			"---------------------------\n"
		);
	}
	public void SelectTitle()
	{
		Console.Write(
			"--------SELECT TITLE-------\n" +
			"MAX 50 CHARS\n" +
			"---------------------------\n"
		);
	}
	public void SelectDescription()
	{
		Console.Write(
			"-----SELECT DESCRIPTION----\n" +
			"MAX 300 CHARS\n" +
			"---------------------------\n"
		);
	}
}