using ProtoBuf;

namespace ECP.B2b.ModelDto.Basic.UserFunction
{
    [ProtoContract]
    public class PageResultReplyDto
    {
        [ProtoMember(1)]
        public int ID { get; set; }

        [ProtoMember(2)]
        public int? USER_ID { get; set; }

        [ProtoMember(3)]
        public int? MENU_ID { get; set; }

        [ProtoMember(4)]
        public int? MENU_FUNCTION_ID { get; set; }
    } 
}
