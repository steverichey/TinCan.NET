﻿/*
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
using System.Text;

namespace TinCan.LRSResponses
{
	/// <summary>
	/// LRS response object.
	/// This isn't abstract because some responses for an LRS won't have content.
	/// In those cases we can get by just returning this base response.
	/// </summary>
	public class LRSResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:TinCan.LRSResponses.LRSResponse"/> was successful.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }

		/// <summary>
		/// Gets or sets the HTTP exception, if one exists.
        /// This should only be non-null if the Success property is false.
		/// </summary>
		/// <value>The HTTP exception.</value>
		public Exception HttpException { get; set; }

		/// <summary>
		/// Gets or sets the error message, if one exists.
		/// This should only be non-null if the Success property is false.
		/// </summary>
		/// <value>The error message.</value>
		public string ErrorMessage { get; set; }

        /// <summary>
        /// Sets the error message from bytes.
        /// </summary>
        /// <param name="content">Content.</param>
        public void SetErrMsgFromBytes(byte[] content)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            ErrorMessage = Encoding.UTF8.GetString(content);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LRSResponses.LRSResponse"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.LRSResponses.LRSResponse"/>.</returns>
        public override string ToString()
        {
            return string.Format("[LRSResponse: Success={0}, HttpException={1}, ErrorMessage={2}]", 
                                 Success, HttpException, ErrorMessage);
        }
    }
}
