namespace Fjtc.Model.SearchModel
{
    public class MpWeiXinUserSerachParameter : SearchParameter
    {
        public long? ProductUserId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }

    }
}
