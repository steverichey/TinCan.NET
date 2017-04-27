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
                Id = new Uri(jobj.Value<string>("id"));
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

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Verb"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Verb"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Verb: Id={0}, Display={1}]", Id, Display);
        }

		/// <summary>
		/// Indicates the activity provider has determined that the session was abnormally terminated either by an actor or due to a system failure.
		/// </summary>
		public static readonly Verb Abandoned = FromName(nameof(Abandoned));

		/// <summary>
		/// Indicates the actor replied to a question, where the object is generally an activity representing the question. The text of the answer will often be included in the response inside result.
		/// </summary>
		public static readonly Verb Answered = FromName(nameof(Answered));

		/// <summary>
		/// Indicates an inquiry by an actor with the expectation of a response or answer to a question.
		/// </summary>
		public static readonly Verb Asked = FromName(nameof(Asked));

		/// <summary>
		/// Indicates the actor made an effort to access the object. An attempt statement without additional activities could be considered incomplete in some cases.
		/// </summary>
		public static readonly Verb Attempted = FromName(nameof(Attempted));

		/// <summary>
		/// Indicates the actor was present at a virtual or physical event or activity.
		/// </summary>
		public static readonly Verb Attended = FromName(nameof(Attended));

		/// <summary>
		/// Indicates the actor provided digital or written annotations on or about an object.
		/// </summary>
		public static readonly Verb Commented = FromName(nameof(Commented));

		/// <summary>
		/// Indicates the actor finished or concluded the activity normally.
		/// </summary>
		public static readonly Verb Completed = FromName(nameof(Completed));

		/// <summary>
		/// Indicates the actor intentionally departed from the activity or object.
		/// </summary>
		public static readonly Verb Exited = FromName(nameof(Exited));

		/// <summary>
		/// Indicates the actor only encountered the object, and is applicable in situations where a specific achievement or completion is not required.
		/// </summary>
		public static readonly Verb Experienced = FromName(nameof(Experienced));

		/// <summary>
		/// Indicates the actor did not successfully pass an activity to a level of predetermined satisfaction.
		/// </summary>
		public static readonly Verb Failed = FromName(nameof(Failed));

		/// <summary>
		/// Indicates the actor introduced an object into a physical or virtual location.
		/// </summary>
		public static readonly Verb Imported = FromName(nameof(Imported));

		/// <summary>
		/// Indicates the activity provider has determined that the actor successfully started an activity.
		/// </summary>
		public static readonly Verb Initialized = FromName(nameof(Initialized));

		/// <summary>
		/// Indicates the actor engaged with a physical or virtual object.
		/// </summary>
		public static readonly Verb Interacted = FromName(nameof(Interacted));

		/// <summary>
		/// Indicates the actor attempted to start an activity.
		/// </summary>
		public static readonly Verb Launched = FromName(nameof(Launched));

		/// <summary>
		/// Indicates the actor gained access to a system or service by identifying and authenticating with the credentials provided by the actor.
		/// </summary>
		public static readonly Verb LoggedIn = FromName(nameof(LoggedIn));

		/// <summary>
		/// Indicates the actor either lost or discontinued access to a system or service.
		/// </summary>
		public static readonly Verb LoggedOut = FromName(nameof(LoggedOut));

		/// <summary>
		/// Indicates the highest level of comprehension or competence the actor performed in an activity.
		/// </summary>
		public static readonly Verb Mastered = FromName(nameof(Mastered));

		/// <summary>
		/// Indicates the actor successfully passed an activity to a level of predetermined satisfaction.
		/// </summary>
		public static readonly Verb Passed = FromName(nameof(Passed));

		/// <summary>
		/// Indicates the selected choices, favored options or settings of an actor in relation to an object or activity.
		/// </summary>
		public static readonly Verb Preferred = FromName(nameof(Preferred));

		/// <summary>
		/// Indicates a value of how much of an actor has advanced or moved through an activity.
		/// </summary>
		public static readonly Verb Progressed = FromName(nameof(Progressed));

		/// <summary>
		/// Indicates the actor is officially enrolled or inducted in an activity.
		/// </summary>
		public static readonly Verb Registered = FromName(nameof(Registered));

		/// <summary>
		/// Indicates an actor reacted or replied to an object.
		/// </summary>
		public static readonly Verb Responded = FromName(nameof(Responded));

		/// <summary>
		/// Indicates the application has determined that the actor continued or reopened a suspended attempt on an activity.
		/// </summary>
		public static readonly Verb Resumed = FromName(nameof(Resumed));

		/// <summary>
		/// Indicates that the authority or activity provider determined the actor has fulfilled the criteria of the object or activity.
		/// </summary>
		public static readonly Verb Satisfied = FromName(nameof(Satisfied));

		/// <summary>
		/// Indicates a numerical value related to an actor's performance on an activity.
		/// </summary>
		public static readonly Verb Scored = FromName(nameof(Scored));

		/// <summary>
		/// Indicates the actor's intent to openly provide access to an object of common interest to other actors or groups.
		/// </summary>
		public static readonly Verb Shared = FromName(nameof(Shared));

		/// <summary>
		/// Indicates the status of a temporarily halted activity when an actor's intent is returning to the or object activity at a later time.
		/// </summary>
		public static readonly Verb Suspended = FromName(nameof(Suspended));

		/// <summary>
		/// Indicates that the actor successfully ended an activity.
		/// </summary>
		public static readonly Verb Terminated = FromName(nameof(Terminated));

		/// <summary>
		/// A special reserved verb used by a LRS or application to mark a statement as invalid.
        /// See the xAPI specification for details on Voided statements.
		/// </summary>
		public static readonly Verb Voided = FromName(nameof(Voided));

		/// <summary>
        /// Indicates that the learning activity requirements were met by means other than completing the activity.
        /// A waived statement is used to indicate that the activity may be skipped by the actor.
		/// </summary>
		public static readonly Verb Waived = FromName(nameof(Waived));
    }
}
