using SQLite;

namespace NomenDeutsch.Models
{
    [Table("Noun_neu")]
    public class NounNeu
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string? Full_Noun { get; set; }
        public string? Artikel { get; set; }
        public string? Noun { get; set; }
        public string? English { get; set; }
        public string? Plural { get; set; }
        public string? Hint { get; set; }
        public string? Beispiel { get; set; }
        public string? English_Options { get; set; }
        public string? Plural_Options { get; set; }
    }
}
