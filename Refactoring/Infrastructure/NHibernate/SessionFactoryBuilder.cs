using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Refactoring.Core;

namespace Refactoring.Infrastructure.NHibernate
{
    public class SessionFactoryBuilder
    {
        public static ISessionFactory Build()
        {
            var deploymentAutoMappingConfiguration = new DeploymentAutoMappingConfiguration();

            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(builder => builder.FromConnectionStringWithKey("SOLID")))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Basket>(deploymentAutoMappingConfiguration)
                    .Conventions.Setup(x =>
                    {
                        x.Add(DefaultCascade.All());
                        x.Add<PrimaryKeyConvention>();
                        x.Add<DefaultStringLengthConvention>();
                        x.Add(ForeignKey.EndsWith("Id"));
                    })))
                    .ExposeConfiguration(DropAndCreate)
                .BuildSessionFactory();
        }

        private static void DropAndCreate(Configuration cfg)
        {
            new SchemaExport(cfg).Drop(false, true);
            new SchemaUpdate(cfg).Execute(false, true);
        }
    }
}