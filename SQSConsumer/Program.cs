using Amazon.SQS;
using Amazon.SQS.Model;
using SQSConsumer;
using System.Text.Json;

namespace SQSConsumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cts=new CancellationTokenSource();
            var sqsClient = new AmazonSQSClient();
            var queueUrlResponse = await sqsClient.GetQueueUrlAsync("Customers");
            var recieveMessageRequest = new ReceiveMessageRequest()
            {
                QueueUrl = queueUrlResponse.QueueUrl,

            };

            while (!cts.IsCancellationRequested)
            {
                var response = await sqsClient.ReceiveMessageAsync(recieveMessageRequest,cts.Token);
                foreach (var message in response.Messages) {
                    Console.WriteLine(message.MessageId);
                    Console.WriteLine(message.Body);
                    await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl,message.ReceiptHandle);
                }
                await Task.Delay(3000);
            }
        }
    }
}
