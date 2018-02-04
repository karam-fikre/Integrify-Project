

$(document).ready(function () {
    $("#imagebrow").change(function () {
        var File = this.files;
        if (File && File[0]) {
            ReadImage(File[0]);
        }
    })

})
var ReadImage = function (file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            var height = this.height;
            var width = this.width;
            var type = this.type;
            var size = ~~(file.size / 1024) + "KB";
            $("#targetImg").attr('src', _file.target.result);
            $("#description").text("Size" + size + ", " + height + "X" + width + "");
            $("#cleardata").show();
            $("#btn-upload").prop('disabled', false);
        }
    }
}



var clearPreview = function () {
   
        $("#imagebrow").val("");
        $("#description").text("");
        $("#imgPreview").load("/Account/UploadProfilePicture #imgPreview")
        $("#btn-upload").prop('disabled', true);
}