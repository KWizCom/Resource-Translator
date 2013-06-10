using System;
using System.ComponentModel;
using System.Diagnostics;

namespace KWizCom.ResourceTranslator.Utilities
{
    /// <summary>
    /// Summary description for SimpleEventLog.
    /// </summary>
    internal class SimpleEventLog : EventLog
    {
        /// as can't tell if there is an error or how to return it 
        /// set up a simple error reporting system

        private string strError;
        private bool bError = false;       

        /// <summary>
        /// get or set the error string
        /// </summary>
        public string Error
        {
            get
            {
                return strError;
            }
            set
            {
                strError = value;
            }
        }

        /// <summary>
        /// check if there has been an error
        /// </summary>
        public bool IsError
        {
            get
            {
                return bError;
            }
            set
            {
                bError = value;
            }
        }        

        /// <summary>
        /// One argument constructor
        /// Creates the eventlog specified if necassary
        /// </summary>
        /// <param name="eventLogName"></param>
        public SimpleEventLog(string eventLogName)
        {
            try
            {
                if (EventLog.SourceExists(eventLogName) == false)
                {
                    /// create the event log on the current computer
                    try
                    {
                        EventLog.CreateEventSource(eventLogName, "KWizCom");
                        Source = eventLogName;                        
                    }
                    catch (ArgumentException)
                    {
                        IsError = true;
                        Error = "The string passed to CreateEventSource is null";
                    }
                    catch (Exception)
                    {
                        IsError = true;
                        Error = "The system could not open the registry key for " + eventLogName;
                    }
                }
                else
                {
                    Source = eventLogName;                    
                }
            }
            catch
            {
                IsError = true;                
            }
        }

        /// <summary>
        /// Second constructor takes two parameters 
        /// Note the use of the contrustor intialiser that calls the one argument
        /// constructor first
        /// The second parameter gives the option to clear the event log on start up
        /// </summary>
        /// <param name="eventLogName"></param>
        /// <param name="clear"></param>
        public SimpleEventLog(string eventLogName, bool clear)
            : this(eventLogName)
        {
            if (clear == true)
            {
                ClearSimpleEventLog();
            }
        }


        /// <summary>
        /// Write an entry into the log
        /// </summary>
        /// <param name="entry">Text to be written to the log</param>
        /// <returns>false on error</returns>
        private bool WriteSimpleEntry(string entry, EventLogEntryType eventType)
        {
            IsError = false;
            try
            {
                WriteEntry(entry, eventType);
            }
            catch (ArgumentException argExp)
            {
                IsError = true;
                Error = "Argument exception is " + argExp.Message;
            }
            catch (InvalidOperationException)
            {
                IsError = true;
                Error = "You do not have write permission for the event log";
            }
            catch (Win32Exception winExp)
            {
                IsError = true;
                Error = "Win32Exception is " + winExp.Message;
            }
            catch (SystemException)
            {
                IsError = true;
                Error = "The event log could not be notified to start recieing events";
            }
            catch (Exception)
            {
                IsError = true;
                Error = "The registry entry for the log could not be opened on the remote computer";
            }


            if (IsError == true)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Write an information entry into the event log
        /// </summary>
        /// <param name="information">Information to be written to the event log</param>
        /// <returns>true if written to the event log</returns>
        public bool WriteInformation(string information)
        {
            return WriteSimpleEntry(information, EventLogEntryType.Information);
        }

        /// <summary>
        /// Write a warning to the current event log
        /// </summary>
        /// <param name="warning">warning to be written to the event log</param>
        /// <returns>true on success</returns>
        public bool WriteWarning(string warning)
        {
            return WriteSimpleEntry(warning, EventLogEntryType.Warning);
        }

        /// <summary>
        /// Write an error to the current event log
        /// </summary>
        /// <param name="error">error to be written to the event log</param>
        /// <returns>true on success</returns>
        public bool WriteError(string error)
        {
            return WriteSimpleEntry(error, EventLogEntryType.Error);
        }

        /// <summary>
        /// clear all the data out of the current event log
        /// </summary>
        /// <returns>false on error </returns>
        public bool ClearSimpleEventLog()
        {
            IsError = false;
            try
            {
                Clear();
            }
            catch (Win32Exception winExp)
            {
                IsError = true;
                Error = "Win 32 Exception " + winExp.Message;
            }
            catch (ArgumentException)
            {
                IsError = true;
                Error = "The log name is empty";
            }
            catch (Exception)
            {
                IsError = true;
                Error = "The log could not be opened";
            }

            if (IsError == true)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Remove the current event log and source from the system
        /// </summary>
        /// <param name="eventLogName">Name of the event log to be deleted</param>
        /// <returns>false if error</returns>
        public bool DeleteSimpleEventLog()
        {
            IsError = false;

            try
            {
                EventLog.Delete(Source);
            }
            catch (ArgumentException)
            {
                IsError = true;
                Error = "Event Log Name is null";
            }
            catch (SystemException sysExp)
            {
                IsError = true;
                Error = "System Exception " + sysExp;
            }

            if (IsError == true)
                return false;
            else
                return true;
        }
    }
}
