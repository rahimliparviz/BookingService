namespace BookingService.Domain
{
    public class Home
    {
        public int HomeId { get; set; }
        public string HomeName { get; set; }
        public List<DateTime> AvailableSlots { get; set; }= new ();

    }
}
