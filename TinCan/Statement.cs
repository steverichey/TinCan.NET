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
	/// The basic communication mechanism of the Experience API.
	/// </summary>
	public class Statement : StatementBase
    {
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
        public Statement(JObject jobj) : base(jobj) 
        {
            if (jobj["id"] != null)
            {
                Id = new Guid(jobj.Value<string>("id"));
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
                Version = (TCAPIVersion)jobj.Value<string>("version");
            }

            // handle SubStatement as target which isn't provided by StatementBase
            // because SubStatements are not allowed to nest
            if (jobj["object"] != null && (string)jobj["object"]["objectType"] == SubStatement.TypeName)
            {
                Target = (SubStatement)jobj.Value<JObject>("object");
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier of this statement.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; set; }

        /// <summary>
        /// Gets or sets the time this statement was stored.
        /// </summary>
        /// <value>The stored time.</value>
        public DateTime? Stored { get; set; }

		/// <summary>
		/// Gets or sets information about whom or what has asserted that this Statement is true.
		/// </summary>
		/// <value>The authority.</value>
		public Agent Authority { get; set; }

        /// <summary>
        /// Gets or sets the xAPI version.
        /// </summary>
        /// <value>The version.</value>
        public TCAPIVersion Version { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }

            if (Stored != null)
            {
                result.Add("stored", Stored.Value.ToString(TimeFormat.Default));
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
        /// Stamp this instance with a unique identifier and timestamp.
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

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Statement"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Statement"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Statement: Actor={0}, Verb={1}, Target={2}, Result={3}, Context={4}, Timestamp={5}, Id={6}, Stored={7}, Authority={8}, Version={9}]", 
                                 Actor, Verb, Target, Result, Context, Timestamp, Id, Stored, Authority, Version);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Statement(JObject jobj)
        {
            return new Statement(jobj);
        }
    }
}
