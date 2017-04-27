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
    /// Statement.
    /// </summary>
    public class Statement : StatementBase
    {
		/// <summary>
		/// The ISOD ate time format.
		/// TODO: put in common location
		/// </summary>
		const String ISODateTimeFormat = "o";

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the stored.
        /// </summary>
        /// <value>The stored.</value>
        public DateTime? Stored { get; set; }

        /// <summary>
        /// Gets or sets the authority.
        /// </summary>
        /// <value>The authority.</value>
        public Agent Authority { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public TCAPIVersion Version { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Statement"/> class.
        /// </summary>
        public Statement() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Statement"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Statement(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Statement"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Statement(JObject jobj) : base(jobj) {
            if (jobj["id"] != null)
            {
                Id = new Guid(jobj.Value<String>("id"));
            }

            if (jobj["stored"] != null)
            {
                Stored = jobj.Value<DateTime>("stored");
            }

            if (jobj["authority"] != null)
            {
                Authority = (Agent)jobj.Value<JObject>("authority");
            }

            if (jobj["version"] != null)
            {
                Version = (TCAPIVersion)jobj.Value<String>("version");
            }

            // handle SubStatement as target which isn't provided by StatementBase
            // because SubStatements are not allowed to nest
            if (jobj["object"] != null && (String)jobj["object"]["objectType"] == SubStatement.OBJECT_TYPE)
            {
                Target = (SubStatement)jobj.Value<JObject>("object");
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }

            if (Stored != null)
            {
                result.Add("stored", Stored.Value.ToString(ISODateTimeFormat));
            }

            if (Authority != null)
            {
                result.Add("authority", Authority.ToJObject(version));
            }

            if (version != null)
            {
                result.Add("version", version.ToString());
            }

            return result;
        }

        /// <summary>
        /// Stamp this instance.
        /// </summary>
        public void Stamp()
        {
            if (Id == null)
            {
                Id = Guid.NewGuid();
            }

            if (Timestamp == null)
            {
                Timestamp = DateTime.UtcNow;
            }
        }
    }
}
