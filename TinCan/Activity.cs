/*
    Copyright 2014-2017 Rustici Software

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
    /// The base xAPI object.
    /// </summary>
    public class Activity : JsonModel, IStatementTarget
    {
        string id;

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
        /// <param name="jobj">The JSON object describing the activity.</param>
        public Activity(JObject jobj)
        {
            if (jobj == null)
            {
                throw new ArgumentNullException(nameof(jobj));
            }

            if (jobj["id"] != null)
            {
                var idFromJSON = jobj.Value<string>("id");
                Id = idFromJSON;
            }

            if (jobj["definition"] != null)
            {
                Definition = (ActivityDefinition)jobj.Value<JObject>("definition");
            }
        }

		/// <inheritdoc />
		public string ObjectType
		{
			get
			{
				return TypeName;
			}
		}

        /// <summary>
        /// Gets or sets the activity identifier.
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
                var uri = new Uri(value);
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the activity definition.
        /// </summary>
        /// <value>The definition.</value>
        public ActivityDefinition Definition { get; set; }

        /// <inheritdoc />
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
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Activity"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Activity"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Activity: ObjectType={0}, Id={1}, Definition={2}]", ObjectType, Id, Definition);
        }

		/// <summary>
		/// The name of this object type.
		/// </summary>
		public static string TypeName = nameof(Activity);

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Activity(JObject jobj)
        {
            return new Activity(jobj);
        }
    }
}
