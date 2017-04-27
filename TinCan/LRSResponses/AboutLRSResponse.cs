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

namespace TinCan.LRSResponses
{
    /// <summary>
    /// LRS response containing information about the LRS, including the xAPI version supported.
    /// </summary>
    public class AboutLRSResponse : LRSResponse
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public About Content { get; set; }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LRSResponses.AboutLRSResponse"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LRSResponses.AboutLRSResponse"/>.</returns>
		public override string ToString()
		{
            return string.Format("[LRSResponse: Success={0}, HttpException={1}, ErrorMessage={2}, Content={3}]", 
                                 Success, HttpException, ErrorMessage, Content);
		}
    }
}
