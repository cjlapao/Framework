class FloatButton {
    constructor() {
        $(".float-bar-btn").click(function () {
            $(this).toggleClass("active");
            $(".float-bar-icons").toggleClass("open");
        });
    }
}