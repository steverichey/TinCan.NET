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
    /// Agent resource object.
    /// </summary>
    public class Agent : JsonModel, IStatementTarget
    {
        /// <summary>
        /// The type of the object.
        /// </summary>
        public static readonly string OBJECT_TYPE = "Agent";

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public virtual string ObjectType 
        { 
            get 
            { 
                return OBJECT_TYPE; 
            } 
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the mbox.
        /// </summary>
        /// <value>The mbox.</value>
        public string Mbox { get; set; }

        /// <summary>
        /// Gets or sets the mbox sha1sum.
        /// </summary>
        /// <value>The mbox sha1sum.</value>
        public string MboxSha1Sum { get; set; }

        /// <summary>
        /// Gets or sets the openid.
        /// </summary>
        /// <value>The openid.</value>
        public string OpenId { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>The account.</value>
        public AgentAccount Account { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Agent"/> class.
        /// </summary>
        public Agent() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Agent"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public Agent(StringOfJSON json) : this(json.ToJObject()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.Agent"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public Agent(JObject jobj)
        {
            if (jobj["name"] != null)
            {
                Name = jobj.Value<string>("name");
            }

            if (jobj["mbox"] != null)
            {
                Mbox = jobj.Value<string>("mbox");
            }

            if (jobj["mbox_sha1sum"] != null)
            {
                MboxSha1Sum = jobj.Value<string>("mbox_sha1sum");
            }

            if (jobj["openid"] != null)
            {
                OpenId = jobj.Value<string>("openid");
            }

            if (jobj["account"] != null)
            {
                Account = (AgentAccount)jobj.Value<JObject>("account");
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version)
        {
            JObject result = new JObject
            {
                { "objectType", ObjectType }
            };

            if (Name != null)
            {
                result.Add("name", Name);
            }

            if (Account != null)
            {
                result.Add("account", Account.ToJObject(version));
            }
            else if (Mbox != null)
            {
                result.Add("mbox", Mbox);
            }
            else if (MboxSha1Sum != null)
            {
                result.Add("mbox_sha1sum", MboxSha1Sum);
            }
            else if (OpenId != null)
            {
                result.Add("openid", OpenId);
            }

            return result;
        }

        /// <summary>
        /// Defines the operation to use when casting from a JObject to this type.
        /// </summary>
        /// <returns>The JObject as this type.</returns>
        /// <param name="jobj">The JObject to cast.</param>
        public static explicit operator Agent(JObject jobj)
        {
            return new Agent(jobj);
        }
    }
}
