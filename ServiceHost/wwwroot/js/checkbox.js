
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
$(document).ready(function() {
    // تنظیمات Toastr
    toastr.options = {
        "closeButton": true,
        "positionClass": "toast-top-left",
        "timeOut": "3000"
    };

    // هندل تغییر چک‌باکس
    $(document).on("change", ".is-done-toggle", function() {
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
            success: function() {
                const row = checkbox.closest("tr");
                row.toggleClass("table-danger", isDone);
                toastr.success("Task status changed successfully.");
            },
            error: function() {
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
