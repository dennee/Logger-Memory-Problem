using System;
using lmp.UrlsProcessorWithProblem.FakeDataGenerator;
using Ninject.Modules;
using NLog;

namespace lmp.UrlsProcessorWithProblem
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(ctx => LoggerFactory.Create(ctx.Request.Service.FullName)).InTransientScope();
            Bind<IFakeDataGenerator<string>>().To<StringFakeDataGenerator>().InSingletonScope();
        }
    }
}
