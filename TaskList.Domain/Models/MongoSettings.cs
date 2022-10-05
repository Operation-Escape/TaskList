using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Domain.Models
{
    public class MongoSettings
    {
        public string ConnectionString;

        public string Database;
    }
}
