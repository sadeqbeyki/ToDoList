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
            .fail(() => alert("An error has occurred, please contact the system administrator."));
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
                error: () => alert("An error has occurred, please contact the system administrator.")
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
            if (!confirm("Are you sure?")) return;
        }

        if (method === "post") {
            $.post(url, data)
                .done(() => window.location.reload())
                .fail(() => alert("An error has occurred, please contact the system administrator."));
        } else {
            $.get(url, data)
                .done(response => console.log(response))
                .fail(() => alert("An error has occurred, please contact the system administrator."));
        }
    });
});



//is done checkbox  in index page -full ajax - with toast in left

$(document).on("click", ".toggle-done", function () {
    const button = $(this);
    const id = button.data("id");

    // تبدیل رشته به boolean
    const currentStatus = button.data("isdone") === true || button.data("isdone") === "True" || button.data("isdone") === "true";
    const newStatus = !currentStatus;

    const token = $('input[name="__RequestVerificationToken"]').val();
    const icon = button.find("i");

    icon.addClass("fa-spin");

    $.ajax({
        url: "?handler=ToggleDone",
        method: "POST",
        data: {
            __RequestVerificationToken: token,
            id: id,
            isDone: newStatus
        },
        success: function () {
            button.data("isdone", newStatus);

            setTimeout(() => {
                icon.removeClass("fa-spin");

                if (newStatus) {
                    icon.removeClass("fa-times text-danger").addClass("fa-check text-success");
                    button.closest("tr").addClass("table-danger");
                } else {
                    icon.removeClass("fa-check text-success").addClass("fa-times text-danger");
                    button.closest("tr").removeClass("table-danger");
                }

                toastr.success("Task status changed successfully.");
            }, 300);
        },
        error: function () {
            icon.removeClass("fa-spin");
            toastr.error("Error changing status. Please try again.");
        }
    });
});


//2

$(document).ready(function () {
    // تنظیمات Toastr
    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-left",
        "timeOut": "3000"
    };

    // هندل تغییر چک‌باکس
    $(document).on("change", ".is-done-toggle", function () {
        const checkbox = $(this);
        const id = checkbox.data("id");
        const isDone = checkbox.is(":checked");
        const token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: "?handler=ToggleDone",
            method: "POST",
            data: {
                __RequestVerificationToken: token,
                id: id,
                isDone: isDone
            },
            success: function () {
                const row = checkbox.closest("tr");
                row.toggleClass("table-danger", isDone);
                toastr.success("Task status changed successfully.");
            },
            error: function () {
                checkbox.prop("checked", !isDone);
                toastr.error("Error changing status. Please try again.");
            }
        });
    });
});

//checkBox Configuration
$(document).ready(function () {
    const viewModeKey = "taskViewMode";

    // خواندن مقدار اولیه از localStorage یا تنظیم پیش‌فرض
    const savedViewMode = localStorage.getItem(viewModeKey) || "icon";
    $("#viewModeSelector").val(savedViewMode);
    toggleViewMode(savedViewMode);

    // تغییر حالت توسط کاربر
    $("#viewModeSelector").on("change", function () {
        const selected = $(this).val();
        localStorage.setItem(viewModeKey, selected);
        toggleViewMode(selected);
    });

    function toggleViewMode(mode) {
        if (mode === "checkbox") {
            $(".icon-mode").hide();
            $(".checkbox-mode").show();
        } else {
            $(".checkbox-mode").hide();
            $(".icon-mode").show();
        }
    }
});

//delete
$(document).ready(function () {
    const token = $('input[name="__RequestVerificationToken"]').val();

    $(document).on('click', '.btn-delete', function (e) {
        e.preventDefault();

        const btn = $(this);
        const id = btn.data('id');
        const url = btn.data('url');

        if (!confirm("Are you sure you want to delete this item?")) return;

        $.ajax({
            url: url,
            type: "POST",
            data: { id: id },
            headers: {
                'RequestVerificationToken': token
            },
            success: function (result) {
                if (result.isSucceeded) {
                    btn.closest("tr").remove();
                } else {
                    alert("خطا: " + result.message);
                }
            },
            error: function () {
                alert("خطا در حذف.");
            }
        });
    });
});

