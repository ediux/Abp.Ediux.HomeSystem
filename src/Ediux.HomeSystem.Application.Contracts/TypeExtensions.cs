﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Ediux.HomeSystem
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class TypeExtensions
    {
        internal static readonly Type[] PredefinedTypes =
        {
            typeof(object),
            typeof(bool),
            typeof(char),
            typeof(string),
            typeof(sbyte),
            typeof(byte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime),
            typeof(TimeSpan),
            typeof(Guid),
            typeof(Math),
            typeof(Convert)
        };

        public static bool IsPredefinedType(this Type type)
        {
            return PredefinedTypes.Any(left => left == type);
        }

        public static string FirstSortableProperty(this Type type)
        {
            var propertyInfo = type.GetRuntimeProperties().FirstOrDefault(property => property.PropertyType.IsPredefinedType());

            if (propertyInfo == null)
            {
                throw new NotSupportedException("CannotFindPropertyToSortBy");
            }

            return propertyInfo.Name;
        }

        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsConstructedGenericType && conversionType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (value == null)
                {
                    return null;
                }

                var nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }

            return Convert.ChangeType(value, conversionType);
        }

        public static bool SameTypes<TEntity, TViewModel>()
        {
            return typeof(TEntity) == typeof(TViewModel);
        }
    }
}
