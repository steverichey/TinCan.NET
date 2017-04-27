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
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TinCan.Documents;
using TinCan.LRSResponses;

namespace TinCan
{
    /// <summary>
    /// Remote lrs.
    /// </summary>
    public class RemoteLRS : ILRS
    {
        readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.RemoteLRS"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint of the remote LRS.</param>
        public RemoteLRS(Uri endpoint)
        {
            Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Version = TCAPIVersion.Latest;
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TinCan.RemoteLRS"/> class.
		/// </summary>
		/// <param name="endpoint">Endpoint of the remote LRS.</param>
		/// <param name="version">Version of xAPI to use.</param>
		public RemoteLRS(Uri endpoint, TCAPIVersion version)
        {
			Endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TinCan.RemoteLRS"/> class.
		/// </summary>
		/// <param name="endpoint">Endpoint of the remote LRS.</param>
		/// <param name="username">Username to use when authenticating with the LRS.</param>
		/// <param name="password">Password to use when authenticating with the LRS.</param>
		public RemoteLRS(Uri endpoint, string username, string password) : this(endpoint)
		{
			SetAuth(username, password);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TinCan.RemoteLRS"/> class.
		/// </summary>
		/// <param name="endpoint">Endpoint of the remote LRS.</param>
		/// <param name="version">Version of xAPI to use.</param>
		/// <param name="username">Username to use when authenticating with the LRS.</param>
		/// <param name="password">Password to use when authenticating with the LRS.</param>
		public RemoteLRS(Uri endpoint, TCAPIVersion version, string username, string password) : this(endpoint, version)
        {
            SetAuth(username, password);
        }

        /// <summary>
        /// Gets the remote LRS endpoint.
        /// </summary>
        /// <value>The endpoint of the LRS.</value>
        public Uri Endpoint { get; }

        /// <summary>
        /// Gets the xAPI version.
        /// </summary>
        /// <value>The xAPI version.</value>
        public TCAPIVersion Version { get; }

        /// <summary>
        /// Gets or sets the authentication header.
        /// </summary>
        /// <value>The authentication header.</value>
        public string Auth { get; private set; }

        /// <summary>
        /// Gets the extended parameters.
        /// </summary>
        /// <value>The extended parameters.</value>
        public Dictionary<string, string> Extended { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Sets basic authentication with a username and password.
        /// </summary>
        /// <param name="username">Username for authentication.</param>
        /// <param name="password">Password for authentication.</param>
        public void SetAuth(string username, string password)
        {
            Auth = string.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password))));
        }

        /// <inheritdoc />
        public async Task<AboutLRSResponse> About()
        {
            var response = new AboutLRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Get,
                Resource = "about"
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;
            response.Content = new About(Encoding.UTF8.GetString(res.Content));

            return response;
        }

        /// <inheritdoc />
        public async Task<StatementLRSResponse> SaveStatement(Statement statement)
        {
            if (statement == null)
            {
                throw new ArgumentNullException(nameof(statement));
            }

            var response = new StatementLRSResponse();

            var request = new LRSHttpRequest()
            {
                QueryParams = new Dictionary<string, string>(),
                Resource = "statements"
            };

            if (statement.Id == null)
            {
                request.Method = HttpMethod.Post;
            }
            else
            {
                request.Method = HttpMethod.Put;
                request.QueryParams.Add("statementId", statement.Id.ToString());
            }

            request.ContentType = "application/json";
            request.Content = Encoding.UTF8.GetBytes(statement.ToJSON(Version));

            var res = await MakeRequest(request);

            if (statement.Id == null)
            {
                if (res.Status != HttpStatusCode.OK)
                {
                    response.Success = false;
                    response.HttpException = res.Exception;
                    response.SetErrMsgFromBytes(res.Content);
                    return response;
                }

                var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));
                statement.Id = new Guid((string)ids[0]);
            }
            else 
            {
                if (res.Status != HttpStatusCode.NoContent)
                {
                    response.Success = false;
                    response.HttpException = res.Exception;
                    response.SetErrMsgFromBytes(res.Content);
                    return response;
                }
            }

            response.Success = true;
            response.Content = statement;

            return response;
        }

        /// <inheritdoc />
        public async Task<StatementLRSResponse> VoidStatement(Guid id, Agent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }

            var voidStatement = new Statement
            {
                Actor = agent,
                Verb = new Verb
                {
                    Id = new Uri("http://adlnet.gov/expapi/verbs/voided"),
                    Display = new LanguageMap()
                },
                Target = new StatementRef
                {
                    Id = id
                }
            };

            voidStatement.Verb.Display.Add("en-US", "voided");

            return await SaveStatement(voidStatement);
        }

        /// <inheritdoc />
        public async Task<StatementsResultLRSResponse> SaveStatements(List<Statement> statements)
        {
            if (statements == null)
            {
                throw new ArgumentNullException(nameof(statements));
            }

            if (statements.Count < 1)
            {
                throw new ArgumentException("No statements given to SaveStatements");
            }

            var response = new StatementsResultLRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Post,
                Resource = "statements",
                ContentType = "application/json"
            };

            var jarray = new JArray();

            foreach (var statement in statements)
            {
                jarray.Add(statement.ToJObject(Version));
            }

            request.Content = Encoding.UTF8.GetBytes(jarray.ToString());

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            var ids = JArray.Parse(Encoding.UTF8.GetString(res.Content));

            for (int i = 0; i < ids.Count; i++)
            {
                statements[i].Id = new Guid((string)ids[i]);
            }

            response.Success = true;
            response.Content = new StatementsResult(statements);

            return response;
        }

		/// <inheritdoc />
		public async Task<StatementLRSResponse> RetrieveStatement(Guid id)
        {
            return await GetStatement(new Dictionary<string, string>
            {
                { "statementId", id.ToString() }
            });
        }

        /// <inheritdoc />
        public async Task<StatementLRSResponse> RetrieveVoidedStatement(Guid id)
        {
            return await GetStatement(new Dictionary<string, string>
            {
                { "voidedStatementId", id.ToString() }
            });
        }
        
        /// <inheritdoc />
        public async Task<StatementsResultLRSResponse> QueryStatements(StatementsQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var response = new StatementsResultLRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Get,
                Resource = "statements",
                QueryParams = query.ToParameterMap(Version)
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;
            response.Content = new StatementsResult(new Json.StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return response;
        }

        /// <inheritdoc />
        public async Task<StatementsResultLRSResponse> MoreStatements(StatementsResult result)
        {
            var response = new StatementsResultLRSResponse();

            var request = new LRSHttpRequest()
            {
                Method = HttpMethod.Get,
                Resource = Endpoint.GetLeftPart(UriPartial.Authority)
            };

            if (!request.Resource.EndsWith("/", StringComparison.Ordinal)) 
            {
                request.Resource += "/";
            }

            request.Resource += result.More;

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;
            response.Content = new StatementsResult(new Json.StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return response;
        }

        /// <inheritdoc />
        public async Task<ProfileKeysLRSResponse> RetrieveStateIds(Activity activity, Agent agent, Guid? registration = null)
		{
			// TODO: since param
			
            if (activity == null)
            {
                throw new ArgumentNullException(nameof(activity));
            }

            if (agent == null)
            {
                throw new ArgumentNullException(nameof(agent));
            }

            var queryParams = new Dictionary<string, string>
            {
                { "activityId", activity.Id },
                { "agent", agent.ToJSON(Version) }
            };

            if (registration != null)
            {
                queryParams.Add("registration", registration.ToString());
            }

            return await GetProfileKeys("activities/state", queryParams);
        }

		/// <inheritdoc />
		public async Task<StateLRSResponse> RetrieveState(string id, Activity activity, Agent agent, Guid? registration = null)
        {
            var response = new StateLRSResponse();

            var queryParams = new Dictionary<string, string>
            {
                { "stateId", id },
                { "activityId", activity.Id },
                { "agent", agent.ToJSON(Version) }
            };

            var state = new StateDocument
            {
                Id = id,
                Activity = activity,
                Agent = agent
            };

            if (registration != null)
            {
                queryParams.Add("registration", registration.ToString());
                state.Registration = registration;
            }

            var resp = await GetDocument("activities/state", queryParams, state);

            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                response.Success = false;
                response.HttpException = resp.Exception;
                response.SetErrMsgFromBytes(resp.Content);
                return response;
            }

            response.Success = true;
            response.Content = state;

            return response;
        }
        
        /// <inheritdoc />
        public async Task<LRSResponse> SaveState(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "stateId", state.Id },
                { "activityId", state.Activity.Id },
                { "agent", state.Agent.ToJSON(Version) }
            };

            if (state.Registration != null)
            {
                queryParams.Add("registration", state.Registration.ToString());
            }

            return await SaveDocument("activities/state", queryParams, state, HttpMethod.Put);
        }

        /// <inheritdoc />
        public async Task<LRSResponse> DeleteState(StateDocument state)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "stateId", state.Id },
                { "activityId", state.Activity.Id },
                { "agent", state.Agent.ToJSON(Version) }
            };

            if (state.Registration != null)
            {
                queryParams.Add("registration", state.Registration.ToString());
            }

            return await DeleteDocument("activities/state", queryParams);
        }

        /// <inheritdoc />
        public async Task<LRSResponse> ClearState(Activity activity, Agent agent, Guid? registration = null)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "activityId", activity.Id },
                { "agent", agent.ToJSON(Version) }
            };

            if (registration != null)
            {
                queryParams.Add("registration", registration.ToString());
            }

            return await DeleteDocument("activities/state", queryParams);
        }

		/// <inheritdoc />
		public async Task<ProfileKeysLRSResponse> RetrieveActivityProfileIds(Activity activity)
		{
			// TODO: since param

			var queryParams = new Dictionary<string, string>
            {
                { "activityId", activity.Id.ToString() }
            };

            return await GetProfileKeys("activities/profile", queryParams);
        }

		/// <inheritdoc />
		public async Task<ActivityProfileLRSResponse> RetrieveActivityProfile(string id, Activity activity)
        {
            var response = new ActivityProfileLRSResponse();

            var queryParams = new Dictionary<string, string>
            {
                { "profileId", id },
                { "activityId", activity.Id }
            };

            var profile = new ActivityProfileDocument
            {
                Id = id,
                Activity = activity
            };

            var resp = await GetDocument("activities/profile", queryParams, profile);

            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                response.Success = false;
                response.HttpException = resp.Exception;
                response.SetErrMsgFromBytes(resp.Content);
                return response;
            }

            response.Success = true;
            response.Content = profile;

            return response;
        }

		/// <inheritdoc />
		public async Task<LRSResponse> SaveActivityProfile(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "profileId", profile.Id },
                { "activityId", profile.Activity.Id }
            };

            return await SaveDocument("activities/profile", queryParams, profile, HttpMethod.Put);
        }

		/// <inheritdoc />
		public async Task<LRSResponse> DeleteActivityProfile(ActivityProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "profileId", profile.Id },
                { "activityId", profile.Activity.Id }
            };

            // TODO: need to pass Etag?

            return await DeleteDocument("activities/profile", queryParams);
        }

		/// <inheritdoc />
		public async Task<ProfileKeysLRSResponse> RetrieveAgentProfileIds(Agent agent)
        {
			// TODO: since param
			var queryParams = new Dictionary<string, string>
            {
                { "agent", agent.ToJSON(Version) }
            };

            return await GetProfileKeys("agents/profile", queryParams);
        }

		/// <inheritdoc />
		public async Task<AgentProfileLRSResponse> RetrieveAgentProfile(string id, Agent agent)
        {
            var response = new AgentProfileLRSResponse();

            var queryParams = new Dictionary<string, string>
            {
                { "profileId", id },
                { "agent", agent.ToJSON(Version) }
            };

            var profile = new AgentProfileDocument
            {
                Id = id,
                Agent = agent
            };

            var resp = await GetDocument("agents/profile", queryParams, profile);

            if (resp.Status != HttpStatusCode.OK && resp.Status != HttpStatusCode.NotFound)
            {
                response.Success = false;
                response.HttpException = resp.Exception;
                response.SetErrMsgFromBytes(resp.Content);
                return response;
            }

            response.Success = true;
            response.Content = profile;

            return response;
        }

		/// <inheritdoc />
		public async Task<LRSResponse> SaveAgentProfile(AgentProfileDocument profile)
        {
            return await SaveAgentProfile(profile, HttpMethod.Put);
        }

		/// <inheritdoc />
		public async Task<LRSResponse> ForceSaveAgentProfile(AgentProfileDocument profile)
        {
            return await SaveAgentProfile(profile, HttpMethod.Post);
        }

        /// <inheritdoc />
        public async Task<LRSResponse> DeleteAgentProfile(AgentProfileDocument profile)
        {
            var queryParams = new Dictionary<string, string>
            {
                { "profileId", profile.Id },
                { "agent", profile.Agent.ToJSON(Version) }
            };

            // TODO: need to pass Etag?

            return await DeleteDocument("agents/profile", queryParams);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.RemoteLRS"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.RemoteLRS"/>.</returns>
        public override string ToString()
        {
            return string.Format("[RemoteLRS: Endpoint={0}, Version={1}, Auth={2}, Extended={3}]", 
                                 Endpoint, Version, Auth, Extended);
        }

        async Task<LRSHttpResponse> MakeRequest(LRSHttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            string url;

            if (request.Resource.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
            {
                url = request.Resource;
            }
            else
            {
                url = Endpoint.ToString();

                if (!url.EndsWith("/", StringComparison.Ordinal) && !request.Resource.StartsWith("/", StringComparison.Ordinal)) 
                {
                    url += "/";
                }

                url += request.Resource;
            }

            if (request.QueryParams != null)
            {
                var queryString = string.Empty;

                foreach (var entry in request.QueryParams)
                {
                    if (queryString != "")
                    {
                        queryString += "&";
                    }

                    queryString += string.Format("{0}={1}", WebUtility.UrlEncode(entry.Key), WebUtility.UrlEncode(entry.Value));
                }

                if (!string.IsNullOrEmpty(queryString))
                {
                    url += string.Format("?{0}", queryString);
                }
            }

            // TODO: handle special properties we recognize, such as content type, modified since, etc.
            var webReq = new HttpRequestMessage(request.Method, new Uri(url));

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("X-Experience-API-Version", Version.ToString());
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(request.ContentType ?? "application/content-stream"));

            if (Auth != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", Auth);
            }

            if (request.Headers != null)
            {
                foreach (var entry in request.Headers)
                {
                    client.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }

            if (request.Content != null)
            {
                webReq.Content = new ByteArrayContent(request.Content);
                webReq.Content.Headers.Add("Content-Length", request.Content.Length.ToString());

                if (!string.IsNullOrWhiteSpace(request.ContentType))
                {
                    webReq.Content.Headers.Add("Content-Type", request.ContentType);
                }
                else
                {
                    webReq.Content.Headers.Add("Content-Type", "application/json");
                }
            }

            LRSHttpResponse response;

            try
            {
                var theResponse = await client.SendAsync(webReq).ConfigureAwait(false);
                response = new LRSHttpResponse(theResponse);
            }
            catch (WebException exception)
            {
                response = new LRSHttpResponse();

                if (exception.Response != null)
                {
                    using (var stream = exception.Response.GetResponseStream())
                    {
                        response.Content = ReadFully(stream, (int)exception.Response.ContentLength);
                    }
                }
                else
                {
                    response.Content = Encoding.UTF8.GetBytes("Web exception without '.Response'");
                }

                response.Exception = exception;
            }

            return response;
        }

        /// <summary>
        /// See http://www.yoda.arachsys.com/csharp/readbinary.html no license found
        /// 
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        static byte[] ReadFully(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            var buffer = new byte[initialLength];
            var read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    var newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }

            // Buffer is now too big. Shrink it.
            var ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        async Task<LRSHttpResponse> GetDocument(string resource, Dictionary<string, string> queryParams, Document document)
        {
            var req = new LRSHttpRequest
            {
                Method = HttpMethod.Get,
                Resource = resource,
                QueryParams = queryParams
            };

            var response = await MakeRequest(req);

            if (response.Status == HttpStatusCode.OK)
            {
                document.Content = response.Content;
                document.ContentType = response.ContentType;
                document.Timestamp = response.LastModified;
                document.Etag = response.Etag;
            }

            return response;
        }

        async Task<ProfileKeysLRSResponse> GetProfileKeys(string resource, Dictionary<string, string> queryParams)
        {
            var response = new ProfileKeysLRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Get,
                Resource = resource,
                QueryParams = queryParams
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;

            var keys = JArray.Parse(Encoding.UTF8.GetString(res.Content));

            if (keys.Count > 0) 
            {
                response.Content = new List<string>();

                foreach (JToken key in keys) 
                {
                    response.Content.Add((string)key);
                }
            }

            return response;
        }

        async Task<LRSResponse> SaveDocument(string resource, Dictionary<string, string> queryParams, Document document, HttpMethod method)
        {
            var response = new LRSResponse();

            var request = new LRSHttpRequest
            {
                Method = method,
                Resource = resource,
                QueryParams = queryParams,
                ContentType = document.ContentType,
                Content = document.Content
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.NoContent)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;

            return response;
        }

        async Task<LRSResponse> DeleteDocument(string resource, Dictionary<string, string> queryParams)
        {
            var response = new LRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Delete,
                Resource = resource,
                QueryParams = queryParams
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.NoContent)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;

            return response;
        }

        async Task<StatementLRSResponse> GetStatement(Dictionary<String, String> queryParams)
        {
            var response = new StatementLRSResponse();

            var request = new LRSHttpRequest
            {
                Method = HttpMethod.Get,
                Resource = "statements",
                QueryParams = queryParams
            };

            var res = await MakeRequest(request);

            if (res.Status != HttpStatusCode.OK)
            {
                response.Success = false;
                response.HttpException = res.Exception;
                response.SetErrMsgFromBytes(res.Content);
                return response;
            }

            response.Success = true;
            response.Content = new Statement(new Json.StringOfJSON(Encoding.UTF8.GetString(res.Content)));

            return response;
        }

        async Task<LRSResponse> SaveAgentProfile(AgentProfileDocument profile, HttpMethod method)
		{
			var queryParams = new Dictionary<string, string>
			{
				{ "profileId", profile.Id },
				{ "agent", profile.Agent.ToJSON(Version) }
			};

			return await SaveDocument("agents/profile", queryParams, profile, method);
		}
    }
}
