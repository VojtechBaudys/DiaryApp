using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DiaryApp;

public class Task
{
	public string Date;
	public string Title;
	public string Description;
	
	internal Task()
	{
		Date = "";
		Title = "";
		Description = "";
	}
	
	// SAVE TO TASKS JSON FILE
	// date [string] - task date
	// title [string] - task title
	// description [description] - task description
	public void SaveTasks()
	{
		JsonFileExist("tasks.json");

		dynamic? tasks = ReadTasks();

		JObject task = new JObject();
		
		task.Add("Title", Title);
		task.Add("Description", Description);
		
		if (tasks?.ContainsKey(Date))
		{
			tasks?[Date].Add(task);
		}
		else
		{
			JArray taskArray = new JArray() {task};
			tasks?.Add(Date, taskArray);
		}
		
		tasks = JsonConvert.SerializeObject(tasks);
		File.WriteAllText("tasks.json", tasks);
	}
	
	// CHECK FILE IF EXISTS
	// FALSE -> CREATE NEW ONE
	// path [string] - file path
	public static void JsonFileExist(string path)
	{
		try
		{
			JsonConvert.DeserializeObject(File.ReadAllText(path));
		}
		catch
		{
			File.Create(path).Close();
			File.WriteAllText(path, "{}");
		}
	}
	
	// READ ALL TASKS
	public static JObject? ReadTasks()
	{
		JsonFileExist("tasks.json");
		
		string jsonString = File.ReadAllText("tasks.json");
		dynamic? jsonFile = JsonConvert.DeserializeObject<JObject>(jsonString);

		return jsonFile;
	}
}