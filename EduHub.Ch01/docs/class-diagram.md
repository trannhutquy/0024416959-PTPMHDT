# Class Diagram – EduHub (sơ bộ)

```mermaid
classDiagram
    class Club {
        +Guid Id PK
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +Create(name, description)$ Club
        +AddMember(Member) void
        +MemberCount() int
    }
    class Member {
        +Guid Id PK
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +Create(name, email, studentCode)$ Member
        +IsActive() bool
    }
    class Event {
        +Guid Id PK
        +Guid ClubId FK
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +Create(clubId, title, date, capacity, isOnline)$ Event
        +HasCapacity(int registeredCount) bool
    }
    class Registration {
        +Guid Id PK
        +Guid MemberId FK
        +Guid EventId FK
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +Create(memberId, eventId)$ Registration
        +Confirm() void
        +Cancel() void
    }
    class CheckIn {
        +Guid Id PK
        +Guid RegistrationId FK
        +DateTime CheckedInAt
        +CheckInMethod Method
        +Create(registrationId, method)$ CheckIn
        +Summary() string
    }

    Club "1" --> "*" Event : tổ chức
    Club "*" -- "*" Member : có thành viên
    Member "1" --> "*" Registration : đăng ký
    Event "1" --> "*" Registration : có
    Registration "1" --> "0..1" CheckIn : điểm danh
```

## Giải thích quan hệ

| Quan hệ | Bội số | Ghi chú |
|---|---|---|
| Club – Event | 1 – * | Event giữ khoá ngoại `ClubId` |
| Club – Member | * – * | Một sinh viên tham gia nhiều CLB, một CLB có nhiều thành viên |
| Member – Registration | 1 – * | Registration giữ khoá ngoại `MemberId` |
| Event – Registration | 1 – * | Registration giữ khoá ngoại `EventId` |
| Registration – CheckIn | 1 – 0..1 | CheckIn giữ khoá ngoại `RegistrationId` |

`Registration` là bảng nối biến quan hệ nhiều–nhiều **Member ↔ Event** thành hai quan hệ 1–*.

Enum đi kèm:
- `RegistrationStatus`: `Pending`, `Confirmed`, `Cancelled`
- `CheckInMethod`: `Manual`, `QrCode`, `Nfc`
