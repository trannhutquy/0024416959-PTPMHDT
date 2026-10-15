namespace EduHub.Ch01.Domain;

/// <summary>Trạng thái của phiếu đăng ký.</summary>
public enum RegistrationStatus
{
    /// <summary>Đang chờ xác nhận.</summary>
    Pending,

    /// <summary>Đã được xác nhận.</summary>
    Confirmed,

    /// <summary>Đã huỷ.</summary>
    Cancelled
}

/// <summary>Phiếu đăng ký của một <see cref="Member"/> tham dự một <see cref="Event"/> (bảng nối nhiều–nhiều).</summary>
public sealed class Registration
{
    /// <summary>Mã định danh duy nhất.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Member"/>.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Event"/>.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Thời điểm đăng ký (UTC).</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Trạng thái hiện tại của phiếu.</summary>
    public RegistrationStatus Status { get; private set; }

    private Registration() { }

    /// <summary>Tạo phiếu đăng ký mới với trạng thái <see cref="RegistrationStatus.Pending"/>.</summary>
    /// <exception cref="ArgumentException">MemberId hoặc EventId rỗng.</exception>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId is required.", nameof(memberId));
        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId is required.", nameof(eventId));

        return new Registration
        {
            Id = Guid.NewGuid(),
            MemberId = memberId,
            EventId = eventId,
            RegisteredAt = DateTime.UtcNow,
            Status = RegistrationStatus.Pending
        };
    }

    /// <summary>Xác nhận phiếu đăng ký.</summary>
    /// <exception cref="InvalidOperationException">Phiếu không ở trạng thái Pending.</exception>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException($"Cannot confirm a registration that is {Status}.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>Huỷ phiếu đăng ký.</summary>
    /// <exception cref="InvalidOperationException">Phiếu đã bị huỷ trước đó.</exception>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Registration is already cancelled.");

        Status = RegistrationStatus.Cancelled;
    }
}
