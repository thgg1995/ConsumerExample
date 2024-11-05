using Caixa.Consumer.Application.Interfaces;
using Caixa.Consumer.Domain.Repositories;
using Caixa.Consumer.Infrastructure.Repositories.Messaging;
using Caixa.Consumer.Application.Services;
using Caixa.Consumer.Infrastructure.Messaging.Consumers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Caixa.Consumer.Domain.Entities;
using Amazon.SQS;
using Amazon.Extensions.NETCore.Setup;
using Amazon;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<KafkaConfiguration>(context.Configuration.GetSection("Kafka"));

        services.AddAWSService<IAmazonSQS>(new AWSOptions
        {
            Region = RegionEndpoint.SAEast1
        });

        services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
        services.AddSingleton<ISQSRepository, SQSRepository>();

        services.AddHostedService<EventClasseService>();
        services.AddHostedService<EventSubClasseService>();
    })
    .Build();

await host.RunAsync();
