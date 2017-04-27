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
    /// Group.
    /// </summary>
    public class Group : Agent
    {
        /// <summary>
        /// The type of the object.
        /// </summary>
        public static readonly new String OBJECT_TYPE = "Group";

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public override String ObjectType 
        { 
            get 
            { 
                return OBJECT_TYPE; 
            } 
        }

        /// <summary>
        /// Gets or sets the member.
        /// </summary>
        /// <value>The member.</value>
        public List<Agent> member { get; set; }

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
                member = new List<Agent>();

                foreach (JObject jagent in jobj["member"])
                {
                    member.Add(new Agent(jagent));
                }
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            JObject result = base.ToJObject(version);

            if (member != null && member.Count > 0)
            {
                var jmember = new JArray();
                result.Add("member", jmember);

                foreach (var agent in member)
                {
                    jmember.Add(agent.ToJObject(version));
                }
            }

            return result;
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator Group(JObject jobj)
        {
            return new Group(jobj);
        }
    }
}
