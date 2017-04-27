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
    /// The abstract base for statement objects.
    /// </summary>
    public abstract class StatementBase : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementBase"/> class.
        /// </summary>
        internal StatementBase() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementBase"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        internal StatementBase(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementBase"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        internal StatementBase(JObject jobj)
        {
            if (jobj["actor"] != null)
            {
                if (jobj["actor"]["objectType"] != null && (string)jobj["actor"]["objectType"] == Group.TypeName)
                {
                    Actor = (Group)jobj.Value<JObject>("actor");
                }
                else
                {
                    Actor = (Agent)jobj.Value<JObject>("actor");
                }
            }

            if (jobj["verb"] != null)
            {
                Verb = (Verb)jobj.Value<JObject>("verb");
            }

            if (jobj["object"] != null)
            {
                if (jobj["object"]["objectType"] != null)
                {
                    if ((string)jobj["object"]["objectType"] == Group.TypeName)
                    {
                        Target = (Group)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Agent.TypeName)
                    {
                        Target = (Agent)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == Activity.TypeName)
                    {
                        Target = (Activity)jobj.Value<JObject>("object");
                    }
                    else if ((string)jobj["object"]["objectType"] == StatementRef.TypeName)
                    {
                        Target = (StatementRef)jobj.Value<JObject>("object");
                    }
                }
                else
                {
                    Target = (Activity)jobj.Value<JObject>("object");
                }
            }

            if (jobj["result"] != null)
            {
                Result = (Result)jobj.Value<JObject>("result");
            }

            if (jobj["context"] != null)
            {
                Context = (Context)jobj.Value<JObject>("context");
            }

            if (jobj["timestamp"] != null)
            {
                Timestamp = jobj.Value<DateTime>("timestamp");
            }
        }

		/// <summary>
		/// Gets or sets the actor related to this statement.
		/// </summary>
		/// <value>The actor.</value>
		public Agent Actor { get; set; }

		/// <summary>
		/// Gets or sets the verb related to this statement.
		/// </summary>
		/// <value>The verb.</value>
		public Verb Verb { get; set; }

		/// <summary>
		/// Gets or sets the target related to this statement.
		/// </summary>
		/// <value>The target.</value>
		public IStatementTarget Target { get; set; }

		/// <summary>
		/// Gets or sets the result related to this statement.
		/// </summary>
		/// <value>The result.</value>
		public Result Result { get; set; }

		/// <summary>
		/// Gets or sets the context of this statement.
		/// </summary>
		/// <value>The context.</value>
		public Context Context { get; set; }

		/// <summary>
		/// Gets or sets the timestamp of this statement.
		/// </summary>
		/// <value>The timestamp.</value>
		public DateTime? Timestamp { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Actor != null)
            {
                result.Add("actor", Actor.ToJObject(version));
            }

            if (Verb != null)
            {
                result.Add("verb", Verb.ToJObject(version));
            }

            if (Target != null)
            {
                result.Add("object", Target.ToJObject(version));
            }

            if (Result != null)
            {
                result.Add("result", this.Result.ToJObject(version));
            }

            if (Context != null)
            {
                result.Add("context", Context.ToJObject(version));
            }

            if (Timestamp != null)
            {
                result.Add("timestamp", Timestamp.Value.ToString(TimeFormat.Default));
            }

            return result;
        }
    }
}
