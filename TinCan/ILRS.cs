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
using System.Threading.Tasks;
using TinCan.Documents;
using TinCan.LRSResponses;

namespace TinCan
{
    /// <summary>
    /// Lrs.
    /// </summary>
    public interface ILRS
    {
        /// <summary>
        /// About this instance.
        /// </summary>
        /// <returns>The about.</returns>
        Task<AboutLRSResponse> About();

        /// <summary>
        /// Saves the statement.
        /// </summary>
        /// <returns>The statement.</returns>
        /// <param name="statement">Statement.</param>
        Task<StatementLRSResponse> SaveStatement(Statement statement);

        /// <summary>
        /// Voids the statement.
        /// </summary>
        /// <returns>The statement.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="agent">Agent.</param>
        Task<StatementLRSResponse> VoidStatement(Guid id, Agent agent);

        /// <summary>
        /// Saves the statements.
        /// </summary>
        /// <returns>The statements.</returns>
        /// <param name="statements">Statements.</param>
        Task<StatementsResultLRSResponse> SaveStatements(List<Statement> statements);

        /// <summary>
        /// Retrieves the statement.
        /// </summary>
        /// <returns>The statement.</returns>
        /// <param name="id">Identifier.</param>
        Task<StatementLRSResponse> RetrieveStatement(Guid id);

        /// <summary>
        /// Retrieves the voided statement.
        /// </summary>
        /// <returns>The voided statement.</returns>
        /// <param name="id">Identifier.</param>
        Task<StatementLRSResponse> RetrieveVoidedStatement(Guid id);

        /// <summary>
        /// Queries the statements.
        /// </summary>
        /// <returns>The statements.</returns>
        /// <param name="query">Query.</param>
        Task<StatementsResultLRSResponse> QueryStatements(StatementsQuery query);

        /// <summary>
        /// Mores the statements.
        /// </summary>
        /// <returns>The statements.</returns>
        /// <param name="result">Result.</param>
        Task<StatementsResultLRSResponse> MoreStatements(StatementsResult result);

        /// <summary>
        /// Retrieves the state identifiers.
        /// </summary>
        /// <returns>The state identifiers.</returns>
        /// <param name="activity">Activity.</param>
        /// <param name="agent">Agent.</param>
        /// <param name="registration">Registration.</param>
        Task<ProfileKeysLRSResponse> RetrieveStateIds(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        /// Retrieves the state.
        /// </summary>
        /// <returns>The state.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="activity">Activity.</param>
        /// <param name="agent">Agent.</param>
        /// <param name="registration">Registration.</param>
        Task<StateLRSResponse> RetrieveState(string id, Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        /// Saves the state.
        /// </summary>
        /// <returns>The state.</returns>
        /// <param name="state">State.</param>
        Task<LRSResponse> SaveState(StateDocument state);

        /// <summary>
        /// Deletes the state.
        /// </summary>
        /// <returns>The state.</returns>
        /// <param name="state">State.</param>
        Task<LRSResponse> DeleteState(StateDocument state);

        /// <summary>
        /// Clears the state.
        /// </summary>
        /// <returns>The state.</returns>
        /// <param name="activity">Activity.</param>
        /// <param name="agent">Agent.</param>
        /// <param name="registration">Registration.</param>
        Task<LRSResponse> ClearState(Activity activity, Agent agent, Guid? registration = null);

        /// <summary>
        /// Retrieves the activity profile identifiers.
        /// </summary>
        /// <returns>The activity profile identifiers.</returns>
        /// <param name="activity">Activity.</param>
        Task<ProfileKeysLRSResponse> RetrieveActivityProfileIds(Activity activity);

        /// <summary>
        /// Retrieves the activity profile.
        /// </summary>
        /// <returns>The activity profile.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="activity">Activity.</param>
        Task<ActivityProfileLRSResponse> RetrieveActivityProfile(string id, Activity activity);

        /// <summary>
        /// Saves the activity profile.
        /// </summary>
        /// <returns>The activity profile.</returns>
        /// <param name="profile">Profile.</param>
        Task<LRSResponse> SaveActivityProfile(ActivityProfileDocument profile);

        /// <summary>
        /// Deletes the activity profile.
        /// </summary>
        /// <returns>The activity profile.</returns>
        /// <param name="profile">Profile.</param>
        Task<LRSResponse> DeleteActivityProfile(ActivityProfileDocument profile);

        /// <summary>
        /// Retrieves the agent profile identifiers.
        /// </summary>
        /// <returns>The agent profile identifiers.</returns>
        /// <param name="agent">Agent.</param>
        Task<ProfileKeysLRSResponse> RetrieveAgentProfileIds(Agent agent);

        /// <summary>
        /// Retrieves the agent profile.
        /// </summary>
        /// <returns>The agent profile.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="agent">Agent.</param>
        Task<AgentProfileLRSResponse> RetrieveAgentProfile(string id, Agent agent);

        /// <summary>
        /// Saves the agent profile.
        /// </summary>
        /// <returns>The agent profile.</returns>
        /// <param name="profile">Profile.</param>
        Task<LRSResponse> SaveAgentProfile(AgentProfileDocument profile);

        /// <summary>
        /// Forces the save agent profile.
        /// </summary>
        /// <returns>The save agent profile.</returns>
        /// <param name="profile">Profile.</param>
        Task<LRSResponse> ForceSaveAgentProfile(AgentProfileDocument profile);

        /// <summary>
        /// Deletes the agent profile.
        /// </summary>
        /// <returns>The agent profile.</returns>
        /// <param name="profile">Profile.</param>
        Task<LRSResponse> DeleteAgentProfile(AgentProfileDocument profile);
    }
}
