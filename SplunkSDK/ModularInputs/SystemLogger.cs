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

namespace Splunk.ModularInputs
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///     List of appropriate log levels for logging functions
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     Debug Messages
        /// </summary>
        Debug,

        /// <summary>
        ///     Informational Messages
        /// </summary>
        Info,

        /// <summary>
        ///     Warning Messages
        /// </summary>
        Warn,

        /// <summary>
        ///     Error Messages
        /// </summary>
        Error,

        /// <summary>
        ///     Fatal Error Messages
        /// </summary>
        Fatal
    }

    /// <summary>
    ///     The SystemLogger class is used to hold all the specifics for logging in a Splunk Modular Input
    /// </summary>
    public static class SystemLogger
    {
        /// <summary>
        ///     Private method that converts a Level into a string
        /// </summary>
        /// <param name="level">The log level to convert</param>
        /// <returns>The string representation of the log level</returns>
        private static string GetLevelString(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Info:
                    return "INFO";
                case LogLevel.Warn:
                    return "WARN";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Fatal:
                    return "FATAL";
                default:
                    Debug.Assert(false, "Unexpected trace level.");
                    return "ERROR";
            }
        }

        /// <summary>
        ///     Convenience method to write log messages to splunkd.log
        /// </summary>
        /// <param name="msg">The message</param>
        public static void Write(string msg)
        {
            Write(LogLevel.Info, msg);
        }

        /// <summary>
        ///     Convenience method to write log messages to splunkd.log
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="msg">The message</param>
        public static void Write(LogLevel level, string msg)
        {
            var line = Format(level, msg);
            Console.Error.WriteLine(line);
            Console.Error.Flush();
        }

        /// <summary>
        /// Format trace
        /// </summary>
        /// <param name="level">The log level</param>
        /// <param name="msg">The message</param>
        /// <returns>A line</returns>
        internal static string Format(LogLevel level, string msg)
        {
            var line = string.Format("{0} {1}", GetLevelString(level), msg);
            return line;
        }
    }
}