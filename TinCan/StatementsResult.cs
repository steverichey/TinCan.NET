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

using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// LRS result with statement information.
    /// </summary>
    public class StatementsResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsResult"/> class.
        /// </summary>
        public StatementsResult() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsResult"/> class.
        /// </summary>
        /// <param name="str">String.</param>
        public StatementsResult(string str) : this(new StringOfJSON(str)) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsResult"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public StatementsResult(StringOfJSON json) : this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsResult"/> class.
        /// </summary>
        /// <param name="statements">Statements.</param>
        public StatementsResult(List<Statement> statements)
        {
            Statements = statements;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TinCan.StatementsResult"/> class.
		/// </summary>
		/// <param name="jobj">Jobj.</param>
		public StatementsResult(JObject jobj)
		{
			if (jobj["statements"] != null)
			{
				Statements = new List<Statement>();

				foreach (var item in jobj.Value<JArray>("statements"))
				{
					Statements.Add(new Statement((JObject)item));
				}
			}

			if (jobj["more"] != null)
			{
				More = jobj.Value<string>("more");
			}
		}

		/// <summary>
		/// Gets or sets the statements from the LRS.
		/// </summary>
		/// <value>The statements.</value>
		public List<Statement> Statements { get; set; }

		/// <summary>
		/// Gets or sets the ID to use to get more results, if applicable.
		/// </summary>
		/// <value>The identifier.</value>
		public string More { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsResult"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsResult"/>.</returns>
        public override string ToString()
        {
            return string.Format("[StatementsResult: Statements={0}, More={1}]", 
                                 Statements, More);
        }
    }
}
