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
	/// A Group represents a collection of Agents and can be used in most of the same situations an Agent can be used. 
	/// </summary>
	public class Group : Agent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Group"/> class.
        /// </summary>
        public Group() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Group"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Group(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Group"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Group(JObject jobj) : base(jobj)
        {
            if (jobj["member"] != null)
            {
                Members = new List<Agent>();

                foreach (JObject jagent in jobj["member"])
                {
                    Members.Add(new Agent(jagent));
                }
            }
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public override string ObjectType 
        { 
            get 
            { 
                return TypeName; 
            } 
        }

        /// <summary>
        /// Gets the members of this group.
        /// </summary>
        /// <value>The member.</value>
        public List<Agent> Members { get; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            if (Members != null && Members.Count > 0)
            {
                var jmember = new JArray();
                result.Add("member", jmember);

                foreach (var agent in Members)
                {
                    jmember.Add(agent.ToJObject(version));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Group"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Group"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Agent: Name={0}, Mbox={1}, MboxSha1Sum={2}, OpenId={3}, Account={4}, Members={5}]",
								 Name, Mbox, MboxSha1Sum, OpenId, Account, Members);
        }

        /// <summary>
        /// The type of the object.
        /// </summary>
        public static readonly new string TypeName = nameof(Group);

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Group(JObject jobj)
        {
            return new Group(jobj);
        }
    }
}
