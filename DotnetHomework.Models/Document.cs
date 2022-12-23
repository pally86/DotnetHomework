using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetHomework.Models
{
    public class Document
    {
        public string Id { get; set; } = "";
        public List<string>? Tags { get; set; }
        public object Data { get; set; }
    }
}
