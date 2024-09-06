$(document).on("click", ".pagination-link", function(e) {
    e.preventDefault();
    const url = $(this).attr("href");
    $.get(url, function(data) {
        const htmlContent = $(data).find("#taskTableContainer").html();
        const paginationContent = $(data).find("#paginationNav").html();
        $("#taskTableContainer").html(htmlContent);
        $("#paginationNav").html(paginationContent);
    });
});
