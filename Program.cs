namespace tasks_solutions
{
    internal class Program1;

    namespace HotelManagementSystem
    {
        public static partial class Program
        {
            // Case 01 | Add New Room | 10 pts
            private static void Case01_AddNewRoom()
            {
                Console.WriteLine("\n--- Add New Room ---");

                int roomNumber = ReadPositiveInt("Enter room number: ");

                // LINQ Any() for the duplicate check - no manual loop.
                if (rooms.Any(r => r.RoomNumber == roomNumber))
                {
                    Console.WriteLine($"Error: A room with number {roomNumber} already exists.");
                    return;
                }

                Console.Write("Enter room type (Single/Double/Suite): ");
                string roomType = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(roomType))
                {
                    Console.WriteLine("Error: Room type cannot be empty.");
                    return;
                }

                double price = ReadPositiveDouble("Enter price per night: ");

                Room newRoom = new Room(roomNumber, roomType, price, true);
                rooms.Add(newRoom);

                Console.WriteLine("\nRoom added successfully!");
                Console.WriteLine($"  Room Number : {newRoom.RoomNumber}");
                Console.WriteLine($"  Room Type   : {newRoom.RoomType}");
                Console.WriteLine($"  Price/Night : OMR {newRoom.PricePerNight:F2}");
                Console.WriteLine($"  Status      : Available");
                Console.WriteLine($"Total rooms in system: {rooms.Count}");
            }
        }
    }
    // Case 02 | Register New Guest | 10 pts
    private static void Case02_RegisterNewGuest()
    {
        Console.WriteLine("\n--- Register New Guest ---");

        string name = ReadNonEmptyString("Enter guest name: ");
        string checkInDate = ReadNonEmptyString("Enter check-in date (e.g. 2026-07-18): ");
        int totalNights = ReadPositiveInt("Enter number of nights: ");

        // Auto-generate guest ID from the current size of the guests list (G001, G002, ...).
        string guestId = $"G{(guests.Count + 1):D3}";

        Guest newGuest = new Guest(guestId, name, checkInDate, totalNights, "Not Assigned");
        guests.Add(newGuest);

        Console.WriteLine("\nGuest registered successfully!");
        Console.WriteLine($"  Guest ID      : {newGuest.GuestId}");
        Console.WriteLine($"  Guest Name    : {newGuest.GuestName}");
        Console.WriteLine($"  Check-In Date : {newGuest.CheckInDate}");
        Console.WriteLine($"  Total Nights  : {newGuest.TotalNights}");
        Console.WriteLine($"  Room Number   : {newGuest.RoomNumber}");
    }
}

 // Case 03 | Book a Room for a Guest | 10 pts
        private static void Case03_BookRoomForGuest()
{
    Console.WriteLine("\n--- Book a Room for a Guest ---");

    string guestId = ReadNonEmptyString("Enter guest ID: ");

    // FirstOrDefault() lookup - no manual loop.
    Guest guest = guests.FirstOrDefault(g => g.GuestId.Equals(guestId, StringComparison.OrdinalIgnoreCase));

    if (guest == null)
    {
        Console.WriteLine($"Error: No guest found with ID '{guestId}'.");
        return;
    }

    int roomNumber = ReadPositiveInt("Enter room number to book: ");

    // FirstOrDefault() lookup - no manual loop.
    Room room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

    if (room == null)
    {
        Console.WriteLine($"Error: No room found with number {roomNumber}.");
        return;
    }

    if (!room.IsAvailable)
    {
        Console.WriteLine("Room is already booked.");
        return;
    }

    // Update both objects in place - the lists reflect the change automatically.
    guest.RoomNumber = room.RoomNumber.ToString();
    room.IsAvailable = false;

    double totalCost = guest.CalculateTotalCost(room.PricePerNight);

    Console.WriteLine("\nBooking confirmed!");
    Console.WriteLine($"  Guest Name   : {guest.GuestName}");
    Console.WriteLine($"  Room Number  : {room.RoomNumber}");
    Console.WriteLine($"  Room Type    : {room.RoomType}");
    Console.WriteLine($"  Price/Night  : OMR {room.PricePerNight:F2}");
    Console.WriteLine($"  Total Nights : {guest.TotalNights}");
    Console.WriteLine($"  Total Cost   : OMR {totalCost:F2}");
}
{
    public static partial class Program
{
    // Case 04 | View All Rooms | 10 pts
    private static void Case04_ViewAllRooms()
    {
        Console.WriteLine("\n--- View All Rooms ---");

        if (!rooms.Any())
        {
            Console.WriteLine("No rooms have been added yet.");
            return;
        }

        Console.WriteLine($"Total rooms: {rooms.Count()}\n");

        // Sorted by room number ascending using OrderBy() - no manual sort.
        var sortedRooms = rooms.OrderBy(r => r.RoomNumber);

        foreach (Room room in sortedRooms)
        {
            room.DisplayRoom();
        }
    }
}




