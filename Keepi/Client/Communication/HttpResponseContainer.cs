namespace Keepi.Client.Communication
{
    public class HttpResponseContainer<T>
    {
        public HttpResponseContainer(T responce, bool success, HttpResponseMessage httpResponseMessage)
        {
            Responce = responce;
            Success = success;
            HttpResponseMessage = httpResponseMessage;
        }

        public T Responce { get; }

        public bool Success { get; }

        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string> GetBody() => await HttpResponseMessage.Content.ReadAsStringAsync();
    }

}
