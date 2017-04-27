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

namespace TinCan.Json
{
    /// <summary>
    /// Json model.
    /// </summary>
    public interface IJsonModel
    {
        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        JObject ToJObject(TCAPIVersion version);

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        JObject ToJObject();

        /// <summary>
        /// Tos the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="version">Version.</param>
        /// <param name="pretty">If set to <c>true</c> pretty.</param>
        String ToJSON(TCAPIVersion version, Boolean pretty = false);

        /// <summary>
        /// Tos the json.
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="pretty">If set to <c>true</c> pretty.</param>
        String ToJSON(Boolean pretty = false);
    }
}
