using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace sdk_cloud_dfe
{
    public class Base
    {
        protected readonly Client _client;
        public Base(Dictionary<string, object> config)
        {

            var configClient = new Dictionary<string, object>
            {
                {"token", config["token"].ToString()},
                {"ambiente", config["ambiente"].ToString()},
                {"timeout", Convert.ToInt32(config["timeout"])},
                {"debug", Convert.ToBoolean(config["debug"])},
            };

            _client = new Client(configClient);

        }

        public string CheckKey(Dictionary<string, object> payload)
        {
            if (!payload.ContainsKey("chave"))
            {
                throw new ArgumentException("A chave não foi informada no payload.");
            }

            string key = Regex.Replace(payload["chave"].ToString(), @"[^0-9]", "");
            if (string.IsNullOrEmpty(key) || key.Length != 44)
            {
                throw new ArgumentException("A chave deve conter 44 dígitos numéricos.");
            }

            return key;
        }
    }
}