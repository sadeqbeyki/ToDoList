// SinglePage object to manage modal operations
const SinglePage = {
    loadModal() {
        const hash = window.location.hash.toLowerCase();
        if (!hash.startsWith("#showmodal")) return;

        const urlParam = hash.split("showmodal=")[1];
        if (!urlParam) return;

        $.get(urlParam)
            .done(html => {
                const $modalContent = $("#ModalContent").html(html);
                const $form = $modalContent.find("form").last();
                $.validator.unobtrusive.parse($form);
                this.showModal();
            })
            .fail(() => alert("خطایی رخ داده، لطفا با مدیر سیستم تماس بگیرید."));
    },

    showModal() {
        const $modal = $("#MainModal");
        $modal.modal("show");

        // Handle close button
        $modal.find(".modal-close").off("click").on("click", () => this.hideModal());

        // Clean up URL
        window.history.replaceState({}, document.title, window.location.pathname);
    },

    hideModal() {
        $("#MainModal").modal("hide");
    },

    handleAjaxFormSubmit($form) {
        const method = $form.attr("method").toLowerCase();
        const url = $form.attr("action");
        const action = $form.data("action");

        if (method === "get") {
            const data = $form.serialize();
            $.get(url, data, result => this.handleCallback(result, action, $form));
        } else {
            const formData = new FormData($form[0]);
            $.ajax({
                url,
                type: "post",
                data: formData,
                enctype: "multipart/form-data",
                dataType: "json",
                processData: false,
                contentType: false,
                success: data => this.handleCallback(data, action, $form),
                error: () => alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید.")
            });
        }
    },

    handleCallback(data, action, $form) {
        switch (action) {
            case "Message":
                alert(data.message);
                break;
            case "Refresh":
                if (data.isSucceeded) window.location.reload();
                else alert(data.message);
                break;
            case "RefreshList":
                this.hideModal();
                const refreshUrl = $form.data("refreshurl");
                const refreshDiv = $form.data("refreshdiv");
                if (refreshUrl && refreshDiv) this.refreshContent(refreshUrl, refreshDiv);
                break;
            case "SetValue":
                const element = $form.data("element");
                if (element) $(`#${element}`).html(data);
                break;
            default:
                this.hideModal();
        }
    },

    refreshContent(url, targetId) {
        const params = window.location.search;
        $.get(url, params, result => $(`#${targetId}`).html(result));
    }
};

// Document ready setup
$(function () {
    window.onhashchange = () => SinglePage.loadModal();

    // Submit AJAX forms
    $(document).on("submit", 'form[data-ajax="true"]', function (e) {
        e.preventDefault();
        SinglePage.handleAjaxFormSubmit($(this));
        return false;
    });

    // Ajax-enabled buttons
    $(document).on("click", 'button[data-ajax="true"]', function () {
        const $btn = $(this);
        const method = $btn.data("request-method")?.toLowerCase();
        let url = $btn.data("request-url");
        const formId = $btn.data("request-form");
        const fieldId = $btn.data("request-field-id");
        const data = formId ? $(`#${formId}`).serialize() : null;

        if (fieldId) url += `/${$(`#${fieldId}`).val()}`;

        if ($btn.data("request-confirm")) {
            if (!confirm("آیا از انجام این عملیات اطمینان دارید؟")) return;
        }

        if (method === "post") {
            $.post(url, data)
                .done(() => window.location.reload())
                .fail(() => alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید."));
        } else {
            $.get(url, data)
                .done(response => console.log(response))
                .fail(() => alert("خطایی رخ داده است. لطفا با مدیر سیستم تماس بگیرید."));
        }
    });
});

//is done checkbox  in index page
//$(document).ready(function () {
//    const token = $('input[name="__RequestVerificationToken"]').val();

//    $(document).on("change", ".is-done-toggle", function () {
//        const id = $(this).data("id");
//        const isDone = $(this).is(":checked");

//        $.ajax({
//            url: "?handler=ToggleDone",
//            method: "POST",
//            headers: {
//                "RequestVerificationToken": token
//            },
//            data: {
//                id: id,
//                isDone: isDone
//            },
//            success: function () {
//                location.reload();
//            },
//            error: function (xhr) {
//                alert("خطای HTTP: " + xhr.status + " | بررسی سرور");
//            }
//        });
//    });
//});

//is done checkbox  in index page - full ajax
$(document).ready(function () {
    $(document).on("change", ".is-done-toggle", function () {
        const checkbox = $(this);
        const id = checkbox.data("id");
        const isDone = checkbox.is(":checked");

        // گرفتن توکن از فرم
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "?handler=ToggleDone",
            method: "POST",
            data: {
                __RequestVerificationToken: token, // مهم
                id: id,
                isDone: isDone
            },
            success: function () {
                // به‌جای رفرش کل صفحه، فقط ردیف را رنگی کن
                const row = checkbox.closest("tr");
                if (isDone)
                    row.addClass("table-danger");
                else
                    row.removeClass("table-danger");

                // پیام موفقیت (اختیاری)
                // alert("وضعیت با موفقیت تغییر کرد");
            },
            error: function (xhr) {
                alert("خطایی رخ داده است: " + xhr.status);
                // برگردوندن چک‌باکس به حالت قبل در صورت خطا
                checkbox.prop("checked", !isDone);
            }
        });
    });
});

