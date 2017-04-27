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
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
	/// <summary>
	/// An optional property that represents the outcome of a graded Activity achieved by an Agent.
	/// </summary>
	public class Score : JsonModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Score"/> class.
        /// </summary>
        public Score() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Score"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Score(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Score"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Score(JObject jobj)
        {
            if (jobj["scaled"] != null)
            {
                Scaled = jobj.Value<Double>("scaled");
            }

            if (jobj["raw"] != null)
            {
                Raw = jobj.Value<Double>("raw");
            }

            if (jobj["min"] != null)
            {
                Min = jobj.Value<Double>("min");
            }

            if (jobj["max"] != null)
            {
                Max = jobj.Value<Double>("max");
            }
        }

		/// <summary>
		/// Gets or sets the score related to the experience as modified by scaling and/or normalization.
		/// </summary>
		/// <value>The score related to the experience as modified by scaling and/or normalization.</value>
		public Double? Scaled { get; set; }

		/// <summary>
		/// Gets or sets the score achieved by the Actor in the experience described by the Statement.
		/// This is not modified by any scaling or normalization.
		/// </summary>
		/// <value>The score achieved by the Actor in the experience described by the Statement.</value>
		public Double? Raw { get; set; }

		/// <summary>
		/// Gets or sets the lowest possible score for the experience described by the Statement.
		/// </summary>
		/// <value>The lowest possible score for the experience described by the Statement.</value>
		public Double? Min { get; set; }

		/// <summary>
		/// Gets or sets the highest possible score for the experience described by the Statement.
		/// </summary>
		/// <value>The highest possible score for the experience described by the Statement.</value>
		public Double? Max { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = new JObject();

            if (Scaled != null)
            {
                result.Add("scaled", Scaled);
            }

            if (Raw != null)
            {
                result.Add("raw", Raw);
            }

            if (Min != null)
            {
                result.Add("min", Min);
            }

            if (Max != null)
            {
                result.Add("max", Max);
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Score"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Score"/>.</returns>
        public override string ToString()
        {
            return string.Format("[Score: Scaled={0}, Raw={1}, Min={2}, Max={3}]", 
                                 Scaled, Raw, Min, Max);
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator Score(JObject jobj)
        {
            return new Score(jobj);
        }
    }
}
