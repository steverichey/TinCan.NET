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
using System.Threading.Tasks;
using TinCan.Documents;
using TinCan.LRSResponses;

namespace TinCan
{
    /// <summary>
    /// Defines the interface for an LRS.
    /// </summary>
    public interface ILRS
    {
        /// <summary>
        /// Retrieve information about this LRS.
        /// </summary>
        /// <returns>The response with information about this LRS.</returns>
        Task<AboutLRSResponse> About();

        /// <summary>
        /// Saves a statement in the LRS.
        /// </summary>
        /// <returns>Response with information about the saved statement.</returns>
        /// <param name="statement">Statement to save in the LRS.</param>
        Task<StatementLRSResponse> SaveStatement(Statement statement);

        /// <summary>
        /// Voids a statement in the LRS.
        /// </summary>
        /// <returns>Response with information about the voided statement.</returns>
        /// <param name="id">Identifier of statement to void.</param>
        /// <param name="agent">Agent of statement to void.</param>
        Task<StatementLRSResponse> VoidStatement(Guid id, Agent agent);

		/// <summary>
		/// Save several statements in the LRS.
		/// </summary>
		/// <returns>Response with information about the saved statements.</returns>
		/// <param name="statements">Statements to save in the LRS.</param>
		Task<StatementsResultLRSResponse> SaveStatements(List<Statement> statements);

		/// <summary>
		/// Retrieves a statement from the LRS.
		/// </summary>
		/// <returns>Response with information about the retrieved statement.</returns>
		/// <param name="id">Identifier of statement to retrieve.</param>
		Task<StatementLRSResponse> RetrieveStatement(Guid id);

		/// <summary>
		/// Retrieves a voided statement from the LRS.
		/// </summary>
		/// <returns>Response with information about the retrieved voided statement.</returns>
		/// <param name="id">Identifier of voided statement to retrieve.</param>
		Task<StatementLRSResponse> RetrieveVoidedStatement(Guid id);

		/// <summary>
		/// Queries the LRS for statements.
		/// </summary>
		/// <returns>Response with information about the queried statements.</returns>
		/// <param name="query">Query to send to the LRS.</param>
		Task<StatementsResultLRSResponse> QueryStatements(StatementsQuery query);

		/// <summary>
		/// Get more statements from the LRS using a previous result.
		/// </summary>
		/// <returns>Response with information about the additional statements.</returns>
		/// <param name="result">Result of a previous operation with ID for retrieving more results.</param>
		Task<StatementsResultLRSResponse> MoreStatements(StatementsResult result);

        /// <summary>
        /// Retrieves state identifiers for the given paraemeters.
        /// </summary>
        /// <returns>Response with information about the state identifiers.</returns>
        /// <param name="activity">Activity related to the state identifiers.</param>
        /// <param name="agent">Agent related to the state identifiers.</param>
        /// <param name="registration">Registration related to the state identifiers.</param>
        Task<ProfileKeysLRSResponse> RetrieveStateIds(Activity activity, Agent agent, Guid? registration = null);

		/// <summary>
		/// Retrieves a state from the LRS.
		/// </summary>
		/// <returns>Response with information about the state.</returns>
		/// <param name="id">Identifier of the state to retrieve.</param>
		/// <param name="activity">Activity related to the state to retrieve.</param>
		/// <param name="agent">Agent related to the state to retrieve.</param>
		/// <param name="registration">Registration related to the state to retrieve.</param>
		Task<StateLRSResponse> RetrieveState(string id, Activity activity, Agent agent, Guid? registration = null);

		/// <summary>
		/// Saves a state in the LRS.
		/// </summary>
		/// <returns>Response with information about the saved state.</returns>
		/// <param name="state">The state to save.</param>
		Task<LRSResponse> SaveState(StateDocument state);

		/// <summary>
		/// Deletes a state from the LRS.
		/// </summary>
		/// <returns>Response with information about the deleted state.</returns>
		/// <param name="state">The state to delete.</param>
		Task<LRSResponse> DeleteState(StateDocument state);

		/// <summary>
		/// Clears the state from the LRS.
		/// </summary>
		/// <returns>Response with information about the cleared state.</returns>
		/// <param name="activity">Activity related to the state.</param>
		/// <param name="agent">Agent related to the state.</param>
		/// <param name="registration">Registration identifier related to the state.</param>
		Task<LRSResponse> ClearState(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        /// Retrieves the activity profile IDs from the LRS.
        /// </summary>
        /// <returns>Response with information about the activity profile IDs.</returns>
        /// <param name="activity">Activity with profile IDs to retrieve.</param>
        Task<ProfileKeysLRSResponse> RetrieveActivityProfileIds(Activity activity);

        /// <summary>
        /// Retrieves an activity profile from the LRS.
        /// </summary>
        /// <returns>Response with information about the activity profile.</returns>
        /// <param name="id">Identifier of the activity profile to retrieve.</param>
        /// <param name="activity">Activity with profile information to retrieve.</param>
        Task<ActivityProfileLRSResponse> RetrieveActivityProfile(string id, Activity activity);

        /// <summary>
        /// Saves an activity profile in the LRS.
        /// </summary>
        /// <returns>Response with information about the saved activity profile.</returns>
        /// <param name="profile">Profile to save.</param>
        Task<LRSResponse> SaveActivityProfile(ActivityProfileDocument profile);

		/// <summary>
		/// Deletes an activity profile from the LRS.
		/// </summary>
		/// <returns>Response with information about the deleted activity profile.</returns>
		/// <param name="profile">Profile to delete.</param>
		Task<LRSResponse> DeleteActivityProfile(ActivityProfileDocument profile);

        /// <summary>
        /// Retrieves agent profile IDs from the LRS.
        /// </summary>
        /// <returns>Response with information about the agent profile identifiers.</returns>
        /// <param name="agent">Agent with profile IDs to retrieve.</param>
        Task<ProfileKeysLRSResponse> RetrieveAgentProfileIds(Agent agent);

		/// <summary>
		/// Retrieves an agent profile from the LRS.
		/// </summary>
		/// <returns>Response with information about the retrieved agent profile.</returns>
		/// <param name="id">Identifier of the agent profile to retrieve.</param>
		/// <param name="agent">Agent with profile to retrieve.</param>
		Task<AgentProfileLRSResponse> RetrieveAgentProfile(string id, Agent agent);

		/// <summary>
		/// Saves the agent profile in the LRS.
		/// </summary>
		/// <returns>Response with information about the saved agent profile.</returns>
		/// <param name="profile">Profile to save.</param>
		Task<LRSResponse> SaveAgentProfile(AgentProfileDocument profile);

		/// <summary>
		/// Saves the agent profile in the LRS and overwrites any previous version of the agent profile.
		/// </summary>
		/// <returns>Response with information about the saved agent profile.</returns>
		/// <param name="profile">Profile to force save.</param>
		Task<LRSResponse> ForceSaveAgentProfile(AgentProfileDocument profile);

		/// <summary>
		/// Deletes an agent profile from the LRS.
		/// </summary>
		/// <returns>Response with information about the deleted agent profile.</returns>
		/// <param name="profile">Profile to delete.</param>
		Task<LRSResponse> DeleteAgentProfile(AgentProfileDocument profile);
    }
}
