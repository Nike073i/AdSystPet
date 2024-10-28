using MongoDB.Bson.Serialization;
using AdSyst.Orders.DAL.MongoDb.Enums;
using AdSyst.Orders.DAL.MongoDb.Interfaces;

namespace AdSyst.Orders.DAL.MongoDb.Models.OrderStates
{
    public class OrderStateConverter : IBsonSerializer<IOrderState>
    {
        public Type ValueType => typeof(IOrderState);

        public static IOrderState ConvertToStatus(OrderStatus orderStatus) =>
            orderStatus switch
            {
                OrderStatus.Created => new CreatedState(),
                OrderStatus.Handled => new HandledState(),
                OrderStatus.Sended => new SentState(),
                OrderStatus.Received => new ReceivedState(),
                OrderStatus.Canceled => new CanceledState(),
                _ => throw new NotImplementedException(),
            };

        public static IOrderState ConvertToStatus(int stateCode) =>
            Enum.IsDefined(typeof(OrderStatus), stateCode)
                ? ConvertToStatus((OrderStatus)stateCode)
                : throw new InvalidCastException();

        public IOrderState Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        )
        {
            int stateCode = context.Reader.ReadInt32();
            return ConvertToStatus(stateCode);
        }

        public void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            IOrderState value
        )
        {
            int stateCode = (int)value.Status;
            context.Writer.WriteInt32(stateCode);
        }

        public void Serialize(
            BsonSerializationContext context,
            BsonSerializationArgs args,
            object value
        )
        {
            if (value is IOrderState state)
                Serialize(context, args, state);
            else
                throw new InvalidCastException();
        }

        object IBsonSerializer.Deserialize(
            BsonDeserializationContext context,
            BsonDeserializationArgs args
        ) => Deserialize(context, args);
    }
}
