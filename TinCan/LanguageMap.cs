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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// Language map.
    /// </summary>
    public class LanguageMap : JsonModel
    {
        Dictionary<string, string> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.LanguageMap"/> class.
        /// </summary>
        public LanguageMap() 
        {
            map = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.LanguageMap"/> class.
        /// </summary>
        /// <param name="map">Map.</param>
        public LanguageMap(Dictionary<string, string> map)
        {
            this.map = map;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.LanguageMap"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public LanguageMap(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.LanguageMap"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public LanguageMap(JObject jobj) : this()
        {
            foreach (var entry in jobj) 
            {
                map.Add(entry.Key, (string)entry.Value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:TinCan.LanguageMap"/> is empty.
        /// </summary>
        /// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty
		{
            get
            {
                return map.Count <= 0;
            }
		}

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            foreach (var entry in map)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }

        /// <summary>
        /// Add the specified language and value to this map.
        /// </summary>]
        /// <param name="lang">Language related to this value.</param>
        /// <param name="value">Value related to the given language.</param>
        public void Add(string lang, string value)
        {
            map.Add(lang, value);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LanguageMap"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LanguageMap"/>.</returns>
        public override string ToString()
        {
            return string.Format("[LanguageMap: IsEmpty={0}, Map={1}]", IsEmpty, map);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator LanguageMap(JObject jobj)
        {
            return new LanguageMap(jobj);
        }
    }
}
