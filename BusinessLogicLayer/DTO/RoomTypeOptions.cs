using System.Runtime.Serialization;

namespace BusinessLogicLayer.DTO
{
    public enum RoomTypeOptions
    {
        [EnumMember(Value = "Standard Room")]
        StandardRoom,
        [EnumMember(Value = "Deluxe Suite")]
        DeluxeSuite,
        [EnumMember(Value = "Executive Suite")]
        ExecutiveSuite,
        [EnumMember(Value = "Single Room")]
        SingleRoom,
        [EnumMember(Value = "Family Suite")]
        FamilySuite,
        [EnumMember(Value = "Cabin")]
        Cabin
    }
}
