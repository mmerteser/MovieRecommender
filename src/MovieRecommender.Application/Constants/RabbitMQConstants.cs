namespace MovieRecommender.Application.Constants
{
    public class RabbitMQConstants
    {
        public const string Host = "localhost";
        public const string DefaultExchangeType = "direct";

        public const string MovieEmailExchangeName = "EmailExchange";
        public const string MovieEmailQueueName = "EmailQueue";
    }
}
