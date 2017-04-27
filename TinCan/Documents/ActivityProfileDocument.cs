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

using System.Text;

namespace TinCan.Documents
{
	/// <summary>
	/// A document where information about an activity is kept.
	/// </summary>
	public class ActivityProfileDocument : Document
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>The activity.</value>
        public Activity Activity { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Documents.ActivityProfileDocument"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.Documents.ActivityProfileDocument"/>.</returns>
		public override string ToString()
		{
			return string.Format("[StateDocument: Id={0}, Etag={1}, Timestamp={2}, ContentType={3}, Content={4}, Activity={5}]",
								 Id, Etag, Timestamp, ContentType, Encoding.UTF8.GetString(Content, 0, Content.Length), Activity);
		}
    }
}
