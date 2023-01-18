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
	public static JArray? ReadTasks()
	{
		JsonFileExist("tasks.json");
		
		string jsonString = File.ReadAllText("tasks.json");
		dynamic? jsonFile = JsonConvert.DeserializeObject(jsonString);

		return jsonFile;
	}
	
	// SAVE TO TASKS JSON FILE
	public void SaveTasks()
	{
		JsonFileExist("tasks.json");

		dynamic? tasks = ReadTasks();

		JObject task = new JObject();
		
		task.Add("Date", Date);
		task.Add("Title", Title);
		task.Add("Description", Description);
		
		tasks?.Add(task);
		
		tasks = JsonConvert.SerializeObject(tasks);
		File.WriteAllText("tasks.json", tasks);
	}

	// REMOVE TASK FROM TASKS
	// index [int] - index of task
	public static void RemoveFromTasks(int index)
	{
		JArray? tasks = ReadTasks();
		
		tasks?.RemoveAt(index);
		
		string tasksString = JsonConvert.SerializeObject(tasks);
		File.WriteAllText("tasks.json", tasksString);
	}

	// EDIT TASK FROM TASKS
	// index [int] - index of task
	public void EditTask(int index)
	{
		JArray tasks = ReadTasks()!;
		
		tasks[index]["Date"] = Date;
		tasks[index]["Title"] = Title;
		tasks[index]["Description"] = Description;
		
		string tasksString = JsonConvert.SerializeObject(tasks);
		File.WriteAllText("tasks.json", tasksString);
	}
}