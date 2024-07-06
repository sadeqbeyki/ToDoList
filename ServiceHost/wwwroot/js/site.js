// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// site-modal.js

// بارگذاری یک Partial View در مودال
function showModal(url) {
    $.get(url, function (data) {
        $('#modal-placeholder').html(data);
        $('#modal-placeholder .modal').modal('show');
    });
}

// هندل کردن ارسال فرم به صورت Ajax
$(document).on('submit', 'form[data-ajax="true"]', function (event) {
    event.preventDefault();

    var $form = $(this);
    var url = $form.attr('action') + '?handler=' + ($form.attr('asp-page-handler') || '');
    var method = $form.attr('method') || 'post';
    var callback = $form.data('callback');

    $.ajax({
        type: method,
        url: url,
        data: $form.serialize(),
        success: function (response) {
            if (response === 'Success') {
                $('#modal-placeholder .modal').modal('hide');
                if (callback && typeof window[callback] === "function") {
                    window[callback](); // اجرای تابع مثلا Refresh
                }
            } else {
                // اگر فرم با ولیدیشن برگشت (بدون Success)، کل Modal را دوباره رندر کن
                $('#modal-placeholder').html(response);
            }
        },
        error: function () {
            alert('An error occurred while processing your request.');
        }
    });

    return false;
});

