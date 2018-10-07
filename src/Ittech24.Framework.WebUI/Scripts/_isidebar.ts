var addResizeBar = function () {
    var menuPusher = $('.menu-pusher');
    var div = $('<div></div>');
    div.addClass("menu-pusher-resize");
    menuPusher.parent().append(div);
};

class iSidebar {
    menuPusher: JQuery<HTMLElement>;
    resizeBar: JQuery<HTMLElement>;

    private isidebar: JQuery<HTMLElement>;
    private containsHeader: boolean;
    private headerSection: JQuery<HTMLElement>;
    private headerStartHeight: number;
    private containsFooter: boolean;
    private footerSection: JQuery<HTMLElement>;
    private footerStartHeight: number;
    private contentSection: JQuery<HTMLElement>;
    private bodySection: JQuery<HTMLElement>;
    private containsFlexyContent: boolean;
    private flexyContent: JQuery<HTMLElement>;
    private ss1: JQuery<HTMLElement>
    private startX: number;
    private startY: number;
    private startWidth: number;
    private startHeight: number;
    private tSize: number;

    private submenu: iSidebarSubmenu;

    expanded: boolean;
    width: number;
    size: number;
    maxSize: number;

    constructor() {
        var self = this;
        this.submenu = new iSidebarSubmenu();
        $("[data-toggle='submenu']").click(function (e) {
            var target = $("[data-toggle='submenu']").attr("data-target");
            self.submenu.animateIn();
        });

        this.isidebar = $(".isidebar");
        // only constructs the sidebar if there is at least one isidebar class

        if (this.isidebar.length > 0) {
            // Getting the sidebar expanded status;
            this.expanded = this.isExpanded();
            // Looking for the header section
            this.headerSection = $('.isidebar-header');
            // Setting the initial calculated height value for the header if defined
            if (this.headerSection.length > 0) {
                this.containsHeader = true;
                this.headerStartHeight = this.headerSection.innerHeight();
                this.headerSection.css("height", this.headerStartHeight);
            }
            else {
                this.containsHeader = false;
            }
            // looking for the footer section
            this.footerSection = $('.isidebar-footer');
            if (this.footerSection.length > 0) {
                this.footerStartHeight = this.footerSection.innerHeight();
                this.footerSection.css("height", this.footerStartHeight);
                this.containsFooter = true;
            }
            else {
                this.containsFooter = false;
            }
            // looking for the flex content section
            this.flexyContent = $('.isidebar-flexy-content');
            if (this.flexyContent.length > 0) {
                this.containsFlexyContent = true;
            }
            else {
                this.containsFlexyContent = false;
            }
            // looking for the content section;
            this.contentSection = $('.isidebar-content');
            this.bodySection = $('.isidebar-body');

            this.menuPusher = $('.menu-pusher');
            this.resizeBar = $('.isidebar-resize');

            this.size = 64;
            this.tSize = 0;
            this.maxSize = 250;

            if (this.containsFlexyContent) {
                var thisMenu = this.menuPusher;
                // Creates menu pusher if it does not exist on the sidebar
                if (this.menuPusher.length === 0) {
                    this.createPusher();
                }
                // Creates resize bar if it does not exist on the sidebar
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
    };

    private createPusher() {
        var pusher = $("<div></div>");
        pusher.addClass("menu-pusher");
        this.menuPusher = pusher;
        this.isidebar.append(pusher);
    };

    private createResizer() {
        var resizer = $("<div></div>");
        resizer.addClass("resize");
        this.resizeBar = resizer;
        this.isidebar.append(resizer);
    };

    private createiSideBarSection() {
        var flexySection = $("<div></div>");
        flexySection.addClass("isidebar-section");
        this.ss1 = flexySection;
    }

    private createResizeEvents() {
        if (this.resizeBar.length > 0) {
            var main = this;
            $(main.resizeBar).mousedown(function (e) {
                main.doDown(e);
            });
            $(main.resizeBar).click(function (e) {
                main.doClick(e);
            });
        };
    };

    private isExpanded(): boolean {
        var result: boolean;
        try {
            result = (localStorage.getItem("isidebar.expanded").toLowerCase() == "true");
        }
        catch{
            result = false;
        }
        return result;
    }

    private doClick(e) {
        this.expanded = this.isExpanded();
        if (this.expanded) {
            if (this.flexyContent.length > 0) {
                //console.log("doClick contract");
                this.contractBody();
            }
        }
        else {
            if (this.flexyContent.length > 0) {
                this.flexyContent.append(this.bodySection);
                //console.log("doClick expand");
                this.expandBody();
            }
        }
    }

    private doDown(e) {
        var main = this;
        // Stop the drag propagation to other elements
        if (e.stopPropagation) e.stopPropagation();
        if (e.preventDefault) e.preventDefault();
        // Set some initial values
        this.startX = e.clientX;
        this.startY = e.clientY;
        this.startWidth = this.menuPusher.width();
        this.startHeight = this.menuPusher.height();
        // Setting the resize flags
        $(this.resizeBar).addClass("resizing");
        $(this.contentSection).addClass("expanding");
        // Setting the header section to 0 if exists
        if (this.containsHeader) {
            this.headerSection.css("height", "0");
        }
        if (this.containsFooter) {
            this.footerSection.css("height", "0");
        }
        //setting the other events on the document to perform the resize
        $(document).mousemove(function (e) {
            main.doDrag(e);
        });
        $(document).mouseup(function (e) {
            main.stopDrag(e);
        });
    }

    private doDrag(e) {
        this.width = (this.startWidth + e.clientX - this.startX);
        //console.log(`width ${this.width}`);
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

    private stopDrag(e) {        
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

    private expandBody() {
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
    }

    private contractBody() {
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
    }


}