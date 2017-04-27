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
        /// <summary>
        /// The map.
        /// </summary>
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
        /// Ises the empty.
        /// </summary>
        /// <returns><c>true</c>, if empty was ised, <c>false</c> otherwise.</returns>
		public bool IsEmpty
		{
            get
            {
                return map.Count <= 0;
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

            foreach (var entry in map)
            {
                result.Add(entry.Key, entry.Value);
            }

            return result;
        }

        /// <summary>
        /// Add the specified lang and value.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="lang">Lang.</param>
        /// <param name="value">Value.</param>
        public void Add(string lang, string value)
        {
            map.Add(lang, value);
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator LanguageMap(JObject jobj)
        {
            return new LanguageMap(jobj);
        }
    }
}
