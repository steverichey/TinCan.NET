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
    /// Verb.
    /// </summary>
    public class Verb : JsonModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>The display.</value>
        public LanguageMap Display { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        public Verb() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Verb(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Verb(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                Id = new Uri(jobj.Value<String>("id"));
            }

            if (jobj["display"] != null)
            {
                Display = (LanguageMap)jobj.Value<JObject>("display");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        /// <param name="uri">URI.</param>
        public Verb(Uri uri)
        {
            Id = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        /// <param name="uri">URI.</param>
        /// <param name="defaultLanguage">Default language.</param>
        /// <param name="defaultTerm">Default term.</param>
        public Verb(Uri uri, string defaultLanguage, string defaultTerm)
        {
            Id = uri;

            Display = new LanguageMap();
            Display.Add(defaultLanguage, defaultTerm);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Verb"/> class.
        /// </summary>
        /// <param name="str">String.</param>
        public Verb(string str)
        {
            Id = new Uri(str);
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) {
            JObject result = new JObject();

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }

            if (Display != null && ! Display.IsEmpty)
            {
                result.Add("display", Display.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator Verb(JObject jobj)
        {
            return new Verb(jobj);
        }

        /// <summary>
        /// Internal method to generate an xAPI verb from a string name.
        /// Defaults to "en-US".
        /// </summary>
        /// <returns>The new xAPI verb.</returns>
        /// <param name="name">String name of the xAPI verb. Will be converted to lowercase.</param>
        internal static Verb FromName(string name)
        {
            var lowercaseName = name.ToLower();
            return new Verb(new Uri(string.Format("http://adlnet.gov/expapi/verbs/{0}", lowercaseName)), "en-US", lowercaseName);
        }

        public override string ToString()
        {
            return string.Format("[Verb: Id={0}, Display={1}]", Id, Display);
        }

        public static readonly Verb Abandoned = FromName(nameof(Abandoned));
        public static readonly Verb Answered = FromName(nameof(Answered));
        public static readonly Verb Asked = FromName(nameof(Asked));
        public static readonly Verb Attempted = FromName(nameof(Attempted));
        public static readonly Verb Attended = FromName(nameof(Attended));
		public static readonly Verb Commented = FromName(nameof(Commented));
        public static readonly Verb Completed = FromName(nameof(Completed));
        public static readonly Verb Exited = FromName(nameof(Exited));
		public static readonly Verb Experienced = FromName(nameof(Experienced));
		public static readonly Verb Failed = FromName(nameof(Failed));
		public static readonly Verb Imported = FromName(nameof(Imported));
		public static readonly Verb Initialized = FromName(nameof(Initialized));
		public static readonly Verb Interacted = FromName(nameof(Interacted));
		public static readonly Verb Launched = FromName(nameof(Launched));
		public static readonly Verb LoggedIn = FromName(nameof(LoggedIn));
		public static readonly Verb LoggedOut = FromName(nameof(LoggedOut));
		public static readonly Verb Mastered = FromName(nameof(Mastered));
		public static readonly Verb Passed = FromName(nameof(Passed));
		public static readonly Verb Preferred = FromName(nameof(Preferred));
		public static readonly Verb Progressed = FromName(nameof(Progressed));
		public static readonly Verb Registered = FromName(nameof(Registered));
		public static readonly Verb Responded = FromName(nameof(Responded));
		public static readonly Verb Resumed = FromName(nameof(Resumed));
		public static readonly Verb Satisfied = FromName(nameof(Satisfied));
		public static readonly Verb Scored = FromName(nameof(Scored));
		public static readonly Verb Shared = FromName(nameof(Shared));
        public static readonly Verb Suspended = FromName(nameof(Suspended));
		public static readonly Verb Terminated = FromName(nameof(Terminated));
		public static readonly Verb Voided = FromName(nameof(Voided));
        public static readonly Verb Waived = FromName(nameof(Waived));
    }
}
