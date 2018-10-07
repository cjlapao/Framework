var common;
(function (common) {
    function getRandomNumber() {
        var max, min, rand;
        var float;
        switch (arguments.length) {
            case 0:
                rand = Math.random();
                break;
            case 1:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        max = arguments[i];
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null) {
                    rand = (Math.random() * max);
                }
                if (float != null && float == false) {
                    rand = Math.floor(Math.random());
                }
                if (float != null && float == true) {
                    rand = Math.random();
                }
                break;
            case 2:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        if (max == null) {
                            max = arguments[i];
                        }
                        else if (min == null) {
                            min = arguments[i];
                        }
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null && min == null) {
                    rand = (Math.random() * max);
                }
                if (max != null && min != null) {
                    rand = (Math.random() * (max - min)) + min;
                }
                if (float != null && float == false) {
                    rand = Math.floor(rand);
                }
                break;
            case 3:
                for (var i = 0; i < arguments.length; i++) {
                    if (typeof arguments[i] == "number") {
                        if (max == null) {
                            max = arguments[i];
                        }
                        else if (min == null) {
                            min = arguments[i];
                        }
                    }
                    if (typeof arguments[i] == "boolean") {
                        float = arguments[i];
                    }
                }
                if (max != null && min == null) {
                    rand = (Math.random() * max);
                }
                if (max != null && min != null) {
                    rand = (Math.random() * (max - min)) + min;
                }
                if (float != null && float == false) {
                    rand = Math.floor(rand);
                }
                break;
        }
        return rand;
    }
    common.getRandomNumber = getRandomNumber;
    function getNoise() {
        var value, noise, response;
        var float;
        noise = common.getRandomNumber(1.10, 0.9);
        for (var i = 0; i < arguments.length; i++) {
            if (typeof arguments[i] == "number") {
                value = arguments[i];
            }
            if (typeof arguments[i] == "boolean") {
                float = arguments[i];
            }
        }
        if (value != null && float != null && float == false) {
            response = value * noise;
            return Math.floor(response);
        }
        else if (value != null) {
            response = value * noise;
            return response;
        }
        return noise;
    }
    common.getNoise = getNoise;
    var themeColorValue;
    (function (themeColorValue) {
        themeColorValue["SkyBlue"] = "#305ee8";
        themeColorValue["TealGrey"] = "#a2b9bc";
        themeColorValue["GreenLeaf"] = "#b2ad7f";
        themeColorValue["YellowLight"] = "#feb236";
        themeColorValue["CherryTomato"] = "#E94B3C";
        themeColorValue["pureWhite"] = "#fefefe";
    })(themeColorValue = common.themeColorValue || (common.themeColorValue = {}));
    var themColor;
    (function (themColor) {
        themColor["skyBlue"] = "skyblue";
        themColor["tealGrey"] = "tealgrey";
        themColor["greenLeaf"] = "greenleaf";
        themColor["yellowLight"] = "yellowlight";
        themColor["cherryTomato"] = "cherrytomato";
        themColor["pureWhite"] = "purewhite";
    })(themColor = common.themColor || (common.themColor = {}));
})(common || (common = {}));

var FloatButton = (function () {
    function FloatButton() {
        $(".float-bar-btn").click(function () {
            $(this).toggleClass("active");
            $(".float-bar-icons").toggleClass("open");
        });
    }
    return FloatButton;
}());

var Notifications = (function () {
    function Notifications() {
    }
    Notifications.prototype.setBar = function () {
        alert("bar set");
    };
    return Notifications;
}());

var Clouds = (function () {
    function Clouds(arg) {
        this.background = '#1a75ff';
        this.colors = ["#FFFFFF", "#F5F5F5", "#E8E8E8", "#DCDCDC", "#D3D3D3"];
        this.svgCloudPath = "d=\"m 27.800094,0.5176531 c -4.508681,8e-4 -8.659763,2.26707 -10.833759,5.9149 -1.300998,-1.23082 -3.01559,-1.91713 -4.797755,-1.92041 -4.4138331,0.0181 -7.4086569,4.26005 -6.989501,7.8766699 C 2.0994322,13.968733 0,17.170473 0,20.882533 c 0,5.28775 4.2571845,9.54455 9.5449268,9.54455 H 38.080454 c 5.287744,0 9.544547,-4.2568 9.544547,-9.54455 0,-4.57491 -3.187135,-8.37613 -7.470845,-9.31845 C 39.93447,5.4081731 34.469479,0.5216331 27.800094,0.5176531 Z\"";
        if (arg) {
            if (arg.amount) {
                this.amount = arg.amount;
            }
            if (arg.noise) {
                this.noise = arg.noise;
            }
            if (arg.background) {
                this.background = arg.background;
            }
        }
        if (this.amount) {
            this.build(this._amount);
        }
    }
    Object.defineProperty(Clouds.prototype, "amount", {
        get: function () {
            return this._amount;
        },
        set: function (theAmount) {
            this._amount = theAmount;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(Clouds.prototype, "noise", {
        get: function () {
            return this._noise;
        },
        set: function (thisNoise) {
            this._noise = thisNoise;
        },
        enumerable: true,
        configurable: true
    });
    Clouds.prototype.build = function () {
        var arg = arguments[0];
        if (typeof arg != typeof undefined) {
            this.amount = arg;
        }
        else if (typeof this.amount == typeof undefined) {
            this.amount = 8;
        }
        var body = $('body');
        $('#svgContainer').remove();
        var container = $("<div></div");
        var mheight = body.innerHeight();
        var mwidth = body.innerWidth();
        container.addClass("svgContainer");
        if (this.noise) {
            container.css("background-image", "url('/lib/img/noise.png')");
        }
        container.css("position", "absolute");
        container.css("top", "0");
        container.css("left", "0");
        container.css("height", "100%");
        container.css("width", "100%");
        container.css("overflow", "hidden");
        container.css("background-color", this.background);
        var heightSlice = Math.floor(mheight / this.amount);
        for (var i = 0; i < this.amount; i++) {
            var noise = common.getNoise();
            console.log("noise:" + noise);
            var top = Math.floor((heightSlice * i) * noise);
            var halfLeft = Math.floor((mwidth / 2) * noise);
            var left = common.getRandomNumber(halfLeft, false);
            var atime = Math.floor(Math.random() * (30 - 15) + 15);
            var atime = common.getRandomNumber(30, 15, false);
            var opacity = common.getNoise(common.getRandomNumber(1, 0.7));
            var delay = common.getRandomNumber(2, -2);
            var scale = common.getNoise(common.getRandomNumber(1.5, 0.5));
            var color = this.getRandomColor();
            if (color == null)
                color = "#FFFFFF";
            var element = $("<div></div>");
            element.css('position', 'absolute');
            element.css('top', top + 'px');
            element.css('left', left + 'px');
            element.css('color', color);
            element.css('opacity', opacity);
            element.css('animation-delay', delay + 's');
            element.css('transform', 'scale(' + scale + ')');
            element.css('animation', 'moveclouds ' + atime + 's linear infinite');
            element.css('-webkit-animation', 'moveclouds ' + atime + 's  linear infinite');
            element.append(this.buildSvg());
            container.append(element);
        }
        body.append(container);
    };
    Clouds.prototype.getRandomColor = function () {
        var random = common.getRandomNumber(this.colors.length - 1, false);
        var color = this.colors[random];
        console.log("color " + color + "| rand" + random);
        return color;
    };
    Clouds.prototype.buildSvg = function () {
        var svg = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 50.270832 30.427084\" height=\"115\" width=\"190\"> <path fill=\"currentcolor\" " + this.svgCloudPath + " ></path> </svg>";
        return svg;
    };
    return Clouds;
}());

var DropdownPosition;
(function (DropdownPosition) {
    DropdownPosition[DropdownPosition["down"] = 0] = "down";
    DropdownPosition[DropdownPosition["left"] = 1] = "left";
    DropdownPosition[DropdownPosition["right"] = 2] = "right";
    DropdownPosition[DropdownPosition["top"] = 3] = "top";
})(DropdownPosition || (DropdownPosition = {}));
var Dropdown = (function () {
    function Dropdown() {
        this.items = $("li.idropdown");
        var self = this;
        $.each(this.items, function (index, item) {
            $(item).click(function (e) {
                var button = $(item).find(".idropdown-toggle");
                self.onDropdownClick(button[0], e);
            });
        });
    }
    Dropdown.prototype.collapseAll = function (btn) {
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
    };
    Dropdown.prototype.onDropdownClick = function (sender, e, pos) {
        if (pos === void 0) { pos = DropdownPosition.down; }
        e.preventDefault();
        var parent = $(sender).parent();
        var navbar = $(parent).parent();
        var dropdown = $(parent).children(".idropdown-menu");
        this.collapseAll(sender);
        var menuVirtuaPos;
        switch (pos) {
            case DropdownPosition.down:
                menuVirtuaPos = ($(sender).offset().left + $(dropdown).width());
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
                    var target = f.target;
                    if ($(parent).has(target).length === 0 && $("a.idropdown-toggle").has(target).length === 0) {
                        $(dropdown).removeClass('show');
                        $(parent).removeClass('show');
                        $(sender).attr("aria-expanded", "false");
                        $(dropdown).css("left", '');
                        $(dropdown).css("top", '');
                        $(dropdown).removeClass("align-right");
                    }
                    else if ($(parent).hasClass('show')) {
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
    };
    return Dropdown;
}());
$(".isubmenu-toggle").on("click", function (e) {
    e.preventDefault();
    var btn = this;
    var parent = $(btn).parent();
    var navbar = $(parent).parent();
    var dropdown = $(parent).children(".imenu-submenu");
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
                var target = f.target;
                if ($(parent).has(target).length === 0 && $("a.submenu-toggle").has(target).length === 0) {
                    $(dropdown).removeClass('show');
                    $(parent).removeClass('show');
                    $(btn).attr("aria-expanded", "false");
                    $(dropdown).css("left", '');
                    $(dropdown).css("top", '');
                    $(dropdown).removeClass("align-right");
                }
                else if ($(parent).hasClass('show')) {
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

var Helpers = (function () {
    function Helpers() {
        this.name = "test";
    }
    Helpers.prototype.getname = function () {
        return this.name;
    };
    return Helpers;
}());

var iSidebarSubmenu = (function () {
    function iSidebarSubmenu() {
        this.initializeSubmenu();
    }
    iSidebarSubmenu.prototype.initializeSubmenu = function () {
        this.parent = $(".isidebar");
        this.submenu = $(".isidebar-submenu");
        this.submenuBackBar = $(".isidebar-submenu-back-bar");
        this.submenuBackButton = $(".isidebar-submenu-back-btn");
        if (this.submenu.length == 0) {
            this.submenu = $("<div></div>");
            this.submenu.addClass("isidebar-submenu");
            this.submenuBackBar = $("<div></div>");
            this.submenuBackBar.addClass("isidebar-submenu-back-bar");
            this.submenuBackButton = $("<div></div>");
            this.submenuBackButton.addClass("isidebar-submenu-back-button");
            this.submenuBackBar.append(this.submenuBackButton);
            this.submenu.append(this.submenuBackBar);
            if (this.parent.length > 0) {
                this.parent.append(this.submenu);
            }
        }
        var self = this;
        this.submenuBackButton.click(function (e) {
            if (self.submenu.width() > 0) {
                self.animateOut();
            }
        });
    };
    iSidebarSubmenu.prototype.animateIn = function () {
        this.submenu.animate({ width: "400px" }, "slow");
    };
    iSidebarSubmenu.prototype.animateOut = function () {
        this.submenu.animate({ width: 0 }, "slow");
    };
    return iSidebarSubmenu;
}());

var addResizeBar = function () {
    var menuPusher = $('.menu-pusher');
    var div = $('<div></div>');
    div.addClass("menu-pusher-resize");
    menuPusher.parent().append(div);
};
var iSidebar = (function () {
    function iSidebar() {
        var self = this;
        this.submenu = new iSidebarSubmenu();
        $("[data-toggle='submenu']").click(function (e) {
            var target = $("[data-toggle='submenu']").attr("data-target");
            self.submenu.animateIn();
        });
        this.isidebar = $(".isidebar");
        if (this.isidebar.length > 0) {
            this.expanded = this.isExpanded();
            this.headerSection = $('.isidebar-header');
            if (this.headerSection.length > 0) {
                this.containsHeader = true;
                this.headerStartHeight = this.headerSection.innerHeight();
                this.headerSection.css("height", this.headerStartHeight);
            }
            else {
                this.containsHeader = false;
            }
            this.footerSection = $('.isidebar-footer');
            if (this.footerSection.length > 0) {
                this.footerStartHeight = this.footerSection.innerHeight();
                this.footerSection.css("height", this.footerStartHeight);
                this.containsFooter = true;
            }
            else {
                this.containsFooter = false;
            }
            this.flexyContent = $('.isidebar-flexy-content');
            if (this.flexyContent.length > 0) {
                this.containsFlexyContent = true;
            }
            else {
                this.containsFlexyContent = false;
            }
            this.contentSection = $('.isidebar-content');
            this.bodySection = $('.isidebar-body');
            this.menuPusher = $('.menu-pusher');
            this.resizeBar = $('.isidebar-resize');
            this.size = 64;
            this.tSize = 0;
            this.maxSize = 250;
            if (this.containsFlexyContent) {
                var thisMenu = this.menuPusher;
                if (this.menuPusher.length === 0) {
                    this.createPusher();
                }
                if (this.resizeBar.length === 0) {
                    this.createResizer();
                }
                this.menuPusher.on("mouseenter", function (e) {
                    thisMenu.addClass("open");
                });
                this.menuPusher.on("mouseleave", function (e) {
                    thisMenu.removeClass("open");
                });
                this.createResizeEvents();
            }
            if (this.expanded) {
                if (this.flexyContent.length > 0) {
                    this.flexyContent.append(this.bodySection);
                    this.expandBody();
                }
            }
        }
    }
    ;
    iSidebar.prototype.createPusher = function () {
        var pusher = $("<div></div>");
        pusher.addClass("menu-pusher");
        this.menuPusher = pusher;
        this.isidebar.append(pusher);
    };
    ;
    iSidebar.prototype.createResizer = function () {
        var resizer = $("<div></div>");
        resizer.addClass("resize");
        this.resizeBar = resizer;
        this.isidebar.append(resizer);
    };
    ;
    iSidebar.prototype.createiSideBarSection = function () {
        var flexySection = $("<div></div>");
        flexySection.addClass("isidebar-section");
        this.ss1 = flexySection;
    };
    iSidebar.prototype.createResizeEvents = function () {
        if (this.resizeBar.length > 0) {
            var main = this;
            $(main.resizeBar).mousedown(function (e) {
                main.doDown(e);
            });
            $(main.resizeBar).click(function (e) {
                main.doClick(e);
            });
        }
        ;
    };
    ;
    iSidebar.prototype.isExpanded = function () {
        var result;
        try {
            result = (localStorage.getItem("isidebar.expanded").toLowerCase() == "true");
        }
        catch (_a) {
            result = false;
        }
        return result;
    };
    iSidebar.prototype.doClick = function (e) {
        this.expanded = this.isExpanded();
        if (this.expanded) {
            if (this.flexyContent.length > 0) {
                this.contractBody();
            }
        }
        else {
            if (this.flexyContent.length > 0) {
                this.flexyContent.append(this.bodySection);
                this.expandBody();
            }
        }
    };
    iSidebar.prototype.doDown = function (e) {
        var main = this;
        if (e.stopPropagation)
            e.stopPropagation();
        if (e.preventDefault)
            e.preventDefault();
        this.startX = e.clientX;
        this.startY = e.clientY;
        this.startWidth = this.menuPusher.width();
        this.startHeight = this.menuPusher.height();
        $(this.resizeBar).addClass("resizing");
        $(this.contentSection).addClass("expanding");
        if (this.containsHeader) {
            this.headerSection.css("height", "0");
        }
        if (this.containsFooter) {
            this.footerSection.css("height", "0");
        }
        $(document).mousemove(function (e) {
            main.doDrag(e);
        });
        $(document).mouseup(function (e) {
            main.stopDrag(e);
        });
    };
    iSidebar.prototype.doDrag = function (e) {
        this.width = (this.startWidth + e.clientX - this.startX);
        if (this.bodySection.length > 0) {
            if (this.width >= (this.size + 3)) {
                if (this.containsHeader) {
                    this.headerSection.addClass("fill");
                }
                this.bodySection.find(".isidebar-section:first-child").addClass("fill");
                this.headerSection.css("height", this.headerStartHeight);
                this.footerSection.css("height", this.footerStartHeight);
                this.tSize = 0;
                this.flexyContent.append(this.bodySection);
            }
            if (this.width >= (this.size + 3) && this.width <= this.maxSize) {
                $(this.menuPusher).css("width", this.width + "px");
                if (this.width <= (this.size * 2)) {
                    if (!$(this.bodySection).hasClass("expanding")) {
                        $(this.bodySection).addClass("expanding");
                    }
                    if ($(this.bodySection).hasClass("expanded")) {
                        $(this.bodySection).removeClass("expanded");
                    }
                    $(this.bodySection).css("transform", "translateX(" + (this.width - this.size) + "px)");
                }
                else {
                    if ($(this.bodySection).hasClass("expanding")) {
                        $(this.bodySection).removeClass("expanding");
                    }
                    $(this.bodySection).css("transform", "");
                    $(this.bodySection).css("width", (this.width - this.size) + "px");
                    this.bodySection.addClass("expanded");
                    if (this.containsHeader) {
                        this.headerSection.addClass("fill");
                    }
                }
            }
        }
    };
    ;
    iSidebar.prototype.stopDrag = function (e) {
        $(document).off("mousemove");
        $(document).off("mouseup");
        this.contentSection.removeClass("expanding");
        this.bodySection.removeClass("expanding");
        this.resizeBar.removeClass("resizing");
        if (this.width < (this.maxSize * 0.75)) {
            this.contractBody();
        }
        else if (this.width >= (this.maxSize * 0.75)) {
            this.expandBody();
        }
    };
    ;
    iSidebar.prototype.expandBody = function () {
        this.menuPusher.css("width", this.maxSize + "px");
        this.bodySection.css("width", (this.maxSize - this.size) + "px");
        if ($(this.bodySection).hasClass("expanding")) {
            $(this.bodySection).removeClass("expanding");
        }
        if (!$(this.bodySection).hasClass("expanded")) {
            this.bodySection.addClass("expanded");
        }
        if (this.containsHeader) {
            this.headerSection.addClass("fill");
        }
        var childSections = this.bodySection.find(".isidebar-section");
        $.each(childSections, function (i, section) {
            $(section).addClass("expanded");
        });
        localStorage.setItem("isidebar.expanded", "true");
    };
    iSidebar.prototype.contractBody = function () {
        if (this.bodySection.hasClass("expanded")) {
            this.bodySection.removeClass("expanded");
        }
        this.menuPusher.css("width", "");
        this.bodySection.css("width", "");
        this.bodySection.css("transform", "");
        this.bodySection.find(".isidebar-section:first-child").removeClass("fill");
        this.bodySection.appendTo(this.contentSection);
        if (this.containsHeader) {
            this.headerSection.removeClass("fill");
            this.headerSection.css("height", this.headerStartHeight);
        }
        if (this.containsFooter) {
            this.footerSection.css("height", this.footerStartHeight);
        }
        var childSections = this.bodySection.find(".isidebar-section");
        $.each(childSections, function (i, section) {
            $(section).removeClass("expanded");
        });
        localStorage.setItem("isidebar.expanded", "false");
    };
    return iSidebar;
}());

var loadingPanel = (function () {
    function loadingPanel(args) {
        this.name = "double-fade";
        this.color = "skyblue";
        if (args) {
            if (args.color) {
                this.color;
            }
            if (args.name) {
                this.name = args.name;
            }
        }
        if (this.name) {
            this.build();
        }
    }
    loadingPanel.prototype.build = function () {
        var target = $('body');
        $(".loading-panel").remove();
        var container = $("<div></div>");
        container.addClass("loading-panel");
        container.attr("id", "loadingPanel");
        if (this.name === 'double-fade') {
            this.doubleFade(container);
        }
        else if (this.name === 'rotateplane') {
            this.foldingSquare(container);
        }
        else if (this.name === 'stretch') {
            this.stretch(container);
        }
        else if (this.name === 'bounce-ball') {
            this.bounceBall(container);
        }
        else if (this.name === 'bounce-dots') {
            this.bounceDots(container);
        }
        else if (this.name === 'dot-circle') {
            this.dotsCircle(container);
        }
        else if (this.name === 'cube-grid') {
            this.cubeGrid(container);
        }
        else if (this.name === 'fading-circle') {
            this.fadingCircle(container);
        }
        else {
            this.doubleFade(container);
        }
    };
    loadingPanel.prototype.doubleFade = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("double-bounce1");
        var child2 = $("<div></div>");
        child2.addClass("double-bounce2");
        spinner.append(child1);
        spinner.append(child2);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.foldingSquare = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("rotateplane");
        spinner.append(child1);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.stretch = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-stretch");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("rect1");
        spinner.append(child1);
        var child2 = $("<div></div>");
        child2.addClass("rect2");
        spinner.append(child2);
        var child3 = $("<div></div>");
        child3.addClass("rect3");
        spinner.append(child3);
        var child4 = $("<div></div>");
        child4.addClass("rect4");
        spinner.append(child4);
        var child5 = $("<div></div>");
        child5.addClass("rect5");
        spinner.append(child5);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.bounceBall = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-bounce-ball");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("dot1");
        var child2 = $("<div></div>");
        child2.addClass("dot2");
        spinner.append(child1);
        spinner.append(child2);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.bounceDots = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-bounce-dots");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("bounce-dot1");
        var child2 = $("<div></div>");
        child2.addClass("bounce-dot2");
        var child3 = $("<div></div>");
        child3.addClass("bounce-dot3");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.dotsCircle = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-dot-circle");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("dot-circle1");
        child1.addClass("circle-child");
        child1.addClass("spinner-before-color");
        var child2 = $("<div></div>");
        child2.addClass("dot-circle2");
        child2.addClass("circle-child");
        child2.addClass("spinner-before-color");
        var child3 = $("<div></div>");
        child3.addClass("dot-circle3");
        child3.addClass("circle-child");
        child3.addClass("spinner-before-color");
        var child4 = $("<div></div>");
        child4.addClass("dot-circle4");
        child4.addClass("circle-child");
        child4.addClass("spinner-before-color");
        var child5 = $("<div></div>");
        child5.addClass("dot-circle5");
        child5.addClass("circle-child");
        child5.addClass("spinner-before-color");
        var child6 = $("<div></div>");
        child6.addClass("dot-circle6");
        child6.addClass("circle-child");
        child6.addClass("spinner-before-color");
        var child7 = $("<div></div>");
        child7.addClass("dot-circle7");
        child7.addClass("circle-child");
        child7.addClass("spinner-before-color");
        var child8 = $("<div></div>");
        child8.addClass("dot-circle8");
        child8.addClass("circle-child");
        child8.addClass("spinner-before-color");
        var child9 = $("<div></div>");
        child9.addClass("dot-circle9");
        child9.addClass("circle-child");
        child9.addClass("spinner-before-color");
        var child10 = $("<div></div>");
        child10.addClass("dot-circle10");
        child10.addClass("circle-child");
        child10.addClass("spinner-before-color");
        var child11 = $("<div></div>");
        child11.addClass("dot-circle11");
        child11.addClass("circle-child");
        child11.addClass("spinner-before-color");
        var child12 = $("<div></div>");
        child12.addClass("dot-circle12");
        child12.addClass("circle-child");
        child12.addClass("spinner-before-color");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        spinner.append(child4);
        spinner.append(child5);
        spinner.append(child6);
        spinner.append(child7);
        spinner.append(child8);
        spinner.append(child9);
        spinner.append(child10);
        spinner.append(child11);
        spinner.append(child12);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.cubeGrid = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-cube-grid");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("grid-cube");
        child1.addClass("grid-cube1");
        spinner.append(child1);
        var child2 = $("<div></div>");
        child2.addClass("grid-cube");
        child2.addClass("grid-cube2");
        spinner.append(child2);
        var child3 = $("<div></div>");
        child3.addClass("grid-cube");
        child3.addClass("grid-cube3");
        spinner.append(child3);
        var child4 = $("<div></div>");
        child4.addClass("grid-cube");
        child4.addClass("grid-cube4");
        spinner.append(child4);
        var child5 = $("<div></div>");
        child5.addClass("grid-cube");
        child5.addClass("grid-cube5");
        spinner.append(child5);
        var child6 = $("<div></div>");
        child6.addClass("grid-cube");
        child6.addClass("grid-cube6");
        spinner.append(child6);
        var child7 = $("<div></div>");
        child7.addClass("grid-cube");
        child7.addClass("grid-cube7");
        spinner.append(child7);
        var child8 = $("<div></div>");
        child8.addClass("grid-cube");
        child8.addClass("grid-cube8");
        spinner.append(child8);
        var child9 = $("<div></div>");
        child9.addClass("grid-cube");
        child9.addClass("grid-cube9");
        spinner.append(child9);
        container.append(spinner);
        $('body').append(container);
    };
    loadingPanel.prototype.fadingCircle = function (container) {
        var spinner = $("<div></div>");
        spinner.addClass("spinner");
        spinner.addClass("spinner-fading-circle");
        spinner.addClass("spinner-" + this.color);
        var child1 = $("<div></div>");
        child1.addClass("fading-circle1");
        child1.addClass("fading-circle");
        child1.addClass("spinner-before-color");
        var child2 = $("<div></div>");
        child2.addClass("fading-circle2");
        child2.addClass("fading-circle");
        child2.addClass("spinner-before-color");
        var child3 = $("<div></div>");
        child3.addClass("fading-circle3");
        child3.addClass("fading-circle");
        child3.addClass("spinner-before-color");
        var child4 = $("<div></div>");
        child4.addClass("fading-circle4");
        child4.addClass("fading-circle");
        child4.addClass("spinner-before-color");
        var child5 = $("<div></div>");
        child5.addClass("fading-circle5");
        child5.addClass("fading-circle");
        child5.addClass("spinner-before-color");
        var child6 = $("<div></div>");
        child6.addClass("fading-circle6");
        child6.addClass("fading-circle");
        child6.addClass("spinner-before-color");
        var child7 = $("<div></div>");
        child7.addClass("fading-circle7");
        child7.addClass("fading-circle");
        child7.addClass("spinner-before-color");
        var child8 = $("<div></div>");
        child8.addClass("fading-circle8");
        child8.addClass("fading-circle");
        child8.addClass("spinner-before-color");
        var child9 = $("<div></div>");
        child9.addClass("fading-circle9");
        child9.addClass("fading-circle");
        child9.addClass("spinner-before-color");
        var child10 = $("<div></div>");
        child10.addClass("fading-circle10");
        child10.addClass("fading-circle");
        child10.addClass("spinner-before-color");
        var child11 = $("<div></div>");
        child11.addClass("fading-circle11");
        child11.addClass("fading-circle");
        child11.addClass("spinner-before-color");
        var child12 = $("<div></div>");
        child12.addClass("fading-circle12");
        child12.addClass("fading-circle");
        child12.addClass("spinner-before-color");
        spinner.append(child1);
        spinner.append(child2);
        spinner.append(child3);
        spinner.append(child4);
        spinner.append(child5);
        spinner.append(child6);
        spinner.append(child7);
        spinner.append(child8);
        spinner.append(child9);
        spinner.append(child10);
        spinner.append(child11);
        spinner.append(child12);
        container.append(spinner);
        $('body').append(container);
    };
    return loadingPanel;
}());

var SvgViewport = (function () {
    function SvgViewport() {
    }
    return SvgViewport;
}());
var Svg = (function () {
    function Svg() {
        this.cssClass = "test";
    }
    return Svg;
}());

var sideBar = new iSidebar();
var dropdownMenus = new Dropdown();
var floatButton = new FloatButton();
var initialize = function () {
    var test = "";
};
