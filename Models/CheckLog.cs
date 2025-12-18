using SQLite;

namespace DoorChecker.Models
{
    public class CheckLog
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime CheckDate { get; set; }
        public string Username { get; set; }
        public int DoorID { get; set; }
        public bool Check1 { get; set; }
        public bool Check2 { get; set; }
        public bool Check3 { get; set; }
        public bool Check4 { get; set; }
        public bool Check5 { get; set; }
        public bool Check6 { get; set; }
        public bool Check7 { get; set; }
        public bool Check8 { get; set; }
        public bool Check9 { get; set; }
        public bool Check10 { get; set; }
        public bool Check11 { get; set; }
        public bool Check12 { get; set; }
        public byte[] ReaderIn { get; set; }
        public byte[] ReaderOut { get; set; }
        public byte[] Lock1 { get; set; }
        public byte[] Lock2 { get; set; }
        public byte[] BGU { get; set; }
        public byte[] RTE { get; set; }

        public CheckLog()
        {
            ReaderIn = new byte[0];
            ReaderOut = new byte[0];
            Lock1 = new byte[0];
            Lock2 = new byte[0];
            BGU = new byte[0];
            RTE = new byte[0];
        }
    }
}
