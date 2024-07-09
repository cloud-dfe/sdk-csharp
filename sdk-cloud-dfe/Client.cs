using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace sdk_cloud_dfe
{
    public class Client
    {
        private readonly Service _service;
        private readonly string _token;
        private readonly string _ambiente;
        private readonly int _timeout;
        private readonly bool _debug;
        
        public Client(Dictionary<string, object> config)
        {
            if (!config.ContainsKey("token")) {
                throw new ArgumentException("O token não foi informado no processo.");
            }

            _token = config["token"].ToString();

            if (!config.ContainsKey("ambiente") || (config["ambiente"].ToString() != Consts.AMBIENTE_PRODUCAO && 
                 config["ambiente"].ToString() != Consts.AMBIENTE_HOMOLOGACAO))
            {
                throw new ArgumentException("O AMBIENTE deve ser 1-PRODUCÃO OU 2-HOMOLOGAÇÃO.");
            }

            _ambiente = config["ambiente"].ToString();
            
            _timeout = config.ContainsKey("timeout") ? Convert.ToInt32(config["timeout"]) : 60;
            _debug = config.ContainsKey("debug") ? Convert.ToBoolean(config["debug"]) : false;

            var baseURI = _ambiente == Consts.AMBIENTE_PRODUCAO ? Consts.URI["api"][Consts.AMBIENTE_PRODUCAO] : Consts.URI["api"][Consts.AMBIENTE_HOMOLOGACAO];
            var configService = new Dictionary<string, object>
            {
                {"token", _token},
                {"baseURI", baseURI},
                {"timeout", _timeout},
                {"debug", _debug},
            };

            _service = new Service(configService);

        }

        public async Task<Dictionary<string, object>> Send(string method, string route, Dictionary<string, object> payload)
        {
            try{
                var response = await _service.Request(method, route, payload);
                return response;
            }
            catch (Exception ex)
            {
                if (_debug)
                {
                    Console.WriteLine($"Erro ao enviar a requisição: {ex.Message}");
                }
                throw new ArgumentException("Falha de Comunicação.");
            }
        }
    }
}