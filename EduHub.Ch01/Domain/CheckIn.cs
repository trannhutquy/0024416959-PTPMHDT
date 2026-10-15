namespace EduHub.Ch01.Domain;

/// <summary>Hình thức điểm danh.</summary>
public enum CheckInMethod
{
    /// <summary>Ban tổ chức điểm danh thủ công.</summary>
    Manual,

    /// <summary>Quét mã QR.</summary>
    QrCode,

    /// <summary>Quẹt thẻ NFC.</summary>
    Nfc
}

/// <summary>Bản ghi điểm danh tại sự kiện, gắn với một <see cref="Registration"/>.</summary>
public sealed class CheckIn
{
    /// <summary>Mã định danh duy nhất.</summary>
    public Guid Id { get; private set; }

    /// <summary>Khoá ngoại tới <see cref="Registration"/>.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Thời điểm điểm danh (UTC).</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Hình thức điểm danh.</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn() { }

    /// <summary>Tạo bản ghi điểm danh mới.</summary>
    /// <exception cref="ArgumentException">RegistrationId rỗng hoặc hình thức không hợp lệ.</exception>
    public static CheckIn Create(Guid registrationId, CheckInMethod method)
    {
        if (registrationId == Guid.Empty)
            throw new ArgumentException("RegistrationId is required.", nameof(registrationId));
        if (!Enum.IsDefined(method))
            throw new ArgumentException("Check-in method is invalid.", nameof(method));

        return new CheckIn
        {
            Id = Guid.NewGuid(),
            RegistrationId = registrationId,
            CheckedInAt = DateTime.UtcNow,
            Method = method
        };
    }

    /// <summary>Trả về chuỗi mô tả bản ghi điểm danh.</summary>
    public string Summary() =>
        $"CheckIn {Id} for registration {RegistrationId} at {CheckedInAt.ToLocalTime():HH:mm:ss} via {Method}";
}
