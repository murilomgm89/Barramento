using System.Configuration;

namespace OiWeb.WebAPI.Handle
{
    public class ApiHandleForViability : RequestHandle
    {   
        private readonly string _urlHost06 = ConfigurationManager.AppSettings["viabilidade.url.token"];
        private readonly string _urlHost07 = ConfigurationManager.AppSettings["viabilidade.url"];
        private readonly string _serviceGetToken = ConfigurationManager.AppSettings["viabilidade.request.GetToken"];
        private readonly string _serviceLogradouro = ConfigurationManager.AppSettings["viabilidade.request.ConsultarLogradouro"];
        private readonly string _serviceViabilidadeVelox = ConfigurationManager.AppSettings["viabilidade.request.ConsultarViabilidadeVelox"];

        public string GetToken(object parameters)
        {
            var response = Post(parameters, _urlHost06, _serviceGetToken, null, null);
            return response.ToString();
        }

        public string ConsultarLogradouro(object parameters)
        {
            var response = Post(parameters, _urlHost07, _serviceLogradouro, null, null);
            return response.ToString();
        }

        public string ConsultarViabilidadeVelox(object parameters)
        {
            var response = Post(parameters, _urlHost07, _serviceViabilidadeVelox, null, null);
            return response.ToString();
        }
    }
}