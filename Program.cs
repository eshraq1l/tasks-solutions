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
}



