﻿using Core.Business;
using Core.Business.Translate;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.SeriLog.Loggers;
using Core.Utilities.Constants;
using Core.Utilities.Helpers.MailHelpers;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<Stopwatch>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<IRequestUserService, RequestUserManager>();
            serviceCollection.AddSingleton<ITranslateContext, TranslateContext>();
            serviceCollection.AddSingleton<IMailHelper, SmtpMailHelper>();
            serviceCollection.AddSingleton<CoreMessages>();
            serviceCollection.AddTransient<FileLogger>();
            serviceCollection.AddTransient<MsSqlLogger>();
        }
    }
}