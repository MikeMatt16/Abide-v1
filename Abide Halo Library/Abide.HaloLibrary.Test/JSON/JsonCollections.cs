using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Donek.Core.JSON
{
    /// <summary>
    /// Represents a collection of JSON element objects.
    /// </summary>
    public class JsonElementCollection : JsonObject, ICollection<JsonElement>, IEnumerable<JsonElement>
    {
        private readonly List<JsonElement> elements = new List<JsonElement>();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonElement this[int index]
        {
            get
            {
                if (index >= 0 && index < elements.Count) return elements[index];
                else throw new ArgumentOutOfRangeException(nameof(index));
            }
            set
            {
                if (index >= 0 && index < elements.Count) elements[index] = value;
                else throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonElement this[string name]
        {
            get
            {
                if (elements.Any(e => e.Name == name)) return elements.First(e => e.Name == name);
                else return null;
            }
        }
        /// <summary>
        /// Gets and returns the number of elments in the collection.
        /// </summary>
        public int Count
        {
            get { return elements.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        public JsonElementCollection() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            //Create
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            if (elements.Count > 0)
            {
                //Loop
                for (int i = 0; i < elements.Count - 1; i++)
                    sb.Append($"{elements[i].Stringify()}, ");
                sb.Append(elements[elements.Count - 1].Stringify());
            }

            sb.Append("}");

            //Return
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            return elements.Any(e => e.Name == name);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Contains(JsonElement element)
        {
            return elements.Contains(element);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            elements.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        public void Add(JsonElement element)
        {
            //Check
            if (elements.Contains(element)) return;

            //Add
            elements.Add(element);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public JsonElement Add(string name)
        {
            //Check
            if (name == null) throw new ArgumentNullException(nameof(name));

            //Add
            JsonElement element = new JsonElement() { Name = name };
            Add(element);

            //Return
            return element;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public bool Remove(JsonElement element)
        {
            //Return
            return elements.Remove(element);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<JsonElement> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        bool ICollection<JsonElement>.IsReadOnly => false;
        void ICollection<JsonElement>.CopyTo(JsonElement[] array, int arrayIndex)
        {
            elements.CopyTo(array, arrayIndex);
        }
        IEnumerator<JsonElement> IEnumerable<JsonElement>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Represents a collection of JSON objects.
    /// </summary>
    public class JsonObjectCollection : JsonObject, ICollection<JsonObject>, IEnumerable<JsonObject>
    {
        private readonly List<JsonObject> elements = new List<JsonObject>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public JsonObject this[int index]
        {
            get
            {
                if (index >= 0 && index < elements.Count) return elements[index];
                else throw new ArgumentOutOfRangeException(nameof(index));
            }
            set
            {
                if (index >= 0 && index < elements.Count) elements[index] = value;
                else throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        /// <summary>
        /// Gets and returns the number of elments in the collection.
        /// </summary>
        public int Count
        {
            get { return elements.Count; }
        }
        /// <summary>
        /// 
        /// </summary>
        public JsonObjectCollection() { }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string Stringify()
        {
            //Check
            if (elements.Count < 1) return string.Empty;

            //Create
            StringBuilder sb = new StringBuilder();

            //Loop
            for (int i = 0; i < elements.Count - 1; i++)
                sb.Append($"{elements[i].Stringify()}, ");
            sb.Append(elements[elements.Count - 1].Stringify());

            //Return
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            elements.Clear();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        public void Add(JsonObject @object)
        {
            //Check
            if (elements.Contains(@object)) return;

            //Add
            elements.Add(@object);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public bool Remove(JsonObject @object)
        {
            //Return
            return elements.Remove(@object);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<JsonObject> GetEnumerator()
        {
            return elements.GetEnumerator();
        }

        bool ICollection<JsonObject>.IsReadOnly => false;
        bool ICollection<JsonObject>.Contains(JsonObject item)
        {
            return elements.Contains(item);
        }
        void ICollection<JsonObject>.CopyTo(JsonObject[] array, int arrayIndex)
        {
            elements.CopyTo(array, arrayIndex);
        }
        IEnumerator<JsonObject> IEnumerable<JsonObject>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
