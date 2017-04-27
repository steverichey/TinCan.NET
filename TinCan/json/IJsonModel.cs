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

using Newtonsoft.Json.Linq;

namespace TinCan.Json
{
    /// <summary>
    /// Defines an object that can be converted to a JObject or JSON string.
    /// </summary>
    public interface IJsonModel
    {
        /// <summary>
        /// Convert this object to a JObject.
        /// </summary>
        /// <returns>The new JObject.</returns>
        /// <param name="version">Version of the API to use.</param>
        JObject ToJObject(TCAPIVersion version);

		/// <summary>
		/// Convert this object to a JObject.
		/// </summary>
		/// <returns>The new JObject.</returns>
		JObject ToJObject();

        /// <summary>
        /// Convert this object to a JSON string.
        /// </summary>
        /// <returns>The new JSON string.</returns>
        /// <param name="version">Version of the API to use.</param>
        /// <param name="pretty">If set to <c>true</c>, string will be formatted with newlines and tabs.</param>
        string ToJSON(TCAPIVersion version, bool pretty = false);

		/// <summary>
		/// Convert this object to a JSON string.
		/// </summary>
		/// <returns>The new JSON string.</returns>
		/// <param name="pretty">If set to <c>true</c>, string will be formatted with newlines and tabs.</param>
		string ToJSON(bool pretty = false);
    }
}
