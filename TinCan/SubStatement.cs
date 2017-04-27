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

using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// Sub statement.
    /// </summary>
    public class SubStatement : StatementBase, IStatementTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.SubStatement"/> class.
        /// </summary>
        public SubStatement() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.SubStatement"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public SubStatement(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.SubStatement"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public SubStatement(JObject jobj) : base(jobj) { }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            var result = base.ToJObject(version);

            result.Add("objectType", ObjectType);

            return result;
		}

		/// <summary>
		/// Gets the type of the object.
		/// </summary>
		/// <value>The type of the object.</value>
		public string ObjectType
		{
			get
			{
				return TypeName;
			}
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.SubStatement"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.SubStatement"/>.</returns>
		public override string ToString()
		{
			return string.Format("[SubStatement: Actor={0}, Verb={1}, Target={2}, Result={3}, Context={4}, Timestamp={5}]", 
                                 Actor, Verb, Target, Result, Context, Timestamp);
		}

        /// <summary>
        /// The name of the type.
        /// </summary>
        public static string TypeName = nameof(SubStatement);

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator SubStatement(JObject jobj)
        {
            return new SubStatement(jobj);
        }
    }
}
