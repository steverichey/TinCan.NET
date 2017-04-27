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
    /// Score.
    /// </summary>
    public class Score : JsonModel
    {
        /// <summary>
        /// Gets or sets the scaled.
        /// </summary>
        /// <value>The scaled.</value>
        public Double? Scaled { get; set; }

        /// <summary>
        /// Gets or sets the raw.
        /// </summary>
        /// <value>The raw.</value>
        public Double? Raw { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>The minimum.</value>
        public Double? Min { get; set; }

        /// <summary>
        /// Gets or sets the max.
        /// </summary>
        /// <value>The max.</value>
        public Double? Max { get; set; }

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
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) {
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
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator Score(JObject jobj)
        {
            return new Score(jobj);
        }
    }
}
