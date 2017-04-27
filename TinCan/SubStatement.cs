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
    /// Sub statement.
    /// </summary>
    public class SubStatement : StatementBase, IStatementTarget
    {
        /// <summary>
        /// The type of the object.
        /// </summary>
        public static readonly string OBJECT_TYPE = "SubStatement";

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public string ObjectType 
        { 
            get 
            { 
                return OBJECT_TYPE; 
            } 
        }

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
        public override JObject ToJObject(TCAPIVersion version) {
            var result = base.ToJObject(version);

            result.Add("objectType", ObjectType);

            return result;
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="jobj">Jobj.</param>
        public static explicit operator SubStatement(JObject jobj)
        {
            return new SubStatement(jobj);
        }
    }
}
