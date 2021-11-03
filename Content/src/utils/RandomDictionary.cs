using System.Collections.Generic;

namespace wastelands.src.utils
{
    public class RandomDictionary<A, B>
    {
        public Dictionary<A, List<B>> values;

        public void Add(A key, B value)
        {
            if (!values.ContainsKey(key))
            {
                values.Add(key, new List<B>());
            }

            values[key].Add(value);
        }

        public B this[A key]
        {
            get
            {
                List<B> v = values[key];
                return v[Vars.random.Next(0, v.Count)];
            }
        }
    }
}
