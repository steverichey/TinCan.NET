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

namespace TinCan
{
    /// <summary>
    /// TCAPIV ersion.
    /// </summary>
    public sealed class TCAPIVersion
    {
        /// <summary>
        /// The v103.
        /// </summary>
        public static readonly TCAPIVersion V103 = new TCAPIVersion("1.0.3");

        /// <summary>
        /// The v102.
        /// </summary>
        public static readonly TCAPIVersion V102 = new TCAPIVersion("1.0.2");

        /// <summary>
        /// The v101.
        /// </summary>
        public static readonly TCAPIVersion V101 = new TCAPIVersion("1.0.1");

        /// <summary>
        /// The v100.
        /// </summary>
        public static readonly TCAPIVersion V100 = new TCAPIVersion("1.0.0");

        /// <summary>
        /// The v095.
        /// </summary>
        public static readonly TCAPIVersion V095 = new TCAPIVersion("0.95");

        /// <summary>
        /// The v090.
        /// </summary>
        public static readonly TCAPIVersion V090 = new TCAPIVersion("0.9");

        /// <summary>
        /// Latest this instance.
        /// </summary>
        /// <returns>The latest.</returns>
        public static TCAPIVersion Latest
        {
            get
            {
                return V103;
            }
        }

        static Dictionary<string, TCAPIVersion> known;
        static Dictionary<string, TCAPIVersion> supported;

        /// <summary>
        /// Gets the known.
        /// </summary>
        /// <returns>The known.</returns>
        public static Dictionary<string, TCAPIVersion> Known
        {
            get
            {
                if (known != null)
                {
                    return known;
                }

                known = new Dictionary<string, TCAPIVersion>
                {
                    { V103.text, V103 },
                    { V102.text, V102 },
                    { V101.text, V101 },
                    { V100.text, V100 },
                    { V095.text, V095 },
                    { V090.text, V090 }
                };

                return known;
            }
        }

        /// <summary>
        /// Gets the supported.
        /// </summary>
        /// <value>The supported.</value>
        public static Dictionary<string, TCAPIVersion> Supported
        {
            get
            {
                if (supported != null)
                {
                    return supported;
                }

                supported = new Dictionary<string, TCAPIVersion>
                {
                    { V103.text, V103 },
                    { V102.text, V102 },
                    { V101.text, V101 },
                    { V100.text, V100 }
                };

                return supported;
            }
        }

        /// <summary>
        /// Ops the explicit.
        /// </summary>
        /// <returns>The explicit.</returns>
        /// <param name="vStr">V string.</param>
        public static explicit operator TCAPIVersion(string vStr)
        {
            var knownVersions = Known;

            if (!knownVersions.ContainsKey(vStr))
            {
                throw new ArgumentException("Unrecognized version: " + vStr);
            }

            return knownVersions[vStr];
        }

        readonly string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TinCan.TCAPIVersion"/> class.
        /// </summary>
        /// <param name="value">Value.</param>
        TCAPIVersion(string value)
        {
            text = value;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.TCAPIVersion"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:TinCan.TCAPIVersion"/>.</returns>
        public override string ToString()
        {
            return text;
        }
    }
}
