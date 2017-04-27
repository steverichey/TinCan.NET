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
    /// Context.
    /// </summary>
    public class Context : JsonModel
    {
        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        /// <value>The registration.</value>
        public Guid? Registration { get; set; }

        /// <summary>
        /// Gets or sets the instructor.
        /// </summary>
        /// <value>The instructor.</value>
        public Agent Instructor { get; set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        /// <value>The team.</value>
        public Agent Team { get; set; }

        /// <summary>
        /// Gets or sets the context activities.
        /// </summary>
        /// <value>The context activities.</value>
        public ContextActivities ContextActivities { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        /// <value>The revision.</value>
        public string Revision { get; set; }

        /// <summary>
        /// Gets or sets the platform.
        /// </summary>
        /// <value>The platform.</value>
        public string Platform { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the statement.
        /// </summary>
        /// <value>The statement.</value>
        public StatementRef Statement { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public Extensions Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Context"/> class.
        /// </summary>
        public Context() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Context"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Context(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Context"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Context(JObject jobj)
        {
            if (jobj["registration"] != null)
            {
                Registration = new Guid(jobj.Value<String>("registration"));
            }

            if (jobj["instructor"] != null)
            {
                // TODO: can be Group?
                Instructor = (Agent)jobj.Value<JObject>("instructor");
            }

            if (jobj["team"] != null)
            {
                // TODO: can be Group?
                Team = (Agent)jobj.Value<JObject>("team");
            }

            if (jobj["contextActivities"] != null)
            {
                ContextActivities = (ContextActivities)jobj.Value<JObject>("contextActivities");
            }

            if (jobj["revision"] != null)
            {
                Revision = jobj.Value<String>("revision");
            }

            if (jobj["platform"] != null)
            {
                Platform = jobj.Value<String>("platform");
            }

            if (jobj["language"] != null)
            {
                Language = jobj.Value<String>("language");
            }

            if (jobj["statement"] != null)
            {
                Statement = (StatementRef)jobj.Value<JObject>("statement");
            }

            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) {
            JObject result = new JObject();

            if (Registration != null)
            {
                result.Add("registration", Registration.ToString());
            }

            if (Instructor != null)
            {
                result.Add("instructor", Instructor.ToJObject(version));
            }

            if (Team != null)
            {
                result.Add("team", Team.ToJObject(version));
            }

            if (ContextActivities != null)
            {
                result.Add("contextActivities", ContextActivities.ToJObject(version));
            }

            if (Revision != null)
            {
                result.Add("revision", Revision);
            }

            if (Platform != null)
            {
                result.Add("platform", Platform);
            }

            if (Language != null)
            {
                result.Add("language", Language);
            }

            if (Statement != null)
            {
                result.Add("statement", Statement.ToJObject(version));
            }

            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Context(JObject jobj)
        {
            return new Context(jobj);
        }
    }
}
