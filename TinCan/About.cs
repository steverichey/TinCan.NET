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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// An object containing information about this LRS.
    /// </summary>
    public class About : JsonModel
    {
        /// <summary>
        /// Gets or sets the xAPI versions this LRS supports.
        /// </summary>
        /// <value>The supported versions.</value>
        public List<TCAPIVersion> Version { get; set; }

        /// <summary>
        /// A map of other properties as needed.
        /// </summary>
        /// <value>Other needed properties.</value>
        public Extensions Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.About"/> class.
        /// </summary>
        /// <param name="str">String of JSON content for an About resource.</param>
        public About(string str) : this(new StringOfJSON(str)) {}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TinCan.About"/> class.
		/// </summary>
		/// <param name="json">String of JSON content for an About resource.</param>
		public About(StringOfJSON json) : this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.About"/> class.
        /// </summary>
        /// <param name="jobj">JSON content for an About resource.</param>
        public About(JObject jobj)
        {
            if (jobj["version"] != null)
            {
                Version = new List<TCAPIVersion>();

                foreach (string item in jobj.Value<JArray>("version"))
                {
                    Version.Add((TCAPIVersion)item);
                }
            }

            if (jobj["extensions"] != null)
            {
                Extensions = new Extensions(jobj.Value<JObject>("extensions"));
            }
        }

        /// <summary>
        /// Convert this to a JObject.
        /// </summary>
        /// <returns>The About resource as a JObject.</returns>
        /// <param name="version">Version to specify when creating extension properties.</param>
        public override JObject ToJObject(TCAPIVersion version) {
            JObject result = new JObject();

            if (Version != null)
            {
                var versions = new JArray();

                foreach (var v in Version) 
                {
                    versions.Add(v.ToString());
                }

                result.Add("version", versions);
            }

            if (Extensions != null && !Extensions.IsEmpty)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }
    }
}
