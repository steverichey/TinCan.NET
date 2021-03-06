﻿/*
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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
	/// <summary>
	/// A map of any other domain-specific context relevant to a Statement. 
	/// </summary>
	public class Extensions : JsonModel
    {
        Dictionary<Uri, JToken> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Extensions"/> class.
        /// </summary>
        public Extensions()
        {
            map = new Dictionary<Uri, JToken>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Extensions"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Extensions(JObject jobj) : this()
        {
            foreach (var item in jobj)
            {
                map.Add(new Uri(item.Key), item.Value); 
            }
		}

		/// <summary>
        /// Gets a value indicating whether this <see cref="T:TinCan.Extensions"/> is empty.
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
            JObject result = new JObject();

            foreach (var entry in map)
            {
                result.Add(entry.Key.ToString(), entry.Value);
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Extensions"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Extensions"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Extensions: IsEmpty={0}, Map={1}]", 
                                 IsEmpty, map);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Extensions(JObject jobj)
        {
            return new Extensions(jobj);
        }
    }
}
