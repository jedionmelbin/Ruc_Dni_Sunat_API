using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunat.WebApi.Models
{
    public class WebHelper
    {
        public static string ConcatParams(Dictionary<string, object> parameters)
        {
            bool FirstParam = true;
            StringBuilder parametros = null;

            if (parameters != null)
            {
                parametros = new StringBuilder();

                foreach (KeyValuePair<string, dynamic> param in parameters)
                {
                    string tipo = param.Value.GetType().ToString();

                    parametros.Append(FirstParam ? "" : "&");
                    parametros.Append(param.Key + "=" + param.Value);
                    FirstParam = false;

                }
            }

            return parametros.ToString();
        }
    }
}
