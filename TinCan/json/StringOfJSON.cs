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

namespace TinCan.Json
{
    /// <summary>
    /// An object that stores a string that can be parsed to a JObject.
    /// </summary>
    public class StringOfJSON : JsonModel
    {
        readonly string source;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Json.StringOfJSON"/> class.
        /// </summary>
        /// <param name="json">String JSON content.</param>
        public StringOfJSON(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException(nameof(json));
            }

            source = json;
        }

        /// <summary>
        /// Parse this string to a new JObject.
        /// </summary>
        /// <returns>The string as a JObject.</returns>
        /// <param name="version">Version of the API to use; currently unused.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            return JObject.Parse(source);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.StringOfJSON"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.StringOfJSON"/>.</returns>
        public override string ToString()
        {
            return source;
        }
    }
}
