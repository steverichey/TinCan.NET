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
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TinCan;
using TinCan.Documents;
using TinCan.LRSResponses;

namespace TinCanTests
{
    [TestFixture]
    class RemoteLRSResourceTest
    {
        RemoteLRS lrs;

        [SetUp]
        public void Init()
        {
            // these are credentials used by the other OSS libs when building via Travis-CI
            // so are okay to include in the repository, if you wish to have access to the
            // results of the test suite then supply your own endpoint, username, and password
            lrs = new RemoteLRS(
                new Uri("https://cloud.scorm.com/tc/U2S4SI5FY0/sandbox/"),
                "Nja986GYE1_XrWMmFUE",
                "Bd9lDr1kjaWWY6RID_4"
            );
        }

        [Test]
        public async Task TestAbout()
        {
            var lrsRes = await lrs.About();
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestAboutFailure()
        {
			var invalidAboutLRS = new RemoteLRS(
                new Uri("http://cloud.scorm.com/tc/3TQLAI9/sandbox/"),
				"Nja986GYE1_XrWMmFUE",
				"Bd9lDr1kjaWWY6RID_4"
			);

            var lrsRes = await invalidAboutLRS.About();
            Assert.IsFalse(lrsRes.Success);
        }

        [Test]
        public async Task TestSaveStatement()
        {
            var statement = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity
            };

            var lrsRes = await lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
            Assert.IsNotNull(lrsRes.Content.Id);
        }

        [Test]
        public async Task TestSaveStatementWithID()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;

            var lrsRes = await lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestSaveStatementStatementRef()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.StatementRef;

            var lrsRes = await lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestSaveStatementSubStatement()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.SubStatement;

            var lrsRes = await lrs.SaveStatement(statement);
            Assert.IsTrue(lrsRes.Success);
            Assert.AreEqual(statement, lrsRes.Content);
        }

        [Test]
        public async Task TestVoidStatement()
        {
            var toVoid = Guid.NewGuid();
            var lrsRes = await lrs.VoidStatement(toVoid, Support.Agent);

            Assert.IsTrue(lrsRes.Success, "LRS response successful");
            Assert.AreEqual(new Uri("http://adlnet.gov/expapi/verbs/voided"), lrsRes.Content.Verb.Id, "voiding statement uses voided verb");
            Assert.AreEqual(toVoid, ((StatementRef) lrsRes.Content.Target).Id, "voiding statement target correct id");
        }

        [Test]
        public async Task TestSaveStatements()
        {
            var statement1 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Parent
            };

            var statement2 = new Statement
            {
                Actor = Support.Agent,
                Verb = Support.Verb,
                Target = Support.Activity,
                Context = Support.Context
            };

            var statements = new List<Statement>
            {
                statement1,
                statement2
            };

            var lrsRes = await lrs.SaveStatements(statements);
            Assert.IsTrue(lrsRes.Success);
            // TODO: check statements match and ids not null
        }

        [Test]
        public async Task TestRetrieveStatement()
        {
            var statement = new Statement();
            statement.Stamp();
            statement.Actor = Support.Agent;
            statement.Verb = Support.Verb;
            statement.Target = Support.Activity;
            statement.Context = Support.Context;
            statement.Result = Support.Result;

            var saveRes = await lrs.SaveStatement(statement);

            if (saveRes.Success)
            {
                var retRes = await lrs.RetrieveStatement(saveRes.Content.Id.Value);
                Assert.IsTrue(retRes.Success);
            }
            else
            {
                // TODO: skipped?
            }
        }

        [Test]
        public async Task TestQueryStatements()
        {
            var query = new StatementsQuery
            {
                Agent = Support.Agent,
                VerbId = Support.Verb.Id,
                ActivityId = Support.Parent.Id,
                RelatedActivities = true,
                RelatedAgents = true,
                Format = StatementsQueryResultFormat.Ids,
                Limit = 10
            };

            var lrsRes = await lrs.QueryStatements(query);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestMoreStatements()
        {
            var query = new StatementsQuery
            {
                Format = StatementsQueryResultFormat.Ids,
                Limit = 2
            };

            var queryRes = await lrs.QueryStatements(query);

            if (queryRes.Success && queryRes.Content.More != null)
            {
                var moreRes = await lrs.MoreStatements(queryRes.Content);
                Assert.IsTrue(moreRes.Success);
            }
            else
            {
                // TODO: skipped?
            }
        }

        [Test]
        public async Task TestRetrieveStateIds()
        {
            var lrsRes = await lrs.RetrieveStateIds(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveState()
        {
            var lrsRes = await lrs.RetrieveState("test", Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<StateDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveState()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                Id = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await lrs.SaveState(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteState()
        {
            var doc = new StateDocument
            {
                Activity = Support.Activity,
                Agent = Support.Agent,
                Id = "test"
            };

            var lrsRes = await lrs.DeleteState(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestClearState()
        {
            var lrsRes = await lrs.ClearState(Support.Activity, Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveActivityProfileIds()
        {
            var lrsRes = await lrs.RetrieveActivityProfileIds(Support.Activity);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveActivityProfile()
        {
            var lrsRes = await lrs.RetrieveActivityProfile("test", Support.Activity);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<ActivityProfileDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveActivityProfile()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                Id = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await lrs.SaveActivityProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteActivityProfile()
        {
            var doc = new ActivityProfileDocument
            {
                Activity = Support.Activity,
                Id = "test"
            };

            var lrsRes = await lrs.DeleteActivityProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveAgentProfileIds()
        {
            var lrsRes = await lrs.RetrieveAgentProfileIds(Support.Agent);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestRetrieveAgentProfile()
        {
            var lrsRes = await lrs.RetrieveAgentProfile("test", Support.Agent);
            Assert.IsTrue(lrsRes.Success);
            Assert.IsInstanceOf<AgentProfileDocument>(lrsRes.Content);
        }

        [Test]
        public async Task TestSaveAgentProfile()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                Id = "test",
                Content = Encoding.UTF8.GetBytes("Test value")
            };

            var lrsRes = await lrs.SaveAgentProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }

        [Test]
        public async Task TestDeleteAgentProfile()
        {
            var doc = new AgentProfileDocument
            {
                Agent = Support.Agent,
                Id = "test"
            };

            var lrsRes = await lrs.DeleteAgentProfile(doc);
            Assert.IsTrue(lrsRes.Success);
        }
    }
}
