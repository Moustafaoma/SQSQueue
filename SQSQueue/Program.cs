namespace SQSQueue;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;

public class Program
{
    static async Task Main(string[] args) 
    {
        var sqsClient = new AmazonSQSClient(); 

        var customer = new CustomerData
        {
            Id = 8,
            Email = "mohamed.omar@example.com",
            Name = "Mohamed",
            CreatedAt = DateTime.UtcNow
        };

        var queueUrlClient = await sqsClient.GetQueueUrlAsync("Customers"); 

        var sendMessageRequest = new SendMessageRequest()
        {
            QueueUrl = queueUrlClient.QueueUrl, 
            MessageBody = JsonSerializer.Serialize(customer), 
        };

        var response = await sqsClient.SendMessageAsync(sendMessageRequest); 

        Console.WriteLine($"✅ Message Sent! MessageId: {response.MessageId}");
    }
}

