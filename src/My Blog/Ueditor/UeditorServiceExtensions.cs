using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Blog.Ueditor
{
    public static class UeditorServiceExtensions
    {
        public static IApplicationBuilder UseUeditor(this IApplicationBuilder app, Action<UeditorOptions> ueditorOptions)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            UeditorOptions option = new UeditorOptions();
            ueditorOptions.Invoke(option);
            Config.UeditorRootPath = option.UeditorRootPath;
            Config.UeditorUrlPrefix = option.UeditorUrlPrefix;

            return app.Map(Config.UeditorUrlPrefix, UeditorHandler);
        }

        public static IApplicationBuilder UseUeditor(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            UeditorOptions option = new UeditorOptions();
            Config.UeditorRootPath = option.UeditorRootPath;
            Config.UeditorUrlPrefix = option.UeditorUrlPrefix;
            return app.Map(Config.UeditorUrlPrefix, UeditorHandler);
        }

        private static void UeditorHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                Handler action = null;
                switch (context.Request.Query["action"])
                {
                    case "config":
                        action = new ConfigHandler(context);
                        break;
                    case "uploadimage":
                        action = new UploadHandler(context, new UploadConfig()
                        {
                            AllowExtensions = Config.GetStringList("imageAllowFiles"),
                            PathFormat = Config.GetString("imagePathFormat"),
                            SizeLimit = Config.GetInt("imageMaxSize"),
                            UploadFieldName = Config.GetString("imageFieldName")
                        });
                        break;
                    case "uploadscrawl":
                        action = new UploadHandler(context, new UploadConfig()
                        {
                            AllowExtensions = new string[] { ".png" },
                            PathFormat = Config.GetString("scrawlPathFormat"),
                            SizeLimit = Config.GetInt("scrawlMaxSize"),
                            UploadFieldName = Config.GetString("scrawlFieldName"),
                            Base64 = true,
                            Base64Filename = "scrawl.png"
                        });
                        break;
                    case "uploadvideo":
                        action = new UploadHandler(context, new UploadConfig()
                        {
                            AllowExtensions = Config.GetStringList("videoAllowFiles"),
                            PathFormat = Config.GetString("videoPathFormat"),
                            SizeLimit = Config.GetInt("videoMaxSize"),
                            UploadFieldName = Config.GetString("videoFieldName")
                        });
                        break;
                    case "uploadfile":
                        action = new UploadHandler(context, new UploadConfig()
                        {
                            AllowExtensions = Config.GetStringList("fileAllowFiles"),
                            PathFormat = Config.GetString("filePathFormat"),
                            SizeLimit = Config.GetInt("fileMaxSize"),
                            UploadFieldName = Config.GetString("fileFieldName")
                        });
                        break;
                    case "listimage":
                        action = new ListFileManager(context, Config.GetString("imageManagerListPath"), Config.GetStringList("imageManagerAllowFiles"));
                        break;
                    case "listfile":
                        action = new ListFileManager(context, Config.GetString("fileManagerListPath"), Config.GetStringList("fileManagerAllowFiles"));
                        break;
                    case "catchimage":
                        action = new CrawlerHandler(context);
                        break;
                    default:
                        action = new NotSupportedHandler(context);
                        break;
                }
                await action.Process();
            });
        }

    }
}
