﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAspect.Patterns.Logging
{
    /// <summary>
    /// Base class for all logging backend implementation
    /// </summary>
    public abstract class LoggingBackend
    {
        /// <summary>
        /// Log message using the given parameters
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="logType">Logging Type</param>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        /// <param name="args">Arguments</param>
        public abstract void Log(MethodContext context, LoggingType logType, string message, System.Exception ex, params object[] args);
    }
}
