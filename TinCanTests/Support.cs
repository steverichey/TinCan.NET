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
using TinCan;

namespace TinCanTests
{
    static class Support
    {
        public static readonly Agent Agent;
        public static readonly Verb Verb;
        public static readonly Activity Activity;
        public static readonly Activity Parent;
        public static readonly Context Context;
        public static readonly Result Result;
        public static readonly Score Score;
        public static readonly StatementRef StatementRef;
        public static readonly SubStatement SubStatement;

        static Support () {
            Agent = new Agent
            {
                Mbox = "mailto:tincancsharp@tincanapi.com"
            };

            Verb = new Verb("http://adlnet.gov/expapi/verbs/experienced")
            {
                Display = new LanguageMap()
            };

            Verb.Display.Add("en-US", "experienced");

            Activity = new Activity
            {
                Id = "http://tincanapi.com/TinCanCSharp/Test/Unit/0",
                Definition = new ActivityDefinition()
            };

            Activity.Definition.Type = new Uri("http://id.tincanapi.com/activitytype/unit-test");
            Activity.Definition.Name = new LanguageMap();
            Activity.Definition.Name.Add("en-US", "Tin Can C# Tests: Unit 0");
            Activity.Definition.Description = new LanguageMap();
            Activity.Definition.Description.Add("en-US", "Unit test 0 in the test suite for the Tin Can C# library.");

            Parent = new Activity
            {
                Id = "http://tincanapi.com/TinCanCSharp/Test",
                Definition = new ActivityDefinition()
            };

            Parent.Definition.Type = new Uri("http://id.tincanapi.com/activitytype/unit-test-suite");
            Parent.Definition.Name = new LanguageMap();
            Parent.Definition.Name.Add("en-US", "Tin Can C# Tests");
            Parent.Definition.Description = new LanguageMap();
            Parent.Definition.Description.Add("en-US", "Unit test suite for the Tin Can C# library.");

            StatementRef = new StatementRef(Guid.NewGuid());

            Context = new Context
            {
                Registration = Guid.NewGuid(),
                Statement = StatementRef,
                ContextActivities = new ContextActivities()
            };

            Context.ContextActivities.Parent = new List<Activity>
            {
                Parent
            };

            Score = new Score
            {
                Raw = 97,
                Scaled = 0.97,
                Max = 100,
                Min = 0
            };

            Result = new Result
            {
                Score = Score,
                Success = true,
                Completion = true,
                Duration = new TimeSpan(1, 2, 16, 43)
            };

            SubStatement = new SubStatement
            {
                Actor = Agent,
                Verb = Verb,
                Target = Parent
            };
        }
    }
}