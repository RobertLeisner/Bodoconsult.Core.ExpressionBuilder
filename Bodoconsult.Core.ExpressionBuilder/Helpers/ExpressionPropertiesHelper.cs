using System;
using Bodoconsult.Core.ExpressionBuilder.Interfaces;
using Bodoconsult.Core.ExpressionBuilder.Tools;

namespace Bodoconsult.Core.ExpressionBuilder.Helpers
{

    /// <summary>
    /// 
    /// </summary>
    public static class ExpressionPropertiesHelper
    {

        public static  IExpressionPropertyCollection LoadPropertyCollection<TItemType>()
        {
            //return _properties = new ExpressionPropertyCollection(type, null);

            var genericType = typeof(ExpressionPropertyCollection<>);
            Type[] typeArgs = { typeof(TItemType) };
            var repositoryType = genericType.MakeGenericType(typeArgs);
             
            return (IExpressionPropertyCollection)Activator.CreateInstance(repositoryType);
        }


        public static IExpressionPropertyCollection LoadPropertyCollection(Type type)
        {
            //return _properties = new ExpressionPropertyCollection(type, null);

            var genericType = typeof(ExpressionPropertyCollection<>);
            Type[] typeArgs = { type };
            var repositoryType = genericType.MakeGenericType(typeArgs);

            return (IExpressionPropertyCollection)Activator.CreateInstance(repositoryType);
        }

        //public static IPropertyCollection LoadProperties(Type type)
        //{
        //    return _properties = new PropertyCollection(type, Resources.Person.ResourceManager);
        //}
    }
}
