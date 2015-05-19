﻿using Microsoft.SharePoint.Client;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HarshPoint
{
    internal static class ClientObjectExtensions
    {
        public static Boolean IsNull(this ClientObject clientObject)
        {
            if (clientObject == null)
            {
                return true;
            }

            if (clientObject.ServerObjectIsNull.HasValue)
            {
                return clientObject.ServerObjectIsNull.Value;
            }

            return false;
        }

        public static Boolean IsPropertyAvailable<T>(this T clientObject, Expression<Func<T, Object>> expression)
            where T : ClientObject
        {
            if (clientObject == null)
            {
                throw Error.ArgumentNull(nameof(clientObject));
            }

            if (expression == null)
            {
                throw Error.ArgumentNull(nameof(expression));
            }

            return clientObject.IsPropertyAvailable(expression.GetMemberName());
        }

        public static async Task<TResult> EnsurePropertyAvailable<T, TResult>(this T clientObject, Expression<Func<T, TResult>> expression)
            where T : ClientObject
        {
            if (clientObject == null)
            {
                throw Error.ArgumentNull(nameof(clientObject));
            }

            if (expression == null)
            {
                throw Error.ArgumentNull(nameof(expression));
            }

            var objectExpression = expression.ConvertToObject();
            var compiledExpression = expression.Compile();

            if (!IsPropertyAvailable(clientObject, objectExpression))
            {
                clientObject.Context.Load(clientObject, objectExpression);
                await clientObject.Context.ExecuteQueryAsync();
            }

            return compiledExpression(clientObject);
        }
    }
}