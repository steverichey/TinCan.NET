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

using System;
using System.Xml;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
	/// <summary>
	/// An optional property that represents a measured outcome related to the Statement in which it is included.
	/// </summary>
	public class Result : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Result"/> class.
        /// </summary>
        public Result() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Result"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Result(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Result"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Result(JObject jobj)
        {
            if (jobj["completion"] != null)
            {
                Completion = jobj.Value<bool>("completion");
            }

            if (jobj["success"] != null)
            {
                Success = jobj.Value<bool>("success");
            }

            if (jobj["response"] != null)
            {
                Response = jobj.Value<string>("response");
            }

            if (jobj["duration"] != null)
            {
                Duration = XmlConvert.ToTimeSpan(jobj.Value<string>("duration"));
            }

            if (jobj["score"] != null)
            {
                Score = (Score)jobj.Value<JObject>("score");
            }

            if (jobj["extensions"] != null)
            {
                Extensions = (Extensions)jobj.Value<JObject>("extensions");
            }
        }

		/// <summary>
		/// Gets or sets whether or not the Activity was completed.
		/// </summary>
		/// <value>Whether or not the Activity was completed.</value>
		public bool? Completion { get; set; }

		/// <summary>
		/// Gets or sets whether or not the attempt on the Activity was successful.
		/// </summary>
		/// <value>Whether or not the attempt on the Activity was successful.</value>
		public bool? Success { get; set; }

		/// <summary>
		/// Gets or sets a response appropriately formatted for the given Activity.
		/// </summary>
		/// <value>A response appropriately formatted for the given Activity.</value>
		public string Response { get; set; }

		/// <summary>
		/// Gets or sets the period of time over which the Statement occurred.
		/// </summary>
		/// <value>The period of time over which the Statement occurred.</value>
		public TimeSpan? Duration { get; set; }

		/// <summary>
		/// Gets or sets the score of the Agent in relation to the success or quality of the experience.
		/// </summary>
		/// <value>The score of the Agent in relation to the success or quality of the experience.</value>
		public Score Score { get; set; }

		/// <summary>
		/// Gets or sets a map of other properties as needed
		/// </summary>
		/// <value>A map of other properties as needed</value>
		public Extensions Extensions { get; set; }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Completion != null)
            {
                result.Add("completion", Completion);
            }

            if (Success != null)
            {
                result.Add("success", Success);
            }

            if (Response != null)
            {
                result.Add("response", Response);
            }

            if (Duration != null)
            {
                result.Add("duration", XmlConvert.ToString((TimeSpan)Duration));
            }

            if (Score != null)
            {
                result.Add("score", Score.ToJObject(version));
            }

            if (Extensions != null)
            {
                result.Add("extensions", Extensions.ToJObject(version));
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Result"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Result"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Result: Completion={0}, Success={1}, Response={2}, Duration={3}, Score={4}, Extensions={5}]", 
                                 Completion, Success, Response, Duration, Score, Extensions);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Result(JObject jobj)
        {
            return new Result(jobj);
        }
    }
}
