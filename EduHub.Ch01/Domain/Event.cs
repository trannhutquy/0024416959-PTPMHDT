namespace EduHub.Ch01.Domain;

/// <summary>Sự kiện do câu lạc bộ tổ chức.</summary>
public sealed class Event
{
    /// <summary>Mã định danh duy nhất.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Club"/> tổ chức sự kiện.</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Tiêu đề sự kiện.</summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>Thời điểm diễn ra sự kiện.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Sức chứa tối đa.</summary>
    public int Capacity { get; private set; }

    /// <summary>Sự kiện tổ chức trực tuyến hay không.</summary>
    public bool IsOnline { get; private set; }

    private Event() { }

    /// <summary>Tạo sự kiện mới sau khi kiểm tra dữ liệu đầu vào.</summary>
    /// <exception cref="ArgumentException">ClubId rỗng, tiêu đề trống hoặc Capacity &lt;= 0.</exception>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("ClubId is required.", nameof(clubId));
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

        return new Event
        {
            Id = Guid.NewGuid(),
            ClubId = clubId,
            Title = title.Trim(),
            Date = date,
            Capacity = capacity,
            IsOnline = isOnline
        };
    }

    /// <summary>Kiểm tra sự kiện còn chỗ cho thêm người đăng ký hay không.</summary>
    /// <param name="registeredCount">Số phiếu đăng ký hiện có.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="registeredCount"/> âm.</exception>
    public bool HasCapacity(int registeredCount)
    {
        if (registeredCount < 0)
            throw new ArgumentOutOfRangeException(nameof(registeredCount), "Registered count cannot be negative.");

        return registeredCount < Capacity;
    }
}
