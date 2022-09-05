
$(document).ajaxSend(function () {
    $("#overlay").fadeIn(300);
});

var inputImage = document.getElementById("productImage");
var previewBox = document.getElementById("previewImage");

inputImage.onchange = event => {
    const [file] = inputImage.files;
    if (file) {
        previewBox.src = URL.createObjectURL(file);
    }
}

function showConfirmAlert(id) {
    Swal.fire({
        title: 'Are you sure ?',
        text: "You won't be able to revert this !",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it !'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Home/DeleteProduct",
                type: "Post",
                data: {
                    id: id
                },
                success: function (response) {
                    setTimeout(function () {
                        $("#overlay").fadeOut(300);
                    }, 500);
                    if (response.status === "Success") {
                        $(`#product-box-${id}`).fadeOut(500);
                        Swal.fire({
                            icon: 'success',
                            title: 'Notification',
                            text: response.message
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: response.message
                        });
                    }
                }
            });
        }
    });
}
