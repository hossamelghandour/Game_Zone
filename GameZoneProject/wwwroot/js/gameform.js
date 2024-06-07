$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.review').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});

//$(document).ready(function () {
//    $('#Cover').on('change', function () {
//        $('.review').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none')
//    });
//});