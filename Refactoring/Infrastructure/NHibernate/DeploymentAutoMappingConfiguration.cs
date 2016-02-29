using System;
using FluentNHibernate.Automapping;
using Refactoring.Core;

namespace Refactoring.Infrastructure.NHibernate
{
    public class DeploymentAutoMappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            var shouldMap = type.Namespace == typeof(Basket).Namespace && type.Name != typeof(BaseEntity).Name;
            return shouldMap;
        }
    }
}