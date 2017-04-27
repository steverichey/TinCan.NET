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

using System;
using System.Collections.Generic;

namespace TinCan
{
    /// <summary>
    /// An object describing a query for statements.
    /// </summary>
    public class StatementsQuery
    {
        string activityId;

        /// <summary>
        /// Gets or sets the agent to query.
        /// </summary>
        /// <value>The agent.</value>
        public Agent Agent { get; set; }

        /// <summary>
        /// Gets or sets the verb identifier to query.
        /// </summary>
        /// <value>The verb identifier.</value>
        public Uri VerbId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier to query.
        /// </summary>
        /// <value>The activity identifier.</value>
        public string ActivityId 
        {
            get 
            { 
                return activityId; 
            }

            set
            {
                var uri = new Uri(value);
                activityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the registration ID to query.
        /// </summary>
        /// <value>The registration.</value>
        public Guid? Registration { get; set; }

		/// <summary>
		/// Gets or sets whether or not to apply the activity filter broadly.
		/// </summary>
		/// <value>Whether or not to apply the activity filter broadly.</value>
		public bool? RelatedActivities { get; set; }

		/// <summary>
		/// Gets or sets whether or not to apply the agent filter broadly.
		/// </summary>
		/// <value>Whether or not to apply the agent filter broadly.</value>
		public bool? RelatedAgents { get; set; }

		/// <summary>
		/// Gets or sets the time for which only Statements stored since the specified Timestamp (exclusive) are returned.
		/// </summary>
		/// <value>The timestamp.</value>
		public DateTime? Since { get; set; }

		/// <summary>
		/// Gets or sets the time for which only Statements stored at or before the specified Timestamp are returned.
		/// </summary>
		/// <value>The timestamp.</value>
		public DateTime? Until { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of statements to return.
        /// </summary>
        /// <value>The limit.</value>
        public Int32? Limit { get; set; }

        /// <summary>
        /// Gets or sets the format to return.
        /// </summary>
        /// <value>The format.</value>
        public StatementsQueryResultFormat Format { get; set; }

		/// <summary>
		/// Gets or sets whether or not to return in ascending order.
		/// </summary>
		/// <value>Whether or not to return in ascending order.</value>
		public bool? Ascending { get; set; }

        /// <summary>
        /// Convert this query to a query parameter map.
        /// </summary>
        /// <returns>The parameter map.</returns>
        /// <param name="version">Version of xAPI to use when building the map.</param>
        public Dictionary<string, string> ToParameterMap (TCAPIVersion version)
        {
            var result = new Dictionary<string, string>();

            if (Agent != null)
            {
                result.Add("agent", Agent.ToJSON(version));
            }

            if (VerbId != null)
            {
                result.Add("verb", VerbId.ToString());
            }

            if (ActivityId != null)
            {
                result.Add("activity", ActivityId);
            }

            if (Registration != null)
            {
                result.Add("registration", Registration.Value.ToString());
            }

            if (RelatedActivities != null)
            {
                result.Add("related_activities", RelatedActivities.Value.ToString());
            }

            if (RelatedAgents != null)
            {
                result.Add("related_agents", RelatedAgents.Value.ToString());
            }

            if (Since != null)
            {
                result.Add("since", Since.Value.ToString(TimeFormat.Default));
            }

            if (Until != null)
            {
                result.Add("until", Until.Value.ToString(TimeFormat.Default));
            }

            if (Limit != null)
            {
                result.Add("limit", Limit.ToString());
            }

            if (Format != null)
            {
                result.Add("format", Format.ToString());
            }

            if (Ascending != null)
            {
                result.Add("ascending", Ascending.Value.ToString());
            }

            return result;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsQuery"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.StatementsQuery"/>.</returns>
        public override string ToString()
        {
            return string.Format("[StatementsQuery: Agent={0}, VerbId={1}, ActivityId={2}, Registration={3}, RelatedActivities={4}, RelatedAgents={5}, Since={6}, Until={7}, Limit={8}, Format={9}, Ascending={10}]", 
                                 Agent, VerbId, ActivityId, Registration, RelatedActivities, RelatedAgents, Since, Until, Limit, Format, Ascending);
        }
    }
}
