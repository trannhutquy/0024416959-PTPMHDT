using System.Text;
using EduHub.Ch01.Domain;

Console.OutputEncoding = Encoding.UTF8;

const string Separator = "==============================";
const int EventCapacity = 50;
const string ClubName = "Tech Club";

static void PrintStep(string title) =>
    Console.WriteLine($"\n{Separator} {title} {Separator}");

// ---------------------------------------------------------------
PrintStep("1. Tạo câu lạc bộ");
var club = Club.Create(ClubName, "Câu lạc bộ công nghệ: chia sẻ kiến thức lập trình, AI và tổ chức workshop cho sinh viên.");
Console.WriteLine($"Đã tạo CLB: {club.Name} (Id = {club.Id})");
Console.WriteLine($"Mô tả: {club.Description}");

// ---------------------------------------------------------------
PrintStep("2. Thêm thành viên vào CLB");
var members = new[]
{
    Member.Create("Nguyễn Thúy Vy", "vy@example.com", "0024418327"),
    Member.Create("Trần Minh Khoa", "khoa@example.com", "0024418001"),
    Member.Create("Lê Ngọc Anh", "anh@example.com", "0024418002")
};

foreach (var member in members)
{
    club.AddMember(member);
    Console.WriteLine($"+ {member.Name} ({member.StudentCode}) - hoạt động: {member.IsActive()}");
}
Console.WriteLine($"Tổng số thành viên của {club.Name}: {club.MemberCount()}");

// ---------------------------------------------------------------
PrintStep("3. Tạo sự kiện");
var workshop = Event.Create(club.Id, "Workshop AI", DateTime.Now.AddDays(7), EventCapacity, isOnline: false);
Console.WriteLine($"Sự kiện: {workshop.Title} | Ngày: {workshop.Date:dd/MM/yyyy HH:mm} | Sức chứa: {workshop.Capacity} | Online: {workshop.IsOnline}");

// ---------------------------------------------------------------
PrintStep("4. Đăng ký tham dự và xác nhận");
var registrations = new List<Registration>();

foreach (var member in members.Take(2))
{
    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine($"Sự kiện đã đầy, không thể đăng ký cho {member.Name}.");
        continue;
    }

    var registration = Registration.Create(member.Id, workshop.Id);
    registrations.Add(registration);
    Console.WriteLine($"{member.Name} đăng ký -> Status = {registration.Status}");

    registration.Confirm();
    Console.WriteLine($"{member.Name} được xác nhận -> Status = {registration.Status}");
}

// ---------------------------------------------------------------
PrintStep("5. Check-in");
var checkIns = new List<CheckIn>();
var firstRegistration = registrations[0];
var checkIn = CheckIn.Create(firstRegistration.Id, CheckInMethod.QrCode);
checkIns.Add(checkIn);
Console.WriteLine(checkIn.Summary());

// ---------------------------------------------------------------
PrintStep("6. Báo cáo tham dự");
var membersById = club.Members.ToDictionary(m => m.Id);

Console.WriteLine($"Sự kiện: {workshop.Title}");
Console.WriteLine($"Số lượt đăng ký: {registrations.Count}/{workshop.Capacity}");
Console.WriteLine($"Số lượt check-in: {checkIns.Count}");

Console.WriteLine("Danh sách đăng ký:");
foreach (var registration in registrations)
{
    var name = membersById[registration.MemberId].Name;
    var attended = checkIns.Any(c => c.RegistrationId == registration.Id);
    Console.WriteLine($"  - {name} | {registration.Status} | {(attended ? "đã check-in" : "chưa check-in")}");
}

Console.WriteLine("Danh sách đã check-in:");
foreach (var item in checkIns)
{
    var registration = registrations.First(r => r.Id == item.RegistrationId);
    Console.WriteLine($"  - {membersById[registration.MemberId].Name} lúc {item.CheckedInAt.ToLocalTime():HH:mm:ss}");
}

// ---------------------------------------------------------------
PrintStep("7. Vi phạm luật nghiệp vụ");
var cancelledRegistration = Registration.Create(members[2].Id, workshop.Id);
cancelledRegistration.Cancel();
Console.WriteLine($"Phiếu của {members[2].Name} đã huỷ -> Status = {cancelledRegistration.Status}");

try
{
    cancelledRegistration.Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Bắt được lỗi nghiệp vụ: {ex.Message}");
}

Console.WriteLine($"\n{Separator} Hoàn tất {Separator}");
