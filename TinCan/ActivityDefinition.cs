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
    /// Activity definition object.
    /// </summary>
    public class ActivityDefinition : JsonModel
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public Uri Type { get; set; }

        /// <summary>
        /// Gets or sets the more info.
        /// </summary>
        /// <value>The more info.</value>
        public Uri MoreInfo { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public LanguageMap Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public LanguageMap Description { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public Extensions Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        public ActivityDefinition() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public ActivityDefinition(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ActivityDefinition"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public ActivityDefinition(JObject jobj)
        {
            if (jobj["type"] != null)
            {
                Type = new Uri(jobj.Value<String>("type"));
            }

            if (jobj["moreInfo"] != null)
            {
                MoreInfo = new Uri(jobj.Value<String>("moreInfo"));
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
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) 
        {
            JObject result = new JObject();

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
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator ActivityDefinition(JObject jobj)
        {
            return new ActivityDefinition(jobj);
        }
    }
}
