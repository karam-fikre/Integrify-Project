var deleteImage = function (id) {

    var data = document.getElementById("imgview").getAttribute('src');
    $.ajax({
        type: "POST",
        url: '../../Gallery/DeleteSnapShot?id=' + id,
        dataType: 'text',
        data: { dataType: data },
        success: function () {
            $("#picdiv").load("/Gallery/ImagesView #picdiv");
        }
    });


};