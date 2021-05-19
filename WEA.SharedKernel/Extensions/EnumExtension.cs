﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace WEA.SharedKernel.Extensions
{
    public static class EnumExtension
    {
        private static string LookupResource(IReflect resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    var resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }
            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            if (fieldInfo == null) return Enum.GetName(type, value);

            if (!(fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) is DisplayAttribute[] descriptionAttributes)
                || descriptionAttributes.Length == 0) return value.ToString();

            if (descriptionAttributes[0].ResourceType != null)
                return LookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }

        // Returns display name of given value
        public static string GetDisplayName(this string enumValue, Type whereToSearch)
        {
            var fields = whereToSearch.GetFields();

            if (!fields.Any())
                return enumValue;

            foreach (var field in fields)
            {
                if (field.Name != enumValue)
                    continue;

                if (field.Name != enumValue || !(field.GetCustomAttributes(typeof(DisplayAttribute)) is DisplayAttribute[] displayAttributes)
                    || displayAttributes.Length == 0)
                    continue;

                if (displayAttributes[0].ResourceType == null)
                    return displayAttributes[0].Name;

                return LookupResource(displayAttributes[0].ResourceType, displayAttributes[0].Name);
            }

            return enumValue;
        }


        public static TEnum ToEnum<TEnum>(this string value)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
                return default(TEnum);

            return Enum.TryParse<TEnum>(value, true, out var result) ? result : default(TEnum);
        }

        public static TEnum GetEnumValueByDisplayName<TEnum>(this string value)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException();

            var type = typeof(TEnum);
            if (!type.IsEnum) throw new InvalidOperationException();

            var fields = type.GetFields();

            foreach (var field in fields)
            {
                var attribute = field.GetCustomAttribute(
                    typeof(DisplayAttribute)) as DisplayAttribute;
                if (attribute != null)
                {
                    var resource = LookupResource(attribute.ResourceType, attribute.Name);
                    if (resource == value)
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == value)
                        return (TEnum)field.GetValue(null);
                }
            }

            throw new ArgumentOutOfRangeException("value");

        }
    }
}
