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

namespace TinCan
{
    /// <summary>
    /// Statements query result format enumeration.
    /// </summary>
    public sealed class StatementsQueryResultFormat
    {
        readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.StatementsQueryResultFormat"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        StatementsQueryResultFormat(string value)
        {
            text = value;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsQueryResultFormat"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsQueryResultFormat"/>.</returns>
        public override string ToString()
        {
            return text;
        }

		/// <summary>
		/// Only include minimum information necessary in Agent, Activity, Verb and Group Objects to identify them.
        /// For Anonymous Groups this means including the minimum information needed to identify each member. 
		/// </summary>
		public static readonly StatementsQueryResultFormat Ids = new StatementsQueryResultFormat("ids");

		/// <summary>
		/// Return Agent, Activity, Verb and Group Objects populated exactly as they were when the Statement was received.
        /// An LRS requesting Statements for the purpose of importing them would use a format of "exact" in order to maintain Statement Immutability. 
		/// </summary>
		public static readonly StatementsQueryResultFormat Exact = new StatementsQueryResultFormat("exact");

		/// <summary>
		/// Return Activity Objects and Verbs populated with the canonical definition of the Activity Objects and Display of the Verbs as determined by the LRS, after applying language filtering, and return the original Agent and Group Objects as in "exact" mode.
		/// </summary>
		public static readonly StatementsQueryResultFormat Canonical = new StatementsQueryResultFormat("canonical");
    }
}
