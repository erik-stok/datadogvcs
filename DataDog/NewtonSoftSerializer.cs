using System;
using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace DataDog
{
    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {
        private static NewtonsoftJsonSerializer _default;

        private readonly Newtonsoft.Json.JsonSerializer _serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                if (_default != null)
                    return _default;

                _default = new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                return _default;
            }
            set
            {
                if (value == null)
                    throw new NullReferenceException(nameof(value));

                _default = value;
            }
        }

        public string ContentType { get; set; } = "application/json";
        public string DateFormat { get; set; }
        public string Namespace { get; set; }
        public string RootElement { get; set; }

        public virtual string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    _serializer.Serialize(jsonTextWriter, obj);

                    return stringWriter.ToString();
                }
            }
        }

        public virtual T Deserialize<T>(IRestResponse response)
        {
            using (var stringReader = new StringReader(response.Content))
            using (var jsonTextReader = new JsonTextReader(stringReader))
                return _serializer.Deserialize<T>(jsonTextReader);
        }
    }
}