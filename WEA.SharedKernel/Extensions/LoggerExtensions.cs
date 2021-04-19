using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace WEA.SharedKernel.Extensions
{
    public static class LoggerExtensions
    {
        public static void DebugEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Debug);
        }
        public static void DebugEx(this ILogger logger, string description)
        {
            logger.LogEx(description, new { }, LogLevel.Debug);
        }
        public static void InfoEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Info);
        }
        public static void InfoEx(this ILogger logger, string description)
        {
            logger.LogEx(description, new { }, LogLevel.Info);
        }

        public static void ErrorEx(this ILogger logger, string description, Exception data)
        {
            ErrorEx(logger, data, description);
        }

        public static void ErrorEx(this ILogger logger, string description = "", object data = null)
        {
            logger.LogEx(description, data, LogLevel.Error);
        }
        public static void ErrorEx(this ILogger logger, Exception data, string description = "")
        {
            NLog
                .Fluent
                .Log
                .Error()
                .Message(data.Message ?? description)
                .Exception(data)
                .Property("ActivityId", Trace.CorrelationManager.ActivityId.ToHex())
                .Property("ProcessId", Process.GetCurrentProcess()?.Id)
                .Property("ManagedThreadId", Thread.CurrentThread?.ManagedThreadId)
                .LoggerName("ErrorChannel")
                .Write()
                ;
        }
        public static void FatalEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Fatal);
        }
        public static void FatalEx(this ILogger logger, string description)
        {
            logger.LogEx(description, new { }, LogLevel.Fatal);
        }
        public static void WarnEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Warn);
        }
        public static void WarnEx(this ILogger logger, string description)
        {
            logger.LogEx(description, new { }, LogLevel.Warn);
        }
        public static void TraceEx<T>(this ILogger logger, string description, T data)
        {
            logger.LogEx(description, data, LogLevel.Trace);
        }

        private static bool IsPrimitiveType<T>() => IsPrimitiveType(typeof(T));

        private static bool IsPrimitiveType(Type type)
        {
            if (type == typeof(int)) return true;
            if (type == typeof(int?)) return true;

            if (type == typeof(short)) return true;
            if (type == typeof(short?)) return true;

            if (type == typeof(long)) return true;
            if (type == typeof(long?)) return true;


            if (type == typeof(uint)) return true;
            if (type == typeof(uint?)) return true;

            if (type == typeof(byte)) return true;
            if (type == typeof(byte?)) return true;

            if (type == typeof(decimal)) return true;
            if (type == typeof(decimal?)) return true;

            if (type == typeof(DateTime)) return true;
            if (type == typeof(DateTime?)) return true;

            if (type == typeof(Guid)) return true;
            if (type == typeof(Guid?)) return true;

            if (type == typeof(string)) return true;

            return false;
        }


        public static void LogEx<T>(this ILogger logger, string description, T data, LogLevel level = null)
        {

            logger = logger ?? LogManager.GetCurrentClassLogger();
            if (logger == null) return;

            string ToStringEx(object x)
            {
                if (x == null) return string.Empty;
                var type = x.GetType();
                if (IsPrimitiveType(type))
                {
                    return x + string.Empty;
                }

                return Newtonsoft.Json.JsonConvert.SerializeObject(x);
            }

            if (data is Exception exception)
            {
                logger.Error(exception, description);
                return;
            }
            try
            {
                if (level == null) level = LogLevel.Debug;
                var result = NLog.Fluent.LoggerExtensions
                    .Info(logger)
                    .Level(level);

                var properties = GetProperties(data?.GetType()).ToArray();
                ;
                if (IsPrimitiveType<T>())
                {
                    result = result.Property(typeof(T).Name + ".Value", ToStringEx(data));
                }
                else
                {
                    foreach (var property in properties)
                    {
                        try
                        {
                            var propertyValue = property.GetValue(data);
                            result = result.Property(property.Name, ToStringEx(propertyValue));
                        }
                        catch (Exception e)
                        {
                            result = result.Property(property.Name, e.Message);
                        }
                    }

                    if (properties.Length == 0)
                    {
                        var settings = new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        };
                        var jObj = JObject.Parse(JsonConvert.SerializeObject(data, Formatting.None, settings));
                        foreach (var keyValue in jObj)
                        {
                            result = result.Property(keyValue.Key, keyValue.Value);
                        }
                    }
                }

                //Add Trace Identifier
                result = result.Property("ActivityId", Trace.CorrelationManager.ActivityId.ToHex());
                result = result.Property("ProcessId", Process.GetCurrentProcess()?.Id);
                result = result.Property("ManagedThreadId", Thread.CurrentThread?.ManagedThreadId);
                //result = result.Property("ManagedThreadName", Thread.CurrentThread?.Name);
                result = result.Property("Description", description)
                    .LoggerName("LogEx");
                result.Write();
            }
            catch
            {
                //Buna dəyməyin loq yaza bilmir cəhənnəmə yazsın
                //logger.Fatal(e, "LoggerExtensions.Exception.data:" + Newtonsoft.Json.JsonConvert.SerializeObject(data));
            }
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (type == null)
            {
                yield break;
            }
            while (type != null)
            {
                var props = type.GetProperties();
                foreach (var prop in props)
                {
                    yield return prop;
                }
                type = type.BaseType;
            }
        }

        private static string ToHex(this Guid guid)
        {
            return BitConverter.ToString(guid.ToByteArray()).Replace("-", "");
        }
    }
}
