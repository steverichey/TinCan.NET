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

using NUnit.Framework;
using Newtonsoft.Json.Linq;
using TinCan;
using TinCan.Json;

namespace TinCanTests
{
    [TestFixture]
    class VerbTest
    {
        [Test]
        public void TestEmptyCtr()
        {
            var obj = new Verb();
            Assert.IsNull(obj.Id);
            Assert.IsNull(obj.Display);

            StringAssert.AreEqualIgnoringCase("{}", obj.ToJSON());
        }

        [Test]
        public void TestJObjectCtr()
        {
            var id = "http://adlnet.gov/expapi/verbs/experienced";
            var cfg = new JObject
            {
                { "id", id }
            };

            var obj = new Verb(cfg);
            Assert.That(obj.ToJSON(), Is.EqualTo("{\"id\":\"" + id + "\"}"));
        }

        [Test]
        public void TestStringOfJSONCtr()
        {
            var id = "http://adlnet.gov/expapi/verbs/experienced";
            var json = "{\"id\":\"" + id + "\"}";
            var strOfJson = new StringOfJSON(json);

            var obj = new Verb(strOfJson);
            Assert.That(obj.ToJSON(), Is.EqualTo(json));
        }
    }
}
