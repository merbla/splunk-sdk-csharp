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

namespace Splunk.Examples.Search
{
    using System;
    using System.Threading;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to perform a normal search.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        /// <param name="argv">The command line arguments</param>
        public static void Main(string[] argv) 
        {
            // Load connection info for Splunk server in .splunkrc file.
            var cli = Command.Splunk("search");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(argv);
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine("Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            var service = Service.Connect(cli.Opts);
            var jobs = service.GetJobs();
            var job = jobs.Create((string)cli.Opts["search"]);
            while (!job.IsDone) 
            {
                Thread.Sleep(1000);
            }

            var outArgs = new JobResultsArgs
            {
                OutputMode = JobResultsArgs.OutputModeEnum.Xml,

                // Return all entries.
                Count = 0
            };

            using (var stream = job.Results(outArgs))
            {
                using (var rr = new ResultsReaderXml(stream))
                {
                    foreach (var @event in rr)
                    {
                        System.Console.WriteLine("EVENT:");
                        foreach (string key in @event.Keys)
                        {
                            System.Console.WriteLine("   " + key + " -> " + @event[key]);
                        }
                    }
                }
            }
        }
    }
}
