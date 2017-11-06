using RestSharp;

namespace OiWeb.WebAPI.Handle
{
    public class RequestHandle
    {
        public object Get(string url, string requestUrl)
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestUrl, Method.GET);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }

        public object Post(object parameters, string url, string requestUrl, string tokenValue, string tokenName)
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestUrl, Method.POST);

            if (!string.IsNullOrEmpty(tokenValue))
                request.AddHeader(tokenName, tokenValue);

            request.AddHeader("Content-Type", "application/json");

            if (parameters != null)
                request.AddJsonBody(parameters);

            IRestResponse response = client.Execute(request);
            return response.Content;
        }

    }
}