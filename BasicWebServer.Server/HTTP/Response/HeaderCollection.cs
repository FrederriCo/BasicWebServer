using System.Collections;
using System.Collections.Generic;

namespace BasicWebServer.Server.HTTP.Response
{
    public class HeaderCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers;

        public HeaderCollection() => headers = new Dictionary<string, Header>();
            
        public int Count => this.headers.Count;

        public void Add(string name, string value)
        {
            var header = new Header(name, value);

            this.headers.Add(name, header);
        }

        public IEnumerator<Header> GetEnumerator() => this.headers.Values.GetEnumerator();


        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
        
    }
}
