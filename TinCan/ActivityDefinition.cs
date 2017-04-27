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
    /// Object defining information about an activity.
    /// </summary>
    public class ActivityDefinition : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        public ActivityDefinition() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        /// <param name="json">String of JSON describing the object.</param>
        public ActivityDefinition(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        /// <param name="jobj">JSON object describing the object.</param>
        public ActivityDefinition(JObject jobj)
        {
            if (jobj["type"] != null)
            {
                Type = new Uri(jobj.Value<string>("type"));
            }

            if (jobj["moreInfo"] != null)
            {
                MoreInfo = new Uri(jobj.Value<string>("moreInfo"));
            }

            if (jobj["name"] != null)
            {
                Name = (LanguageMap)jobj.Value<JObject>("name");
            }

            if (jobj["description"] != null)
            {
                Description = (LanguageMap)jobj.Value<JObject>("description");
            }

            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
        }

        /// <summary>
        /// Gets or sets the object type.
        /// </summary>
        /// <value>The type of the object.</value>
        public Uri Type { get; set; }

        /// <summary>
        /// Gets or sets the URI for accessing more info.
        /// </summary>
        /// <value>The more info endpoint.</value>
        public Uri MoreInfo { get; set; }

        /// <summary>
        /// Gets or sets the language map name of this activity.
        /// </summary>
        /// <value>The name.</value>
        public LanguageMap Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public LanguageMap Description { get; set; }

        /// <summary>
        /// Gets or sets the additional data associated with the activity.
        /// </summary>
        /// <value>The additional data.</value>
        public Extensions Extensions { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version) 
        {
            var result = new JObject();

            if (Type != null)
            {
                result.Add("type", Type.ToString());
            }

            if (MoreInfo != null)
            {
                result.Add("moreInfo", MoreInfo.ToString());
            }

            if (Name != null && !Name.IsEmpty)
            {
                result.Add("name", Name.ToJObject(version));
            }

            if (Description != null && !Description.IsEmpty)
            {
                result.Add("description", Description.ToJObject(version));
            }

            if (Extensions != null && ! Extensions.IsEmpty)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.ActivityDefinition"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.ActivityDefinition"/>.</returns>
        public override string ToString()
        {
            return string.Format("[ActivityDefinition: Type={0}, MoreInfo={1}, Name={2}, Description={3}, Extensions={4}]", 
                                 Type, MoreInfo, Name, Description, Extensions);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator ActivityDefinition(JObject jobj)
        {
            return new ActivityDefinition(jobj);
        }
    }
}
