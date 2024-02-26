using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ApiTestingDemo.InsuranceApi.Data;

internal class DateOnlySerializer : StructSerializerBase<DateOnly>
{
    private static readonly TimeOnly zeroTimeComponent = new();

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateOnly value)
    {
        var dateTime = value.ToDateTime(zeroTimeComponent);
        var ticks = BsonUtils.ToMillisecondsSinceEpoch(dateTime);
        context.Writer.WriteDateTime(ticks);
    }

    public override DateOnly Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var ticks = context.Reader.ReadDateTime();
        var dateTime = BsonUtils.ToDateTimeFromMillisecondsSinceEpoch(ticks);
        return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}