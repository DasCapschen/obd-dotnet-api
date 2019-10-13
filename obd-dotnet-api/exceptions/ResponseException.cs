/**
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */

/**
 * Generic message error
 *
 */

using System;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace obd_dotnet_api.exceptions
{
    public class ResponseException : Exception 
    {

        private readonly string _message;
        private string _response;
        private string _command;
        private readonly bool _matchRegex;

        /**
         * <p>Constructor for ResponseException.</p>
         *
         * @param message a {@link java.lang.String} object.
         */
        protected ResponseException(string message) 
        {
            _message = message;
        }

        /**
         * <p>Constructor for ResponseException.</p>
         *
         * @param message a {@link java.lang.String} object.
         * @param matchRegex a boolean.
         */
        protected ResponseException(string message, bool matchRegex) 
        {
            _message = message;
            _matchRegex = matchRegex;
        }

        private static string Clean(string s) 
        {
            
            return s == null ? "" : Regex.Replace(s, "\\s", "").ToUpper();
        }

        /**
         * <p>isError.</p>
         *
         * @param response a {@link java.lang.String} object.
         * @return a boolean.
         */
        public bool IsError(string response) 
        {
            _response = response;
            if (_matchRegex)
            {
                return Regex.IsMatch(Clean(response), Clean(_message));
            } 
            else 
            {
                return Clean(response).Contains(Clean(_message));
            }
        }

        /**
         * <p>Setter for the field <code>command</code>.</p>
         *
         * @param command a {@link java.lang.String} object.
         */
        public string Command
        {
            private get => _command;
            set => _command = value;
        }

        public override string Message => "Error running " + _command + ", response: " + _response;
    }
}
