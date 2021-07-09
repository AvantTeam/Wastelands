using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static Utils;

namespace Content
{
	public class Rooms
	{
		List<List<List<string>>> R = new List<List<List<string>>>();
		List<List<List<string>>> D = new List<List<List<string>>>();
		List<List<List<string>>> L = new List<List<List<string>>>();
		List<List<List<string>>> U = new List<List<List<string>>>();
		string directory = "./Assets/Resources/Rooms";
		FileInfo[] fileInfo;
		string[] fileNames;
		char dot, semicolon;
		public void ClassifyRoom(string room)
		{
			dot = '.';
			semicolon = ';';

			string[] data = SplitByChar(room, semicolon).ToArray();

			int[] size = Array.ConvertAll<string, int>(SplitByChar(data[1], dot).ToArray(), int.Parse);

			List<List<String>> roomData = new List<List<string>>();
			string connectionData;

			bool valid = data.Length == 21;

			try
			{
				if (valid)
				{
					for (int i = 3; i < data.Length; i++)
					{
						List<string> rowList = SplitByChar(data[i], dot);

						if (rowList.Count != 25)
						{
							Debug.Log("Column Error: " + rowList.Count + " " + DebugList<string>(rowList));
							valid = false;
						}
						else roomData.Add(rowList);
					}
				}
				else Debug.Log("Row Error: " + data.Length);

				if (size[0] == 25 && size[1] == 18)
				{
					if (valid)
					{
						connectionData = data[2];

						if (connectionData.Contains("R"))
						{
							R.Add(roomData);
						}

						if (connectionData.Contains("D"))
						{
							D.Add(roomData);
						}

						if (connectionData.Contains("L"))
						{
							L.Add(roomData);
						}

						if (connectionData.Contains("U"))
						{
							U.Add(roomData);
						}
					}
				}
			}
			catch (Exception e)
			{
				Debug.Log(e);
			}
		}

		public void Load()
		{
			fileNames = Directory.GetFiles(directory, "*.wrd", SearchOption.AllDirectories);

			foreach (string i in fileNames)
			{
				try
				{
					using (StreamReader sr = new StreamReader(i))
					{
						string line;

						while ((line = sr.ReadLine()) != null)
						{
							ClassifyRoom(line);
						}
					}
				}
				catch (Exception e)
				{
					Debug.Log(e);
				}
			}
		}
	}
}