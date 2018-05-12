using System.Diagnostics;

namespace Debugging
{
    public class DebugTools
    {
        /// <summary>
        /// Prints a message to th Unity console if the DEBUG preproc is defined in player settings
        /// </summary>
        /// <param name="message">Message to print</param>
        [Conditional("DEBUG")]
        public static void Log(string message)
        {
            //UnityEngine.Debug.Log(message);
        }

        /// <summary>
        /// Prints a message to th Unity console if the DEBUG preproc is defined in player settings
        /// </summary>
        /// <param name="message">Message to print</param>
        /// <param name="args">Arguments to insert into the message string using <see cref="string.Format(string, object[])"/></param>
        [Conditional("DEBUG")]
        public static void Log(string message, params object[] args)
        {
            message = string.Format(message, args);
            UnityEngine.Debug.Log(message);
        }

        /// <summary>
        /// Prints a warning to the Unity console if the DEBUG preproc is defined in player settings
        /// </summary>
        /// <param name="message">Warning to print</param>
        [Conditional("DEBUG")]
        public static void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        /// <summary>
        /// Prints a warning to the Unity console if the DEBUG preproc is defined in player settings
        /// </summary>
        /// <param name="message">Warning to print</param>
        /// <param name="args">Arguments to insert into the message string using <see cref="string.Format(string, object[])"/></param>
        [Conditional("DEBUG")]
        public static void LogWarning(string message, params object[] args)
        {
            message = string.Format(message, args);
            LogWarning(message);
        }


        /// <summary>
        /// Prints an error to the Unity console if the LOGS_ENABLED preproc is defined in player settings or if you are in the Unity Editor
        /// </summary>
        /// <param name="message">Error to print</param>
        public static void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }

        /// <summary>
        /// Prints an error to th Unity console if the LOGS_ENABLED preproc is defined in player settings or if you are in the Unity Editor
        /// </summary>
        /// <param name="message">Error to print</param>
        /// <param name="args">Arguments to insert into the message string using <see cref="string.Format(string, object[])"/></param>
        public static void LogError(string message, params object[] args)
        {
            message = string.Format(message, args);
            LogError(message);
        }

        /// <summary>
        /// Asserts that the expression provided is true. If it is false the error message will be printed
        /// </summary>
        /// <param name="expression">Expression to assert</param>
        /// <param name="errorMessage">Message to log in the event of failed assertion</param>
        /// <remarks>UNITY_ASSERTIONS is defined by the Unity Engine and does not need to be defined by us</remarks>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(bool expression, string errorMessage)
        {
            Debug.Assert(expression, errorMessage);
        }


        /// <summary>
        /// Asserts that the expression provided is true. If it is false the error message will be printed
        /// </summary>
        /// <param name="expression">Expression to assert</param>
        /// <param name="errorMessage">Message to log in the event of failed assertion</param>
        /// <param name="args">Arguments to insert into the Message string using <see cref="string.Format(string, object[])"/></param>
        /// <remarks>UNITY_ASSERTIONS is defined by the Unity Engine and does not need to be defined by us</remarks>
        [Conditional("UNITY_ASSERTIONS")]
        public static void Assert(bool expression, string errorMessage, params object[] args)
        {
            errorMessage = string.Format(errorMessage, args);
            Assert(expression, errorMessage);
        }
    }
}