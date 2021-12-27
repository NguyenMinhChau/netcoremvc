var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btnActive').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id');
            $.ajax({
                url: '/Admin/User/ChangeStatus',
                data: { id: id },
                dataType: 'json',
                type: 'POST',
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Kích hoạt')
                        btn.css("background-color", "green");
                    } else {
                        btn.text('Đã khóa')
                        btn.css("background-color", "red");
                    }
                }
            })
        })
    }
}
user.init();