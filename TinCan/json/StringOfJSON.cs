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
    /// String of json.
    /// </summary>
    public class StringOfJSON
    {
        readonly string source;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Json.StringOfJSON"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public StringOfJSON(String json)
        {
            source = json;
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        public JObject ToJObject()
        {
            if (source == null)
            {
                return null;
            }

            return JObject.Parse(source);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.StringOfJSON"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Json.StringOfJSON"/>.</returns>
        public override String ToString()
        {
            return source;
        }
    }
}
