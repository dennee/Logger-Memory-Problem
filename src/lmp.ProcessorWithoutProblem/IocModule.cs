﻿using System;
using lmp.UrlsProcessorWithProblem.DataProcessors;
using lmp.UrlsProcessorWithProblem.FakeDataGenerator;
using Ninject.Modules;
using NLog;

namespace lmp.UrlsProcessorWithProblem
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IFakeDataGenerator<string>>().To<StringFakeDataGenerator>().InSingletonScope();
            Bind<IDataProcessor<string, string>>().To<StringDataProcessor>().InTransientScope();
            Bind<IDataProcessor<string, int>>().To<StringLengthDataProcessor>().InTransientScope();
            Bind<DataProcessorFactory>().To<DataProcessorFactory>().InSingletonScope();
            Bind<ProcessorsManager>().To<ProcessorsManager>().InSingletonScope();
        }
    }
}
