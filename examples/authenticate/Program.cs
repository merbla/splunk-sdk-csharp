﻿/*
 * Copyright 2013 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

namespace Splunk.Examples.Authenticate
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to authenticate to the server 
    /// and print the received token.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        public static void Main()
        {
            // Load connection info for Splunk server in .splunkrc file.
            var cli = Command.Splunk("authenticate");

            var service = Service.Connect(cli.Opts);

            System.Console.WriteLine("Token: ");
            System.Console.WriteLine("    " + service.Token);
        }
    }
}