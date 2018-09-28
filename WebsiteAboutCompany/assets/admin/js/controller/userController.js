var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Kích hoạt');
                    }
                    else {
                        btn.text('Khoá');
                    }
                }
            });
        });
        $(".btnDelete").off('click').on('click', function (e) {
            e.preventDefault();
            var id = $(this).data('id');
            swal({ title: 'Are you sure?', showCancelButton: true }).then(result => {
                if (result.value) {
                    $.ajax({
                        url: "/Admin/User/Delete",
                        data: { id: id },
                        dataType: "json",
                        type: "POST",
                        success: function(response) {
                            if (response.result === true) {
                                swal({ title: "Đã xóa!", text: "Đã xóa thành công", type: "success" }, function () {
                                    // Redirect the user
                                    window.location.reload();
                                });
                            } else {
                                swal("Oops", "We couldn't connect to the server!", "error");
                            }
                        }
                    });
                }
            });


        });

    }
};
user.init();
   