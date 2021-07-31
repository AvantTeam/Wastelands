using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utilities;
using static Utils;

namespace Content
{
	public class Rooms
	{
		public RandomDictionary<List<List<string>>> roomDict = new RandomDictionary<List<List<string>>>();
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

					roomData.Reverse();
				}
				else Debug.Log("Row Error: " + data.Length);

				if (size[0] == 25 && size[1] == 18)
				{
					if (valid)
					{
						connectionData = data[2];

						roomDict.Add(connectionData, roomData);
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