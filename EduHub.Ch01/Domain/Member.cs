namespace EduHub.Ch01.Domain;

/// <summary>Thành viên sinh viên của câu lạc bộ.</summary>
public sealed class Member
{
    /// <summary>Mã định danh duy nhất.</summary>
    public Guid Id { get; private set; }

    /// <summary>Họ tên thành viên.</summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>Email liên hệ.</summary>
    public string Email { get; private set; } = string.Empty;

    /// <summary>Mã số sinh viên.</summary>
    public string StudentCode { get; private set; } = string.Empty;

    /// <summary>Thời điểm tham gia (UTC).</summary>
    public DateTime JoinedAt { get; private set; }

    private Member() { }

    /// <summary>Tạo thành viên mới sau khi kiểm tra dữ liệu đầu vào.</summary>
    /// <exception cref="ArgumentException">Tên/mã sinh viên trống hoặc email không hợp lệ.</exception>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email is invalid.", nameof(email));
        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Student code is required.", nameof(studentCode));

        return new Member
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Email = email.Trim(),
            StudentCode = studentCode.Trim(),
            JoinedAt = DateTime.UtcNow
        };
    }

    /// <summary>Thành viên đang hoạt động nếu thời điểm tham gia không ở tương lai.</summary>
    public bool IsActive() => JoinedAt <= DateTime.UtcNow;
}
