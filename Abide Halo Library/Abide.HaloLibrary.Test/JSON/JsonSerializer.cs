using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Donek.Core.JSON
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class JsonSerializer
    {
        private readonly List<object> objectList = new List<object>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public T Deserialize<T>(JsonObject jsonObject) where T : new()
        {
            //Check
            if (jsonObject == null) throw new ArgumentNullException(nameof(jsonObject));

            //Get value
            T instance = (T)GetValue(typeof(T), jsonObject);

            //Return
            return instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public object Deserialize(JsonObject jsonObject, Type objectType)
        {
            //Check
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            else if (jsonObject == null) throw new ArgumentNullException(nameof(jsonObject));

            //Check if value is null and type is nullable.
            else if (jsonObject is JsonNull && !objectType.IsValueType)
                return null;

            //Return
            return GetValue(objectType, jsonObject);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public object Deserialize(string json, Type objectType)
        {
            //Check
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));

            //Check
            if (JsonObject.TryParse(json, out JsonObject jsonObject))
                return GetValue(objectType, jsonObject);

            //Return
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T Deserialize<T>(string json) where T : new()
        {
            //Prepare
            T instance = default;

            //Parse JSON
            if (JsonObject.TryParse(json, out JsonObject jsonObject))
                instance = (T)GetValue(typeof(T), jsonObject);

            //Return
            return instance;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="object"></param>
        /// <returns></returns>
        public JsonObject Serialize(object @object)
        {
            //Check
            if (@object == null || objectList.Contains(@object))
                return new JsonNull();

            //Get type
            Type objectType = @object.GetType();

            //Check if array
            if (objectType.IsArray)
                return SerializeArray((Array)@object);

            //Check if IDictionary
            else if (objectType.GetInterface(typeof(IDictionary).Name) != null)
                return SerializeDictionary((IDictionary)@object);

            //Check if ICollection
            else if (objectType.GetInterface(typeof(ICollection).Name) != null)
                return SerializeCollection((ICollection)@object);

            //Check if object
            else if (Type.GetTypeCode(objectType) == TypeCode.Object)
            {
                //Add to object list to prevent circular references
                objectList.Add(@object);

                //Serialize
                JsonObject jsonObject = SerializeObject(@object);

                //Remove object from list
                objectList.Remove(@object);

                //Return
                return jsonObject;
            }

            //Check type code
            switch (Type.GetTypeCode(objectType))
            {
                case TypeCode.String: return new JsonString() { Value = (string)@object };
                case TypeCode.Boolean: return new JsonBoolean() { Value = (bool)@object };
                case TypeCode.Decimal: return new JsonNumber() { Value = (decimal)@object };
                case TypeCode.Char: return new JsonNumber() { Value = Convert.ToDecimal((char)@object) };
                case TypeCode.SByte: return new JsonNumber() { Value = Convert.ToDecimal((sbyte)@object) };
                case TypeCode.Byte: return new JsonNumber() { Value = Convert.ToDecimal((byte)@object) };
                case TypeCode.Int16: return new JsonNumber() { Value = Convert.ToDecimal((short)@object) };
                case TypeCode.UInt16: return new JsonNumber() { Value = Convert.ToDecimal((ushort)@object) };
                case TypeCode.Int32: return new JsonNumber() { Value = Convert.ToDecimal((int)@object) };
                case TypeCode.UInt32: return new JsonNumber() { Value = Convert.ToDecimal((uint)@object) };
                case TypeCode.Int64: return new JsonNumber() { Value = Convert.ToDecimal((long)@object) };
                case TypeCode.UInt64: return new JsonNumber() { Value = Convert.ToDecimal((ulong)@object) };
                case TypeCode.Single: return new JsonNumber() { Value = Convert.ToDecimal((float)@object) };
                case TypeCode.Double: return new JsonNumber() { Value = Convert.ToDecimal((double)@object) };
                default: return new JsonNull(); //Return null
            }
        }

        private JsonElementCollection SerializeObject(object @object)
        {
            //Prepare
            JsonElementCollection collection = new JsonElementCollection();
            JsonSerializeAttribute attribute;

            //Check
            if (@object == null) throw new ArgumentNullException(nameof(@object));
            Type objectType = @object.GetType();

            //Loop through properties in object
            foreach (PropertyInfo propertyInfo in objectType.GetProperties())
            {
                //Prepare
                JsonElement element = new JsonElement() { Name = propertyInfo.Name };

                try
                {
                    //Get property value
                    object propertyValue = propertyInfo.GetValue(@object);
                    if (propertyValue is Delegate) 
                        element.Value = new JsonNull();
                    else if (propertyValue is Type type) 
                        element.Value = new JsonString() { Value = type.AssemblyQualifiedName };
                    else
                    {
                        //Get JsonSerializeAttribute
                        attribute = propertyInfo.GetCustomAttribute<JsonSerializeAttribute>();
                        if (attribute != null)
                        {
                            //Check name
                            if (!string.IsNullOrEmpty(attribute.Name)) element.Name = attribute.Name;
                        }

                        //Set value
                        element.Value = Serialize(propertyValue);
                    }
                }
                catch { element.Value = new JsonNull(); }

                //Add element
                collection.Add(element);
            }

            //Loop through fields in object
            foreach (FieldInfo fieldInfo in objectType.GetFields())
            {
                //Prepare
                JsonElement element = new JsonElement() { Name = fieldInfo.Name };

                try
                {
                    //Get field value
                    object fieldValue = fieldInfo.GetValue(@object);

                    //Get JsonSerializeAttribute
                    attribute = fieldInfo.GetCustomAttribute<JsonSerializeAttribute>();
                    if (attribute != null)
                    {
                        //Check name
                        if (!string.IsNullOrEmpty(attribute.Name)) element.Name = attribute.Name;
                    }

                    //Set value
                    element.Value = Serialize(fieldValue);
                }
                catch { element.Value = new JsonNull(); }

                //Add element
                collection.Add(element);
            }

            //Return
            return collection;
        }
        private JsonArray SerializeDictionary(IDictionary dictionary)
        {
            //Prepare
            JsonArray jsonArray = new JsonArray();

            //Check
            if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

            //Loop
            foreach (object key in dictionary.Keys)
            {
                //Serialize key
                JsonObject keyObject = Serialize(key);

                //Serialize value
                JsonObject valueObject = Serialize(dictionary[key]);

                //Create new collection
                JsonElementCollection jsonKeyValuePair = new JsonElementCollection();
                jsonKeyValuePair.Add(new JsonElement() { Name = "key", Value = keyObject });
                jsonKeyValuePair.Add(new JsonElement() { Name = "value", Value = valueObject });

                //Add
                jsonArray.Elements.Add(jsonKeyValuePair);
            }

            //Return
            return jsonArray;
        }
        private JsonArray SerializeCollection(ICollection collection)
        {
            //Prepare
            JsonArray jsonArray = new JsonArray();

            //Check
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            //Loop
            foreach (object element in collection)
            {
                //Serialize object
                JsonObject @object = Serialize(element);

                //Add
                jsonArray.Elements.Add(@object);
            }

            //Return
            return jsonArray;
        }
        private JsonArray SerializeArray(Array array)
        {
            //Prepare
            JsonArray jsonArray = new JsonArray();

            //Check
            if (array == null) throw new ArgumentNullException(nameof(array));

            //Loop
            foreach (object element in array)
            {
                //Serialize object
                JsonObject @object = Serialize(element);

                //Add
                jsonArray.Elements.Add(@object);
            }

            //Return
            return jsonArray;
        }
        private void DeserializeObject(object instance, Type objectType, JsonObject jsonObject)
        {
            //Check
            JsonElementCollection jsonCollection = new JsonElementCollection();
            if (jsonObject is JsonElementCollection) jsonCollection = (JsonElementCollection)jsonObject;

            //Loop through publicly exposed writable properties
            foreach (var propertyInfo in objectType.GetProperties())
            {
                //Prepare
                string name = propertyInfo.Name;

                //Check attributes
                JsonSerializeAttribute attribute = propertyInfo.GetCustomAttribute<JsonSerializeAttribute>();
                if (attribute != null)
                {
                    if (!string.IsNullOrEmpty(attribute.Name)) name = attribute.Name;
                }

                //Check if writable
                if (propertyInfo.CanWrite)
                {
                    //Set property value
                    if (jsonCollection.Contains(name))
                        propertyInfo.SetValue(instance, GetValue(propertyInfo.PropertyType, jsonCollection[name].Value));
                }

                //Check if is not value type and is currently not null.
                else if (!propertyInfo.PropertyType.IsValueType && propertyInfo.GetValue(instance) != null)
                {
                    //Get instance
                    object value = propertyInfo.GetValue(instance);
                    Type propertyType = propertyInfo.PropertyType;

                    //Check if value implements IDictionary
                    if (propertyType.GetInterface(typeof(IDictionary<,>).Name) != null && value is IDictionary dictionary)
                    {
                        //Prepare
                        Type dictionaryType = propertyType.GetInterface(typeof(IDictionary<,>).FullName);
                        Type keyType = dictionaryType.GetGenericArguments()[0];
                        Type valueType = dictionaryType.GetGenericArguments()[1];
                        MethodInfo addMethod = dictionaryType.GetMethod("Add");
                        JsonObject jsonValue = jsonCollection[name].Value;

                        //Loop through key value pairs
                        if (jsonValue is JsonObjectCollection jsonObjects)
                            foreach (JsonObject jsonKeyValuePair in jsonObjects)
                            {
                                //Check if object is a JSON element collection
                                if (jsonKeyValuePair is JsonElementCollection jsonKeyValuePairCollection)
                                {
                                    //Get key and value from the JSON key value pair
                                    object keyValuePairKey = GetValue(keyType, jsonKeyValuePairCollection["key"].Value);
                                    object keyValuePairValue = GetValue(valueType, jsonKeyValuePairCollection["value"].Value);

                                    //Add
                                    addMethod.Invoke(value, new object[] { keyValuePairKey, keyValuePairValue });
                                }
                            }
                    }

                    //Check if value implements ICollection
                    else if (propertyType.GetInterface(typeof(ICollection<>).Name) != null)
                    {
                        //Prepare
                        Type collectionType = propertyType.GetInterface(typeof(ICollection<>).FullName);
                        Type elementType = collectionType.GetGenericArguments()[0];
                        MethodInfo addMethod = collectionType.GetMethod("Add");
                        JsonObject jsonValue = jsonCollection[name].Value;

                        //Deserialize element
                        if (jsonValue is JsonObjectCollection jsonObjects)
                            foreach (var foo in jsonObjects)
                            {
                                object element = GetValue(elementType, foo);
                                addMethod.Invoke(value, new object[] { element });
                            }
                    }

                    //Deserialize
                    else DeserializeObject(value, propertyType, jsonCollection[name].Value);
                }
            }

            //Loop through publicly exposed writable fields
            foreach (var fieldInfo in objectType.GetFields())
            {
                //Prepare
                string name = fieldInfo.Name;

                //Check attributes
                JsonSerializeAttribute attribute = fieldInfo.GetCustomAttribute<JsonSerializeAttribute>();
                if (attribute != null)
                {
                    if (!string.IsNullOrEmpty(attribute.Name)) name = attribute.Name;
                }

                //Set field value
                if (jsonCollection.Contains(name) && !fieldInfo.IsInitOnly)
                    fieldInfo.SetValue(instance, GetValue(fieldInfo.FieldType, jsonCollection[name].Value));

                //Check if field is reference type and is not null.
                else if (!fieldInfo.FieldType.IsValueType && fieldInfo.GetValue(instance) != null)
                {
                    //Get instance
                    object value = fieldInfo.GetValue(instance);
                    Type fieldType = fieldInfo.FieldType;

                    //Check if value implements IDictionary
                    if (fieldType.GetInterface(typeof(IDictionary<,>).Name) != null && value is IDictionary dictionary)
                    {
                        //Prepare
                        Type dictionaryType = fieldType.GetInterface(typeof(IDictionary<,>).FullName);
                        Type keyType = dictionaryType.GetGenericArguments()[0];
                        Type valueType = dictionaryType.GetGenericArguments()[1];
                        MethodInfo addMethod = dictionaryType.GetMethod("Add");
                        JsonObject jsonValue = jsonCollection[name].Value;

                        //Loop through key value pairs
                        if (jsonValue is JsonObjectCollection jsonObjects)
                            foreach (JsonObject jsonKeyValuePair in jsonObjects)
                            {
                                //Check if object is a JSON element collection
                                if (jsonKeyValuePair is JsonElementCollection jsonKeyValuePairCollection)
                                {
                                    //Get key and value from the JSON key value pair
                                    object keyValuePairKey = GetValue(keyType, jsonKeyValuePairCollection["key"].Value);
                                    object keyValuePairValue = GetValue(valueType, jsonKeyValuePairCollection["value"].Value);

                                    //Add
                                    addMethod.Invoke(value, new object[] { keyValuePairKey, keyValuePairValue });
                                }
                            }
                    }

                    //Check if value implements ICollection
                    else if (fieldType.GetInterface(typeof(ICollection<>).Name) != null)
                    {
                        //Prepare
                        Type collectionType = fieldType.GetInterface(typeof(ICollection<>).FullName);
                        Type elementType = collectionType.GetGenericArguments()[0];
                        MethodInfo addMethod = collectionType.GetMethod("Add");
                        JsonObject jsonValue = jsonCollection[name].Value;

                        //Deserialize element
                        if (jsonValue is JsonObjectCollection jsonObjects)
                            foreach (var foo in jsonObjects)
                            {
                                object element = GetValue(elementType, foo);
                                addMethod.Invoke(value, new object[] { element });
                            }
                    }

                    //Deserialize
                    else DeserializeObject(value, fieldType, jsonCollection[name].Value);
                }
            }
        }
        private object GetValue(Type type, object value)
        {
            //Check if number
            if (value is JsonNumber jsonNumber)
                try
                {
                    //Check type
                    switch (Type.GetTypeCode(type))
                    {
                        case TypeCode.Char: return Convert.ToChar(jsonNumber.Value);
                        case TypeCode.SByte: return Convert.ToSByte(jsonNumber.Value);
                        case TypeCode.Byte: return Convert.ToByte(jsonNumber.Value);
                        case TypeCode.Int16: return Convert.ToInt16(jsonNumber.Value);
                        case TypeCode.UInt16: return Convert.ToUInt16(jsonNumber.Value);
                        case TypeCode.Int32: return Convert.ToInt32(jsonNumber.Value);
                        case TypeCode.UInt32: return Convert.ToUInt32(jsonNumber.Value);
                        case TypeCode.Int64: return Convert.ToInt64(jsonNumber.Value);
                        case TypeCode.UInt64: return Convert.ToUInt64(jsonNumber.Value);
                        case TypeCode.Single: return Convert.ToSingle(jsonNumber.Value);
                        case TypeCode.Double: return Convert.ToDouble(jsonNumber.Value);
                        case TypeCode.Decimal: return jsonNumber.Value;
                        default: return null;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            //Check if string
            else if (Type.GetTypeCode(type) == TypeCode.String && value is JsonString jsonString)
                return jsonString.Value;

            //Check if boolean
            else if (Type.GetTypeCode(type) == TypeCode.Boolean && value is JsonBoolean jsonBoolean)
                return jsonBoolean.Value;

            //Check if object
            else if (Type.GetTypeCode(type) == TypeCode.Object && value is JsonObject jsonObject)
            {
                //Check if null
                if (jsonObject is JsonNull) return null;

                //Check if type is unspecified and the value is a value container.
                if (type == typeof(object) && value is JsonValueContainer jsonValueContainer)
                    return jsonValueContainer.Value;

                //Check if type is unspecified
                else if (type == typeof(object))
                    return value;

                //Check if array
                else if (type.IsArray && value is JsonObjectCollection jsonArray)
                {
                    //Create array
                    Array array = Array.CreateInstance(type.GetElementType(), jsonArray.Count);

                    //Deserialize
                    for (int i = 0; i < jsonArray.Count; i++)
                        array.SetValue(Deserialize(jsonArray[i], type.GetElementType()), i);

                    //Return
                    return array;
                }

                //Check if implements IDictionary<,>
                else if (type.GetInterface(typeof(IDictionary<,>).Name) != null && value is JsonObjectCollection jsonDictionary)
                {
                    //Get interface
                    Type dictionaryType = type.GetInterface(typeof(IDictionary<,>).Name);
                    Type keyType = type.GetGenericArguments()[0];
                    Type valueType = type.GetGenericArguments()[1];

                    //Create dictionary
                    object dictionary = Activator.CreateInstance(type);

                    //Deserialize
                    MethodInfo addMethod = dictionaryType.GetMethod("Add");
                    for (int i = 0; i < jsonDictionary.Count; i++)
                        addMethod.Invoke(dictionary, new object[]
                        {
                            Deserialize(((JsonElementCollection)jsonDictionary[i])["Key"].Value as JsonObject, keyType),
                            Deserialize(((JsonElementCollection)jsonDictionary[i])["Value"].Value as JsonObject, valueType),
                        });

                    //Return
                    return dictionary;
                }

                //Check if implements ICollection<>
                else if (type.GetInterface(typeof(ICollection<>).Name) != null && value is JsonObjectCollection jsonCollection)
                {
                    //Get interface
                    Type collectionType = type.GetInterface(typeof(ICollection<>).Name);
                    Type elementType = type.GetGenericArguments()[0];

                    //Create collection
                    object collection = Activator.CreateInstance(type);

                    //Deserialize
                    MethodInfo addMethod = collectionType.GetMethod("Add");
                    for (int i = 0; i < jsonCollection.Count; i++)
                        addMethod.Invoke(collection, new object[] { Deserialize(jsonCollection[i], elementType) });

                    //Return
                    return collection;
                }

                //Deserialize
                else
                {
                    //Create instance
                    object instance = null;
                    try { instance = Activator.CreateInstance(type); }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    //Deserialize object
                    try { DeserializeObject(instance, type, jsonObject); }
                    catch(Exception ex)
                    {
                        throw ex;
                    }

                    //Return instance
                    return instance;
                }
            }

            //Return
            return null;
        }
    }
}
