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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TinCan.Json
{
    /// <summary>
    /// Base implementation of the JSON model interface.
    /// </summary>
    public abstract class JsonModel : IJsonModel
    {
		/// <summary>
        /// Implementing classes should use this to convert data to JObject format.
        /// Other methods in this abstract class will use this implementation.
        /// </summary>
        /// <returns>The implementing object as a JObject.</returns>
        /// <param name="version">Version of the API to use.</param>
		public abstract JObject ToJObject(TCAPIVersion version);

		/// <inheritdoc />
		public JObject ToJObject()
        {
            return ToJObject(TCAPIVersion.Latest);
        }

		/// <inheritdoc />
		public string ToJSON(TCAPIVersion version, bool pretty = false)
        {
            var formatting = Formatting.None;

            if (pretty)
            {
                formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(ToJObject(version), formatting);
        }

		/// <inheritdoc />
		public string ToJSON(bool pretty = false)
        {
            return ToJSON(TCAPIVersion.Latest, pretty);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.JsonModel"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.JsonModel"/>.</returns>
        public override string ToString()
        {
            return ToJSON();
        }
    }
}
