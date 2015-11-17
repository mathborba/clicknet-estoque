using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engefibra.Bll
{
    public static class Configuration
    {
        /// <summary>
        /// Recupera um valor da configuração do sistema
        /// </summary>
        /// <param name="key">Chave do valor</param>
        /// <returns></returns>
        public static string GetValue(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }
    }
}