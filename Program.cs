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
{
    public static partial class Program
{
    // Case 05 | View All Guests | 10 pts
    private static void Case05_ViewAllGuests()
    {
        Console.WriteLine("\n--- View All Guests ---");

        if (!guests.Any())
        {
            Console.WriteLine("No guests have been registered yet.");
            return;
        }

        Console.WriteLine($"Total guests: {guests.Count()}\n");

        // Sorted alphabetically by guest name using OrderBy() - no manual sort.
        var sortedGuests = guests.OrderBy(g => g.GuestName);

        foreach (Guest guest in sortedGuests)
        {
            guest.DisplayGuest();
        }
    }
}

{
    public static partial class Program
{
    // Case 06 | Search & Filter Rooms | 15 pts
    private static void Case06_SearchAndFilterRooms()
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\n--- Search & Filter Rooms ---");
            Console.WriteLine("1. Show all available rooms");
            Console.WriteLine("2. Filter by room type");
            Console.WriteLine("3. Filter by max price");
            Console.WriteLine("4. Room price statistics");
            Console.WriteLine("0. Back");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    {
                        var available = rooms.Where(r => r.IsAvailable)
                                              .OrderBy(r => r.PricePerNight)
                                              .ToList();

                        Console.WriteLine($"\nAvailable rooms found: {available.Count}");

                        if (!available.Any())
                            Console.WriteLine("No rooms found for the selected criteria.");
                        else
                            foreach (var r in available) r.DisplayRoom();

                        break;
                    }
                case "2":
                    {
                        string type = ReadNonEmptyString("Enter room type to filter by: ");

                        var filtered = rooms.Where(r => r.RoomType.Equals(type, StringComparison.OrdinalIgnoreCase))
                                             .ToList();

                        Console.WriteLine($"\nRooms of type '{type}' found: {filtered.Count}");

                        if (!filtered.Any())
                            Console.WriteLine("No rooms found for the selected criteria.");
                        else
                            foreach (var r in filtered) r.DisplayRoom();

                        break;
                    }
                case "3":
                    {
                        double maxPrice = ReadPositiveDouble("Enter maximum price: ");

                        var filtered = rooms.Where(r => r.IsAvailable && r.PricePerNight <= maxPrice)
                                             .OrderBy(r => r.PricePerNight)
                                             .ToList();

                        Console.WriteLine($"\nAvailable rooms at or below OMR {maxPrice:F2}: {filtered.Count}");

                        if (!filtered.Any())
                            Console.WriteLine("No rooms found for the selected criteria.");
                        else
                            foreach (var r in filtered) r.DisplayRoom();

                        break;
                    }
                case "4":
                    {
                        if (!rooms.Any())
                        {
                            Console.WriteLine("No rooms found for the selected criteria.");
                            break;
                        }

                        int totalRooms = rooms.Count();
                        int availableRooms = rooms.Count(r => r.IsAvailable);
                        double avgPrice = rooms.Average(r => r.PricePerNight);
                        double minPrice = rooms.Min(r => r.PricePerNight);
                        double maxPriceValue = rooms.Max(r => r.PricePerNight);

                        Console.WriteLine("\nRoom Price Statistics");
                        Console.WriteLine($"  Total Rooms     : {totalRooms}");
                        Console.WriteLine($"  Available Rooms : {availableRooms}");
                        Console.WriteLine($"  Average Price   : OMR {avgPrice:F2}");
                        Console.WriteLine($"  Cheapest Price  : OMR {minPrice:F2}");
                        Console.WriteLine($"  Most Expensive  : OMR {maxPriceValue:F2}");

                        break;
                    }
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
{
    public static partial class Program
{
    // Case 07 | Guest & Booking Statistics | 15 pts
    private static void Case07_GuestAndBookingStatistics()
    {
        Console.WriteLine("\n--- Guest & Booking Statistics ---");

        int totalGuests = guests.Count();
        int guestsWithRoom = guests.Count(g => g.RoomNumber != "Not Assigned");
        int totalRooms = rooms.Count();
        int bookedRooms = rooms.Count(r => !r.IsAvailable);

        Console.WriteLine($"Total Registered Guests : {totalGuests}");
        Console.WriteLine($"Guests With a Room      : {guestsWithRoom}");
        Console.WriteLine($"Total Rooms             : {totalRooms}");
        Console.WriteLine($"Booked Rooms            : {bookedRooms}");

        var activeGuests = guests.Where(g => g.RoomNumber != "Not Assigned").ToList();

        if (!activeGuests.Any())
        {
            Console.WriteLine("\nNo active bookings recorded.");
            return;
        }

        double avgNights = guests.Where(g => g.RoomNumber != "Not Assigned").Average(g => g.TotalNights);
        Console.WriteLine($"\nAverage Nights (Active Bookings): {avgNights:F2}");

        // Top 3 highest-spending guests - OrderByDescending on calculateTotalCost(), then Take(3).
        var topGuests = activeGuests
            .OrderByDescending(g => GetGuestTotalCost(g))
            .Take(3)
            .ToList();

        Console.WriteLine("\nTop 3 Highest-Spending Guests:");
        foreach (var g in topGuests)
        {
            Console.WriteLine($"  {g.GuestName} — Room {g.RoomNumber} — OMR {GetGuestTotalCost(g):F2}");
        }

        // Select() to produce a summary line per booked guest.
        var summaryLines = activeGuests.Select(g =>
            $"{g.GuestName} — Room {g.RoomNumber} — {g.TotalNights} nights — OMR {GetGuestTotalCost(g):F2}");

        Console.WriteLine("\nBooking Summary:");
        foreach (var line in summaryLines)
        {
            Console.WriteLine($"  {line}");
        }
    }

    // Helper that always routes through calculateTotalCost() using the guest's linked room price.
    private static double GetGuestTotalCost(Guest g)
    {
        Room room = rooms.FirstOrDefault(r => r.RoomNumber.ToString() == g.RoomNumber);
        double price = room != null ? room.PricePerNight : 0;
        return g.CalculateTotalCost(price);
    }
}



{
    public static partial class Program
{
    // Case 08 | Update Room Price | 15 pts
    private static void Case08_UpdateRoomPrice()
    {
        Console.WriteLine("\n--- Update Room Price ---");

        int roomNumber = ReadPositiveInt("Enter room number: ");

        // FirstOrDefault() lookup - no manual loop.
        Room room = rooms.FirstOrDefault(r => r.RoomNumber == roomNumber);

        if (room == null)
        {
            Console.WriteLine($"Error: No room found with number {roomNumber}.");
            return;
        }

        Console.Write("Enter new price per night: ");
        string input = Console.ReadLine();

        if (!double.TryParse(input, out double newPrice) || newPrice <= 0)
        {
            Console.WriteLine("Error: Invalid price. No changes made.");
            return;
        }

        double oldPrice = room.PricePerNight;
        room.PricePerNight = newPrice; // updated in place

        Console.WriteLine("\nPrice updated successfully!");
        Console.WriteLine($"  Room Number : {room.RoomNumber}");
        Console.WriteLine($"  Old Price   : OMR {oldPrice:F2}");
        Console.WriteLine($"  New Price   : OMR {room.PricePerNight:F2}");
    }
}
{
    public static partial class Program
{
    // Case 09 | Guest Lookup by Name | 15 pts
    private static void Case09_GuestLookupByName()
    {
        Console.WriteLine("\n--- Guest Lookup by Name ---");

        string search = ReadNonEmptyString("Enter name or partial name to search: ");

        // Case-insensitive Where() search - no manual loop.
        var matches = guests.Where(g => g.GuestName.ToLower().Contains(search.ToLower())).ToList();

        Console.WriteLine($"\nMatches found: {matches.Count()}");

        if (!matches.Any())
        {
            Console.WriteLine("No guests matched that search.");
            return;
        }

        foreach (var g in matches)
        {
            Console.WriteLine($"  ID: {g.GuestId} | Name: {g.GuestName} | Room: {g.RoomNumber}");
        }
    }
}
{
    public static partial class Program
{
    // Case 10 | Room Type Breakdown Report | 15 pts
    private static void Case10_RoomTypeBreakdownReport()
    {
        Console.WriteLine("\n--- Room Type Breakdown Report ---");

        string[] types = { "Single", "Double", "Suite" };

        foreach (string type in types)
        {
            int count = rooms.Count(r => r.RoomType.Equals(type, StringComparison.OrdinalIgnoreCase));

            string avgDisplay;
            if (count > 0)
            {
                double avg = rooms.Where(r => r.RoomType.Equals(type, StringComparison.OrdinalIgnoreCase))
                                   .Average(r => r.PricePerNight);
                avgDisplay = $"OMR {avg:F2}";
            }
            else
            {
                avgDisplay = "N/A";
            }

            Console.WriteLine($"  {type,-7} — Count: {count,2} | Average Price: {avgDisplay}");
        }

        if (rooms.Any())
        {
            double overallAvg = rooms.Average(r => r.PricePerNight);
            Console.WriteLine($"\nOverall Average Price (All Rooms): OMR {overallAvg:F2}");
        }
        else
        {
            Console.WriteLine("\nNo rooms available to calculate an overall average.");
        }
    }
}
{
    public static partial class Program
{
    // Case 11 | Check Out a Guest | 20 pts
    private static void Case11_CheckOutGuest()
    {
        Console.WriteLine("\n--- Check Out a Guest ---");

        string guestId = ReadNonEmptyString("Enter guest ID: ");

        // FirstOrDefault() lookup #1 - guest.
        Guest guest = guests.FirstOrDefault(g => g.GuestId.Equals(guestId, StringComparison.OrdinalIgnoreCase));

        if (guest == null)
        {
            Console.WriteLine($"Error: No guest found with ID '{guestId}'.");
            return;
        }

        if (guest.RoomNumber == "Not Assigned")
        {
            Console.WriteLine("This guest has no active booking.");
            return;
        }

        // FirstOrDefault() lookup #2 - linked room.
        Room room = rooms.FirstOrDefault(r => r.RoomNumber.ToString() == guest.RoomNumber);

        if (room == null)
        {
            Console.WriteLine("Error: Linked room could not be found. Data inconsistency detected.");
            return;
        }

        double totalCost = guest.CalculateTotalCost(room.PricePerNight);

        Console.WriteLine("\nFinal Bill");
        Console.WriteLine($"  Guest Name    : {guest.GuestName}");
        Console.WriteLine($"  Room Number   : {room.RoomNumber}");
        Console.WriteLine($"  Room Type     : {room.RoomType}");
        Console.WriteLine($"  Check-In Date : {guest.CheckInDate}");
        Console.WriteLine($"  Total Nights  : {guest.TotalNights}");
        Console.WriteLine($"  Price/Night   : OMR {room.PricePerNight:F2}");
        Console.WriteLine($"  Total Cost    : OMR {totalCost:F2}");

        Console.Write("\nConfirm checkout? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToUpper();

        if (confirm != "Y")
        {
            Console.WriteLine("Checkout cancelled. No changes made.");
            return;
        }

        // Free the room BEFORE removing the guest, then remove the guest with Remove().
        room.IsAvailable = true;
        guests.Remove(guest);

        Console.WriteLine("\nCheckout complete!");
        Console.WriteLine($"Remaining guests: {guests.Count}");
        Console.WriteLine($"Total rooms: {rooms.Count}");

        bool roomNowAvailable = rooms.Any(r => r.RoomNumber == room.RoomNumber && r.IsAvailable);
        Console.WriteLine($"Room {room.RoomNumber} is now available: {roomNowAvailable}");
    }
}
{
    public static partial class Program
{
    // Shared predicate so the preview (Where) and the removal (RemoveAll)
    // use exactly the same logic, as required.
    private static bool IsRoomSafelyRemovable(Room r)
    {
        return !r.IsAvailable && !guests.Any(g => g.RoomNumber == r.RoomNumber.ToString());
    }

    // Case 12 | Remove Unavailable Rooms | 20 pts
    private static void Case12_RemoveUnavailableRooms()
    {
        Console.WriteLine("\n--- Remove Unavailable Rooms ---");

        var removable = rooms.Where(r => IsRoomSafelyRemovable(r))
                              .OrderBy(r => r.RoomNumber)
                              .ToList();

        if (!removable.Any())
        {
            Console.WriteLine("All unavailable rooms are currently occupied. No rooms can be decommissioned.");
            return;
        }

        Console.WriteLine("Safely removable rooms:");
        foreach (var r in removable)
        {
            Console.WriteLine($"  Room {r.RoomNumber} | Type: {r.RoomType} | Price: OMR {r.PricePerNight:F2}");
        }

        Console.WriteLine($"\nTotal removable rooms: {removable.Count}");
        Console.Write("Confirm removal? (Y/N): ");
        string confirm = Console.ReadLine()?.Trim().ToUpper();

        if (confirm != "Y")
        {
            Console.WriteLine("No rooms removed.");
            return;
        }

        // Single-statement removal using RemoveAll() with the identical logic.
        rooms.RemoveAll(r => IsRoomSafelyRemovable(r));

        Console.WriteLine($"\nRooms removed. Updated total room count: {rooms.Count}");
        Console.WriteLine("Remaining rooms:");

        var remaining = rooms.Select(r => new { r.RoomNumber, r.RoomType });
        foreach (var r in remaining)
        {
            Console.WriteLine($"  Room {r.RoomNumber} — {r.RoomType}");
        }
    }
}
{
    public static partial class Program
{
    // Case 13 | Extend Guest Stay | 20 pts
    private static void Case13_ExtendGuestStay()
    {
        Console.WriteLine("\n--- Extend Guest Stay ---");

        string guestId = ReadNonEmptyString("Enter guest ID: ");

        // FirstOrDefault() lookup - no manual loop.
        Guest guest = guests.FirstOrDefault(g => g.GuestId.Equals(guestId, StringComparison.OrdinalIgnoreCase));

        if (guest == null)
        {
            Console.WriteLine($"Error: No guest found with ID '{guestId}'.");
            return;
        }

        if (guest.RoomNumber == "Not Assigned")
        {
            Console.WriteLine("This guest has no active booking to extend.");
            return;
        }

        Console.Write("Enter number of additional nights: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int additionalNights) || additionalNights <= 0)
        {
            Console.WriteLine("Error: Invalid number of nights. No changes made.");
            return;
        }

        guest.TotalNights += additionalNights; // updated in place

        Room room = rooms.FirstOrDefault(r => r.RoomNumber.ToString() == guest.RoomNumber);
        double price = room != null ? room.PricePerNight : 0;
        double newTotalCost = guest.CalculateTotalCost(price);

        Console.WriteLine("\nStay extended successfully!");
        Console.WriteLine($"  Guest Name     : {guest.GuestName}");
        Console.WriteLine($"  Updated Nights : {guest.TotalNights}");
        Console.WriteLine($"  New Total Cost : OMR {newTotalCost:F2}");
    }
}

{
    public static partial class Program
{
    // Case 14 | Highest Revenue Booking | 20 pts
    private static void Case14_HighestRevenueBooking()
    {
        Console.WriteLine("\n--- Highest Revenue Booking ---");

        // Where() to filter to guests with an active booking.
        var activeGuests = guests.Where(g => g.RoomNumber != "Not Assigned").ToList();

        if (!activeGuests.Any())
        {
            Console.WriteLine("No active bookings recorded.");
            return;
        }

        // Select() to project name, room number, and total cost (via calculateTotalCost()).
        var projected = activeGuests.Select(g =>
        {
            Room room = rooms.FirstOrDefault(r => r.RoomNumber.ToString() == g.RoomNumber);
            double price = room != null ? room.PricePerNight : 0;
            return new
            {
                GuestName = g.GuestName,
                RoomNumber = g.RoomNumber,
                TotalCost = g.CalculateTotalCost(price)
            };
        });

        // OrderByDescending() + Take(1) to find the single highest-revenue booking.
        var topEarner = projected.OrderByDescending(x => x.TotalCost).Take(1).First();

        Console.WriteLine("\nHighest Revenue Booking:");
        Console.WriteLine($"  Guest Name  : {topEarner.GuestName}");
        Console.WriteLine($"  Room Number : {topEarner.RoomNumber}");
        Console.WriteLine($"  Total Cost  : OMR {topEarner.TotalCost:F2}");
    }
}
{
    public static partial class Program
{
    // Case 15 | Guest Pagination Viewer | 20 pts
    private static void Case15_GuestPaginationViewer()
    {
        Console.WriteLine("\n--- Guest Pagination Viewer ---");

        const int pageSize = 3;

        if (!guests.Any())
        {
            Console.WriteLine("No guests have been registered yet.");
            return;
        }

        int totalPages = (int)Math.Ceiling(guests.Count / (double)pageSize);

        int pageNumber = ReadPositiveInt($"Enter page number (1-{totalPages}): ");

        if (pageNumber > totalPages)
        {
            Console.WriteLine("That page does not exist.");
            return;
        }

        // Skip() and Take() together - no manual index-range loop.
        var pageGuests = guests.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        Console.WriteLine($"\nPage {pageNumber} of {totalPages}");
        foreach (var g in pageGuests)
        {
            g.DisplayGuest();
        }
    }
}
// Represents a single guest registered in the hotel system.
public class Guest
{
    public string GuestId { get; set; }
    public string GuestName { get; set; }
    public string RoomNumber { get; set; }   // "Not Assigned" until a room is booked
    public string CheckInDate { get; set; }
    public int TotalNights { get; set; }

    public Guest(string guestId, string guestName, string checkInDate, int totalNights,
                 string roomNumber = "Not Assigned")
    {
        GuestId = guestId;
        GuestName = guestName;
        CheckInDate = checkInDate;
        TotalNights = totalNights;
        RoomNumber = roomNumber;
    }

    public void DisplayGuest()
    {
        Console.WriteLine(
            $"ID: {GuestId} | Name: {GuestName,-15} | Room: {RoomNumber,-12} | Check-in: {CheckInDate,-10} | Nights: {TotalNights}");
    }

    // The linked Room's price per night is looked up by the caller and passed in here,
    // since Guest itself only stores the room number, not the room's price.
    public double CalculateTotalCost(double pricePerNight)
    {
        return pricePerNight * TotalNights;
    }
}
namespace HotelManagementSystem
{
    // This class is split across many files (partial class) - one file per case,
    // as requested. This file only holds Main(), the menu loop, the shared
    // data lists, and small shared input-validation helpers.
    public static partial class Program
    {
        public static List<Room> rooms = new List<Room>();
        public static List<Guest> guests = new List<Guest>();

        public static void Main(string[] args)
        {
            PreloadRooms();
            RunMenu();
        }

        private static void PreloadRooms()
        {
            rooms.Add(new Room(101, "Single", 35.00));
            rooms.Add(new Room(102, "Single", 38.00));
            rooms.Add(new Room(201, "Double", 55.00));
            rooms.Add(new Room(202, "Double", 60.00));
            rooms.Add(new Room(301, "Suite", 120.00));
            rooms.Add(new Room(302, "Suite", 140.00));
        }

        private static void RunMenu()
        {
            bool running = true;

            while (running)
            {
                PrintMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": Case01_AddNewRoom(); break;
                    case "2": Case02_RegisterNewGuest(); break;
                    case "3": Case03_BookRoomForGuest(); break;
                    case "4": Case04_ViewAllRooms(); break;
                    case "5": Case05_ViewAllGuests(); break;
                    case "6": Case06_SearchAndFilterRooms(); break;
                    case "7": Case07_GuestAndBookingStatistics(); break;
                    case "8": Case08_UpdateRoomPrice(); break;
                    case "9": Case09_GuestLookupByName(); break;
                    case "10": Case10_RoomTypeBreakdownReport(); break;
                    case "11": Case11_CheckOutGuest(); break;
                    case "12": Case12_RemoveUnavailableRooms(); break;
                    case "13": Case13_ExtendGuestStay(); break;
                    case "14": Case14_HighestRevenueBooking(); break;
                    case "15": Case15_GuestPaginationViewer(); break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nThank you for using Grand Vista Hotel Management System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress ENTER to return to the main menu...");
                    Console.ReadLine();
                }
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("================================================");
            Console.WriteLine("GRAND VISTA HOTEL — MANAGEMENT SYSTEM");
            Console.WriteLine("================================================");
            Console.WriteLine(" 1. Add New Room");
            Console.WriteLine(" 2. Register New Guest");
            Console.WriteLine(" 3. Book a Room for a Guest");
            Console.WriteLine(" 4. View All Rooms");
            Console.WriteLine(" 5. View All Guests");
            Console.WriteLine(" 6. Search & Filter Rooms");
            Console.WriteLine(" 7. Guest & Booking Statistics");
            Console.WriteLine(" 8. Update Room Price");
            Console.WriteLine(" 9. Guest Lookup by Name");
            Console.WriteLine("10. Room Type Breakdown Report");
            Console.WriteLine("11. Check Out a Guest");
            Console.WriteLine("12. Remove Unavailable Rooms");
            Console.WriteLine("13. Extend Guest Stay");
            Console.WriteLine("14. Highest Revenue Booking");
            Console.WriteLine("15. Guest Pagination Viewer");
            Console.WriteLine(" 0. Exit");
            Console.WriteLine("================================================");
            Console.Write("Enter your choice: ");
        }

        // ---------- Shared input helpers (used by several case files) ----------

        public static int ReadPositiveInt(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out value) && value > 0)
                    return value;
                Console.WriteLine("Invalid input. Please enter a positive whole number.");
            }
        }

        public static double ReadPositiveDouble(string prompt)
        {
            double value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out value) && value > 0)
                    return value;
                Console.WriteLine("Invalid input. Please enter a positive number.");
            }
        }

        public static string ReadNonEmptyString(string prompt)
        {
            string value;
            while (true)
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                    return value.Trim();
                Console.WriteLine("Input cannot be empty.");
            }
        }
    }
}
// Represents a single room in the hotel.
public class Room
{
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }
    public double PricePerNight { get; set; }
    public bool IsAvailable { get; set; }

    public Room(int roomNumber, string roomType, double pricePerNight, bool isAvailable = true)
    {
        RoomNumber = roomNumber;
        RoomType = roomType;
        PricePerNight = pricePerNight;
        IsAvailable = isAvailable;
    }

    public void DisplayRoom()
    {
        string status = IsAvailable ? "Available" : "Booked";
        Console.WriteLine(
            $"Room {RoomNumber,-6} | Type: {RoomType,-7} | Price/Night: OMR {PricePerNight,8:F2} | Status: {status}");
    }
}
}


