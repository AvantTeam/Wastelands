using UnityEngine;
using System.Collections.Generic;

namespace Utilities
{
	public class RandomDictionary<T>
	{
		public List<string> keys = new List<string>();
		public List<T> values = new List<T>();

		public void Add(string key, T value){
			keys.Add(key);
			values.Add(value);
		}

		public T Get(string key){
			List<T> candidates = new List<T>();

			for (int i = 0; i < keys.Count; i++){
				if(keys[i] == key){
					candidates.Add(values[i]);
				}
			}

			try
			{
				return candidates[Random.Range(0, candidates.Count - 1)];
			} catch {
				return default(T);
			}
		}
	}
}