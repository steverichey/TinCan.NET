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
    /// Agent account resource.
    /// </summary>
    public class AgentAccount : JsonModel
    {
		/// <summary>
		/// Gets or sets the home page.
		/// TODO: check to make sure is absolute?
		/// </summary>
		/// <value>The home page.</value>
		public Uri HomePage { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.AgentAccount"/> class.
        /// </summary>
        public AgentAccount() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.AgentAccount"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public AgentAccount(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.AgentAccount"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public AgentAccount(JObject jobj)
        {
            if (jobj["homePage"] != null)
            {
                HomePage = new Uri(jobj.Value<String>("homePage"));
            }

            if (jobj["name"] != null)
            {
                Name = jobj.Value<String>("name");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.AgentAccount"/> class.
        /// </summary>
        /// <param name="homePage">Home page.</param>
        /// <param name="name">Name.</param>
        public AgentAccount(Uri homePage, String name)
        {
            HomePage = homePage;
            Name = name;
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            JObject result = new JObject();

            if (HomePage != null)
            {
                result.Add("homePage", HomePage.ToString());
            }

            if (Name != null)
            {
                result.Add("name", Name);
            }

            return result;
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator AgentAccount(JObject jobj)
        {
            return new AgentAccount(jobj);
        }
    }
}
