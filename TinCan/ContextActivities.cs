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
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TinCan.Json;

namespace TinCan
{
    /// <summary>
    /// Context activities.
    /// </summary>
    public class ContextActivities : JsonModel
    {
        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>The parent.</value>
        public List<Activity> Parent { get; set; }

        /// <summary>
        /// Gets or sets the grouping.
        /// </summary>
        /// <value>The grouping.</value>
        public List<Activity> Grouping { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        public List<Activity> Category { get; set; }

        /// <summary>
        /// Gets or sets the other.
        /// </summary>
        /// <value>The other.</value>
        public List<Activity> Other { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ContextActivities"/> class.
        /// </summary>
        public ContextActivities() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ContextActivities"/> class.
        /// </summary>
        /// <param name="json">Json.</param>
        public ContextActivities(StringOfJSON json): this(json.ToJObject()) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.ContextActivities"/> class.
        /// </summary>
        /// <param name="jobj">Jobj.</param>
        public ContextActivities(JObject jobj)
        {
            if (jobj["parent"] != null)
            {
                Parent = new List<Activity>();

                foreach (var jactivity in jobj["parent"]) 
                {
                    Parent.Add((Activity)jactivity);
                }
            }

            if (jobj["grouping"] != null)
            {
                Grouping = new List<Activity>();

                foreach (var jactivity in jobj["grouping"]) 
                {
                    Grouping.Add((Activity)jactivity);
                }
            }

            if (jobj["category"] != null)
            {
                Category = new List<Activity>();

                foreach (var jactivity in jobj["category"]) 
                {
                    Category.Add((Activity)jactivity);
                }
            }

            if (jobj["other"] != null)
            {
                Other = new List<Activity>();

                foreach (var jactivity in jobj["other"]) 
                {
                    Other.Add((Activity)jactivity);
                }
            }
        }

        /// <summary>
        /// Tos the JO bject.
        /// </summary>
        /// <returns>The JO bject.</returns>
        /// <param name="version">Version.</param>
        public override JObject ToJObject(TCAPIVersion version) {
            var result = new JObject();

            if (Parent != null && Parent.Count > 0)
            {
                var jparent = new JArray();
                result.Add("parent", jparent);

                foreach (var activity in Parent)
                {
                    jparent.Add(activity.ToJObject(version));
                }
            }

            if (Grouping != null && Grouping.Count > 0)
            {
                var jgrouping = new JArray();
                result.Add("grouping", jgrouping);

                foreach (var activity in Grouping)
                {
                    jgrouping.Add(activity.ToJObject(version));
                }
            }

            if (Category != null && Category.Count > 0)
            {
                var jcategory = new JArray();
                result.Add("category", jcategory);

                foreach (var activity in Category)
                {
                    jcategory.Add(activity.ToJObject(version));
                }
            }

            if (Other != null && Other.Count > 0)
            {
                var jother = new JArray();
                result.Add("other", jother);

                foreach (var activity in Other)
                {
                    jother.Add(activity.ToJObject(version));
                }
            }

            return result;
        }

		/// <summary>
		/// Defines the operation to use when casting from a JObject to this type.
		/// </summary>
		/// <returns>The JObject as this type.</returns>
		/// <param name="jobj">The JObject to cast.</param>
		public static explicit operator ContextActivities(JObject jobj)
        {
            return new ContextActivities(jobj);
        }
    }
}
