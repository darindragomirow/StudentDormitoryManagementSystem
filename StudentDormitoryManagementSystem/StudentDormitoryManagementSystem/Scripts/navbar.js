$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
    $(".body-wrapper").toggleClass("active");
});