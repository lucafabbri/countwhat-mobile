using System;
namespace countwhat.Models
{
    public class CounterDataModel
    {
        public string Id { get; set; }
        public string What { get; set; }
        public long Value { get; set; }
        public string OwnerId { get; set; }
    }
}
