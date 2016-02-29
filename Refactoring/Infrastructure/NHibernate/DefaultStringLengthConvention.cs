using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Refactoring.Infrastructure.NHibernate
{
    public class DefaultStringLengthConvention
        : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.Length(250);
        }
    }
}