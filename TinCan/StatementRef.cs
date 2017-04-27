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
    /// An object describing a reference to another statement.
    /// </summary>
    public class StatementRef : JsonModel, IStatementTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementRef"/> class.
        /// </summary>
        public StatementRef() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementRef"/> class.
        /// </summary>
        /// <param name="id">Identifier.</param>
        public StatementRef(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementRef"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public StatementRef(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementRef"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public StatementRef(JObject jobj)
        {
            if (jobj["id"] != null)
            {
                Id = new Guid(jobj.Value<string>("id"));
            }
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
        /// Gets or sets the identifier of the referenced statement.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid? Id { get; set; }

        /// <inheritdoc />
        public override JObject ToJObject(TCAPIVersion version) 
        {
            var result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Id != null)
            {
                result.Add("id", Id.ToString());
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementRef"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementRef"/>.</returns>
        public override string ToString()
        {
            return string.Format("[StatementRef: ObjectType={0}, Id={1}]", 
                                 ObjectType, Id);
        }

		/// <summary>
		/// The type of the object.
		/// </summary>
		public static readonly string TypeName = nameof(StatementRef);

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator StatementRef(JObject jobj)
        {
            return new StatementRef(jobj);
        }
    }
}
