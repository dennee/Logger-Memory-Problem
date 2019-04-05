using System;
using lmp.ProcessorWithProblem.DataProcessors;
using lmp.ProcessorWithProblem.FakeDataGenerator;
using Ninject.Modules;
using NLog;

namespace lmp.ProcessorWithProblem
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().ToMethod(ctx => LoggerFactory.Create(ctx.Request.Service.FullName)).InTransientScope();
            Bind<IFakeDataGenerator<string>>().To<StringFakeDataGenerator>().InSingletonScope();
            Bind<IDataProcessor<string, string>>().To<StringDataProcessor>().InTransientScope();
            Bind<IDataProcessor<string, int>>().To<StringLengthDataProcessor>().InTransientScope();
            Bind<DataProcessorFactory>().To<DataProcessorFactory>().InSingletonScope();
            Bind<ProcessorsManager>().To<ProcessorsManager>().InSingletonScope();
        }
    }
}
