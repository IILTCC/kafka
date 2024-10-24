using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System.Threading;

namespace KafkaMain
{
    public static class ServerSettings
    {
        private const string KAFKA_URL = "localhost:9092";
        public static void AddSettings()
        {
            string[] topicNames = new string[4] { "FlightBoxDownIcd","FlightBoxUpIcd","FiberBoxUpIcd","FiberBoxDownIcd"};
            AdminClientConfig config = new AdminClientConfig
            {
                BootstrapServers = KAFKA_URL
            };
            IAdminClient adminClient = new AdminClientBuilder(config).Build();
            List<string> allTopics = (from i in WaitForKafka(adminClient) select i.Topic).ToList(); 

            //adminClient.DeleteTopicsAsync(allTopics.ToArray());
            
            CreateTopics(allTopics, adminClient,topicNames);
        }
        private static void CreateTopics(List<string>topicList,IAdminClient adminClient,string[] topicNames)
        {
            foreach (string topic in topicNames)
            {
                if (topicList.Contains(topic))
                    continue;

                TopicSpecification topicSpecification = new TopicSpecification
                {
                    Name = topic,
                    NumPartitions = 1,
                };

                try
                {
                    adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSpecification }).Wait();
                }
                catch (Exception e)
                { 
                }
            }
        }
        private static List<TopicMetadata> WaitForKafka(IAdminClient adminClient)
        {
            bool isKafkaRunning = false;
            // run until we get message back from kafka meaning its running
            while(!isKafkaRunning)
            { 
                try
                {
                    var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));

                    isKafkaRunning = true; 
                    return metadata.Topics;
                    
                }
                catch (KafkaException e)
                {
                }
                catch (Exception ex)
                {
                }
            }
            return null;
        }
            
    }
}
