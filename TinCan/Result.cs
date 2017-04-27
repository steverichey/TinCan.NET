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
using System.Xml;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// Result.
    /// </summary>
    public class Result : JsonModel
    {
        /// <summary>
        /// Gets or sets the completion.
        /// </summary>
        /// <value>The completion.</value>
        public Boolean? Completion { get; set; }

        /// <summary>
        /// Gets or sets the success.
        /// </summary>
        /// <value>The success.</value>
        public Boolean? Success { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public Score Score { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>The extensions.</value>
        public Extensions Extensions { get; set; }

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
                Completion = jobj.Value<Boolean>("completion");
            }

            if (jobj["success"] != null)
            {
                Success = jobj.Value<Boolean>("success");
            }

            if (jobj["response"] != null)
            {
                Response = jobj.Value<String>("response");
            }

            if (jobj["duration"] != null)
            {
                Duration = XmlConvert.ToTimeSpan(jobj.Value<String>("duration"));
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
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) {
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
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator Result(JObject jobj)
        {
            return new Result(jobj);
        }
    }
}
