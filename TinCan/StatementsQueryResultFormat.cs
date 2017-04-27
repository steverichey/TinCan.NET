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

namespace TinCan
{
    /// <summary>
    /// Statements query result format.
    /// </summary>
    public sealed class StatementsQueryResultFormat
    {
        /// <summary>
        /// The identifiers.
        /// </summary>
        public static readonly StatementsQueryResultFormat Ids = new StatementsQueryResultFormat("ids");

        /// <summary>
        /// The exact.
        /// </summary>
        public static readonly StatementsQueryResultFormat Exact = new StatementsQueryResultFormat("exact");

        /// <summary>
        /// The canonical.
        /// </summary>
        public static readonly StatementsQueryResultFormat Canonical = new StatementsQueryResultFormat("canonical");

        readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsQueryResultFormat"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        StatementsQueryResultFormat(string value)
        {
            text = value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return text;
        }
    }
}
