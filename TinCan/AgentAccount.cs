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
    /// An account associated with an agent.
    /// </summary>
    public class AgentAccount : JsonModel
    {
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
                HomePage = new Uri(jobj.Value<string>("homePage"));
            }

            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.AgentAccount"/> class.
        /// </summary>
        /// <param name="homePage">Home page.</param>
        /// <param name="name">Name.</param>
        public AgentAccount(Uri homePage, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (!homePage.IsAbsoluteUri)
            {
                throw new ArgumentException("Homepage URI must be absolute");
            }

            HomePage = homePage ?? throw new ArgumentNullException(nameof(homePage));
            Name = name;
        }

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>The home page.</value>
        public Uri HomePage { get; set; }

        /// <summary>
        /// Gets or sets the name of the Agent's account.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

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
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.AgentAccount"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.AgentAccount"/>.</returns>
        public override string ToString()
        {
            return string.Format("[AgentAccount: HomePage={0}, Name={1}]", 
                                 HomePage, Name);
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
