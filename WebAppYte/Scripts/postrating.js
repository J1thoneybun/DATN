
// Lắng nghe sự kiện thay đổi khi người dùng chọn một sao
document.addEventListener('DOMContentLoaded', function () {
    // Lấy tất cả các phần tử radio button trong mỗi dòng sao
    document.querySelectorAll('.star-line input').forEach(input => {
        input.addEventListener('change', function () {
            // Lấy giá trị sao người dùng chọn
            const rating = this.value;

            // Hiển thị giá trị đánh giá trên giao diện
            document.getElementById('rating-value').textContent = `Đánh giá: ${rating} sao`;

            // Gửi dữ liệu đánh giá lên server
            sendRatingToServer(rating, noidung);
        });
    });
});

// Hàm gửi dữ liệu đánh giá lên server
function sendRatingToServer(madanhgia, rating, noidung) {
    // Sử dụng Fetch API để gửi yêu cầu POST
    fetch('/api/rating', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ rating: rating }), // Chuyển đối tượng thành JSON
    })
        .then(response => response.json()) // Chuyển phản hồi của server thành đối tượng JSON
        .then(data => {
            console.log('Đánh giá đã được gửi thành công:', data);
        })
        .catch(error => {
            console.error('Lỗi khi gửi đánh giá:', error);
        });
}
