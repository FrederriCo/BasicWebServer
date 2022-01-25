
using BasicWebServer.Server.Common;

namespace BasicWebServer.Server.HTTP.Response
{
    public class Header
    {
        public const string ContentType = "Content-Type";
        public const string ContentLength = "Content-Length";
        public const string Date = "Date";
        public const string Location = "Location";
        public const string Server = "Server";
        public Header(string _name, string _value)
        {
            Guard.AgainstNull(_name, nameof(_name));
            Guard.AgainstNull(_value, nameof(_value));

            this.Name = _name;
            this.Value = _value;
        }

        public string Name { get; init; }
        public string Value { get; init; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
