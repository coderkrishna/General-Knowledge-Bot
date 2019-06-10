// <copyright file="ConfigurationCredentialProvider.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace GeneralKnowledgeBot
{
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Configuration;

    public class ConfigurationCredentialProvider : SimpleCredentialProvider
    {
        public ConfigurationCredentialProvider(IConfiguration configuration)
            : base(configuration["MicrosoftAppId"], configuration["MicrosoftAppPassword"])
        {
        }
    }
}