/*
    Copyright 2014 Rustici Software

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/
using System;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// Activity.
    /// </summary>
    public class Activity : JsonModel, IStatementTarget
    {
        /// <summary>
        /// The type of the object.
        /// </summary>
        public static readonly string OBJECT_TYPE = "Activity";

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType 
        { 
            get
            { 
                return OBJECT_TYPE; 
            } 
        }

        string id;

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id
        {
            get 
            { 
                return id; 
            }

            set
            {
                Uri uri = new Uri(value);
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the definition.
        /// </summary>
        /// <value>The definition.</value>
        public ActivityDefinition Definition { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Activity"/> class.
        /// </summary>
        public Activity() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Activity"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Activity(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Activity"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Activity(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                string idFromJSON = jobj.Value<String>("id");
                Uri uri = new Uri(idFromJSON);
                Id = idFromJSON;
            }

            if (jobj["definition"] != null)
            {
                Definition = (ActivityDefinition)jobj.Value<JObject>("definition");
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Id != null)
            {
                result.Add("id", Id);
            }

            if (Definition != null)
            {
                result.Add("definition", Definition.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator Activity(JObject jobj)
        {
            return new Activity(jobj);
        }
    }
}
