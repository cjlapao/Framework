/**
 * Dropdown menu on typescript for the Ittech24 Framework
 * 
 * @author Carlos Lapao <cjlapao@ittech24.co.uk>
 * @version 0.1.0.200
 */

enum DropdownPosition {
    down,
    left,
    right,
    top
}

class Dropdown {

    items: JQuery<HTMLElement>;

    constructor() {
        this.items = $("li.idropdown");
        var self = this;
        $.each(this.items, function (index, item) {
            $(item).click(function (e) {
                var button = $(item).find(".idropdown-toggle");
                self.onDropdownClick(button[0], e);
            });
        })
    }

    private collapseAll(btn: HTMLElement): void {
        var self = this;
        $.each(this.items, function (index, dropdown) {
            var button = $(dropdown).children("a.dropdown-toggle");
            if (btn !== button[0]) {
                var buttonParent = $(button).parent();
                var buttonDropdown = $(buttonParent).children(".idropdown-menu");
                $(buttonDropdown).removeClass('show');
                $(buttonParent).removeClass('show');
                $(button).attr("aria-expanded", "false");
                $(buttonDropdown).removeAttr("style");
                $(buttonDropdown).removeClass("align-right");
            }
        });
    }

    private onDropdownClick(sender: HTMLElement, e: JQuery.Event, pos: DropdownPosition = DropdownPosition.down): void {
        e.preventDefault();
        //var parent = $(sender).parent();
        var parent = $(sender).parent();

        var navbar = $(parent).parent();
        var dropdown = $(parent).children(".idropdown-menu");

        this.collapseAll(sender);

        var menuVirtuaPos: number;

        switch (pos) {
            case DropdownPosition.down:
                menuVirtuaPos = ($(sender).offset().left + $(dropdown).width())
                break;
        }

        if (dropdown) {
            if (!$(parent).hasClass('show')) {
                $(dropdown).addClass('show');
                $(parent).addClass('show');
                $(sender).attr("aria-expanded", "true");
                if (menuVirtuaPos > $(window).innerWidth()) {
                    $(dropdown).css("left", "calc(100% - " + $(dropdown).width() + "px)");
                    $(dropdown).addClass("align-right");
                }

                $(document).one('click', function closeTooltip(f) {
                    var target = <any>f.target;
                    if ($(parent).has(target).length === 0 && $("a.idropdown-toggle").has(target).length === 0) {
                        $(dropdown).removeClass('show');
                        $(parent).removeClass('show');
                        $(sender).attr("aria-expanded", "false");
                        $(dropdown).css("left", '');
                        $(dropdown).css("top", '');
                        $(dropdown).removeClass("align-right");
                    } else if ($(parent).hasClass('show')) {
                        $(document).one('click', closeTooltip);
                    }
                });
            }
            else {
                $(dropdown).removeClass('show');
                $(parent).removeClass('show');
                $(dropdown).css("left", '');
                $(dropdown).css("top", '');
                $(sender).attr("aria-expanded", "false");
            }
        }
    } 
}

///// <summary>Collapses all active dropdown menus on the page</summary>
///// <param name="btn" type="HTMLElement">The clicked button to avoid collapsing</param>
//var dropdownMenus = function (btn) {
//    var aNavbarDropDowns = $("li.idropdown");
//    $.each(aNavbarDropDowns, function (i, dropdown) {
//        var aBtn = $(dropdown).children("a.idropdown-toggle");
//        if (btn !== aBtn[0]) {
//            var aParent = $(aBtn).parent();
//            var aDropdown = $(aParent).children(".idropdown-menu");
//            $(aDropdown).removeClass('show');
//            $(aParent).removeClass('show');
//            $(aBtn).attr("aria-expanded", "false");
//            $(aDropdown).removeAttr("style");
//            $(aDropdown).removeClass("align-right");
//        }
//    });
//}

////$(".idropdown").on("mouseenter", function (e) {
////    e.preventDefault;
////    var parent = this;
////    var btn = $(parent).children("a.idropdown-toggle");
////    var dropdown = $(this).children(".idropdown-menu");
////    var menuVirtuaPos = ($(btn).offset().left + $(dropdown).width());
////    if (dropdown) {
////        $(dropdown).addClass('show');
////        $(parent).addClass('show');
////        $(btn).attr("aria-expanded", "true");
////        if (menuVirtuaPos > $(window).innerWidth()) {
////            $(dropdown).css("left", "calc(100% - " + $(dropdown).width() + "px)");
////            $(dropdown).addClass("align-right");
////        }
////    }
////});

////$(".idropdown").on("mouseleave", function (e) {
////    e.preventDefault;
////    var parent = this;
////    var btn = $(parent).children("a.idropdown-toggle");
////    var dropdown = $(this).children(".idropdown-menu");
////    if (dropdown) {
////        $(dropdown).removeClass('show');
////        $(parent).removeClass('show');
////        $(dropdown).css("left", '');
////        $(dropdown).css("top", '');
////        $(btn).attr("aria-expanded", "false");
////    }
////});

//$(".idropdown-toggle").on("click", function (e) {
//    e.preventDefault();
//    var btn = this;
//    var parent = $(btn).parent();
//    var navbar = $(parent).parent();
//    var dropdown = $(parent).children(".idropdown-menu");

//    dropdownMenus(btn);

//    var menuVirtuaPos = ($(btn).offset().left + $(dropdown).width());
//    if (dropdown) {
//        if (!$(parent).hasClass('show')) {
//            $(dropdown).addClass('show');
//            $(parent).addClass('show');
//            $(btn).attr("aria-expanded", "true");
//            if (menuVirtuaPos > $(window).innerWidth()) {
//                $(dropdown).css("left", "calc(100% - " + $(dropdown).width() + "px)");
//                $(dropdown).addClass("align-right");
//            }

//            $(document).one('click', function closeTooltip(f) {
//                var target = <any>f.target;
//                if ($(parent).has(target).length === 0 && $("a.idropdown-toggle").has(target).length === 0) {
//                    $(dropdown).removeClass('show');
//                    $(parent).removeClass('show');
//                    $(btn).attr("aria-expanded", "false");
//                    $(dropdown).css("left", '');
//                    $(dropdown).css("top", '');
//                    $(dropdown).removeClass("align-right");
//                } else if ($(parent).hasClass('show')) {
//                    $(document).one('click', closeTooltip);
//                }
//            });
//        }
//        else {
//            $(dropdown).removeClass('show');
//            $(parent).removeClass('show');
//            $(dropdown).css("left", '');
//            $(dropdown).css("top", '');
//            $(btn).attr("aria-expanded", "false");
//        }
//    }
//});

$(".isubmenu-toggle").on("click", function (e) {
    e.preventDefault();
    var btn = this;
    var parent = $(btn).parent();
    var navbar = $(parent).parent();
    var dropdown = $(parent).children(".imenu-submenu");

    //dropdownMenus(btn);

    var menuVirtuaPos = ($(btn).offset().left + $(btn).width() + $(dropdown).width());
    console.log("btn offset:" + $(btn).offset().left + "btn width:" + $(btn).width() + "parent width:" + $(parent).width() + "dropdown width:" + $(dropdown).width() + "virtual:" + menuVirtuaPos);
    if (dropdown) {
        if (!$(parent).hasClass('show')) {
            $(dropdown).addClass('show');
            $(parent).addClass('show');
            $(btn).attr("aria-expanded", "true");
            if (menuVirtuaPos > $(window).innerWidth()) {
                $(dropdown).css("left", " -" + $(dropdown).width() + "px");
                $(dropdown).addClass("align-right");
            }
            $(dropdown).css("top", " " + ($(btn).offset().top - $(navbar).offset().top) + "px");
            console.log($(btn).offset().top + " - " + $(navbar).offset().top + " - " + ($(btn).offset().top - $(parent).offset().top));
            $(document).one('click', function closeTooltip(f) {
                var target = <any>f.target;
                if ($(parent).has(target).length === 0 && $("a.submenu-toggle").has(target).length === 0) {
                    $(dropdown).removeClass('show');
                    $(parent).removeClass('show');
                    $(btn).attr("aria-expanded", "false");
                    $(dropdown).css("left", '');
                    $(dropdown).css("top", '');
                    $(dropdown).removeClass("align-right");
                } else if ($(parent).hasClass('show')) {
                    $(document).one('click', closeTooltip);
                }
            });
        }
        else {
            $(dropdown).removeClass('show');
            $(parent).removeClass('show');
            $(dropdown).css("left", '');
            $(dropdown).css("top", '');
            $(btn).attr("aria-expanded", "false");
        }
    }
});