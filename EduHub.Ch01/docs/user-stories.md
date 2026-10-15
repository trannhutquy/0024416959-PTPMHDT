# User Stories – EduHub

## Sinh viên

### US-01: Đăng ký tham gia CLB
**As a** sinh viên, **I want** gửi yêu cầu tham gia một câu lạc bộ, **so that** tôi trở thành thành viên và được tham dự các hoạt động của CLB.

**Acceptance criteria**
- Given tôi chưa là thành viên của CLB, When tôi gửi yêu cầu tham gia với họ tên, email hợp lệ và mã số sinh viên, Then yêu cầu được ghi nhận và chờ Admin phê duyệt.
- Given tôi nhập email không có ký tự `@` hoặc để trống họ tên, When tôi gửi yêu cầu, Then hệ thống từ chối và hiển thị thông báo lỗi cụ thể.

### US-02: Đăng ký sự kiện
**As a** sinh viên, **I want** đăng ký tham dự một sự kiện của CLB, **so that** tôi có chỗ và được ban tổ chức xác nhận.

**Acceptance criteria**
- Given sự kiện còn chỗ, When tôi đăng ký, Then phiếu đăng ký được tạo với trạng thái Pending.
- Given sự kiện đã đủ capacity, When tôi đăng ký, Then hệ thống từ chối và báo hết chỗ.
- Given tôi đã huỷ phiếu đăng ký, When ban CLB cố xác nhận phiếu đó, Then hệ thống báo lỗi và giữ nguyên trạng thái Cancelled.

### US-03: Check-in tại sự kiện
**As a** sinh viên, **I want** check-in khi đến sự kiện, **so that** sự có mặt của tôi được ghi nhận chính thức.

**Acceptance criteria**
- Given phiếu đăng ký của tôi đã Confirmed, When tôi check-in (thủ công hoặc quét QR), Then hệ thống tạo bản ghi điểm danh kèm thời điểm và hình thức check-in.
- Given phiếu đăng ký của tôi chưa được xác nhận hoặc đã bị huỷ, When tôi check-in, Then hệ thống từ chối điểm danh.

## Ban CLB

### US-04: Tạo sự kiện
**As a** thành viên ban CLB, **I want** tạo sự kiện với tiêu đề, thời gian, sức chứa và hình thức online/offline, **so that** sinh viên có thể đăng ký tham dự.

**Acceptance criteria**
- Given tôi nhập đầy đủ tiêu đề, ngày và capacity lớn hơn 0, When tôi lưu, Then sự kiện được tạo và thuộc về CLB của tôi.
- Given tôi để trống tiêu đề hoặc nhập capacity nhỏ hơn hoặc bằng 0, When tôi lưu, Then hệ thống từ chối và nêu rõ trường không hợp lệ.

### US-05: Xem danh sách thành viên và xuất báo cáo tham dự
**As a** thành viên ban CLB, **I want** xem danh sách thành viên và xuất báo cáo tham dự sự kiện, **so that** tôi nắm được quy mô CLB và mức độ tham gia thực tế.

**Acceptance criteria**
- Given CLB có thành viên, When tôi mở danh sách thành viên, Then tôi thấy họ tên, mã số sinh viên và ngày tham gia của từng người.
- Given sự kiện đã diễn ra, When tôi xuất báo cáo, Then báo cáo gồm số đăng ký, số check-in và danh sách tên người tham dự.
- Given sự kiện chưa có ai đăng ký, When tôi xuất báo cáo, Then báo cáo hiển thị 0 đăng ký và 0 check-in, không báo lỗi.

## Admin

### US-06: Phê duyệt thành viên và quản lý CLB
**As an** Admin, **I want** phê duyệt hoặc từ chối yêu cầu tham gia và quản lý thông tin các CLB, **so that** danh sách thành viên luôn chính xác và CLB hoạt động đúng quy định.

**Acceptance criteria**
- Given có yêu cầu tham gia đang chờ, When tôi phê duyệt, Then sinh viên trở thành thành viên của CLB.
- Given sinh viên đã là thành viên, When tôi cố thêm lại lần nữa, Then hệ thống từ chối vì trùng.
- Given tôi tạo CLB mới, When tôi nhập tên và mô tả hợp lệ, Then CLB được lưu cùng thời điểm thành lập.
