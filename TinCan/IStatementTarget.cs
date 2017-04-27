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

using Newtonsoft.Json.Linq;

namespace TinCan
{
    /// <summary>
    /// Statement target.
    /// </summary>
    public interface IStatementTarget
    {
        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        JObject ToJObject(TCAPIVersion version);

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        string ObjectType { get; }
    }
}
