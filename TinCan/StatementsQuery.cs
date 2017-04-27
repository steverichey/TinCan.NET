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
using System.Collections.Generic;

namespace TinCan
{
    /// <summary>
    /// Statements query.
    /// </summary>
    public class StatementsQuery
    {
		/// <summary>
		/// The ISOD ate time format.
		/// TODO: put in common location
		/// </summary>
		const string ISODateTimeFormat = "o";

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>The agent.</value>
        public Agent Agent { get; set; }

        /// <summary>
        /// Gets or sets the verb identifier.
        /// </summary>
        /// <value>The verb identifier.</value>
        public Uri VerbId { get; set; }

        /// <summary>
        /// The activity identifier.
        /// </summary>
        string activityId;

        /// <summary>
        /// Gets or sets the activity identifier.
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
                Uri uri = new Uri(value);
                activityId = value;
            }
        }

        /// <summary>
        /// Gets or sets the registration.
        /// </summary>
        /// <value>The registration.</value>
        public Guid? Registration { get; set; }

        /// <summary>
        /// Gets or sets the related activities.
        /// </summary>
        /// <value>The related activities.</value>
        public bool? RelatedActivities { get; set; }

        /// <summary>
        /// Gets or sets the related agents.
        /// </summary>
        /// <value>The related agents.</value>
        public bool? RelatedAgents { get; set; }

        /// <summary>
        /// Gets or sets the since.
        /// </summary>
        /// <value>The since.</value>
        public DateTime? Since { get; set; }

        /// <summary>
        /// Gets or sets the until.
        /// </summary>
        /// <value>The until.</value>
        public DateTime? Until { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public Int32? Limit { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        public StatementsQueryResultFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the ascending.
        /// </summary>
        /// <value>The ascending.</value>
        public bool? Ascending { get; set; }

        /// <summary>
        /// Tos the parameter map.
        /// </summary>
        /// <returns>The parameter map.</returns>
        /// <param name="version">Version.</param>
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
                result.Add("since", Since.Value.ToString(ISODateTimeFormat));
            }

            if (Until != null)
            {
                result.Add("until", Until.Value.ToString(ISODateTimeFormat));
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
    }
}
