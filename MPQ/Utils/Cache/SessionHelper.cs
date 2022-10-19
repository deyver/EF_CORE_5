using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPQ.Utils.Cache
{
    public class SessionHelper : ISessionHelper
    {
        private readonly ISession _session;
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext != null)
                this._session = httpContextAccessor.HttpContext.Session;
        }

        public void Adicionar(string key, object valor)
        {
            var _possuiKey = this._session.Keys.Count(i => i == key) > 0;

            if (_possuiKey)
                this._session.Remove(key: key);

            string valorParsed = JsonConvert.SerializeObject(valor);

            this._session.SetString(key: key, value: valorParsed);

        }

        public T RecuperarValor<T>(string key) where T : class
        {
            var _v = this._session.GetString(key: key);

            if (!string.IsNullOrEmpty(_v))
            {
                return JsonConvert.DeserializeObject<T>(_v);
            }
            return null;
        }
    }
}
