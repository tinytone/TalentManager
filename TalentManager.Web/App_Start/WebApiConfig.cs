﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;

using Newtonsoft.Json.Serialization;

namespace TalentManager.Web
{
    public static class WebApiConfig
    {

        //// ----------------------------------------------------------------------------------------------------------
		 
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var xmlMediaTypeFormatter = config.Formatters.OfType<XmlMediaTypeFormatter>().First();
            config.Formatters.Remove(xmlMediaTypeFormatter);
        }

        //// ----------------------------------------------------------------------------------------------------------
    }
}
