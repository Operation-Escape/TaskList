using Microsoft.Extensions.Configuration;

namespace TaskList.Shared.Common.Swagger
{
    public class ConfigModel
    {
        public SwaggerOptions SwaggerOptions { get; set; }
        public static ConfigModel Bind(IConfiguration config)
        {
            var conf = new ConfigModel();
            config.Bind(conf);
            return conf;
        }
    }

    public class SwaggerOptions
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UIEndpoint { get; set; }
    }
}
