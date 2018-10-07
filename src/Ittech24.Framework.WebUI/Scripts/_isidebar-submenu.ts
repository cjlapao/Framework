class iSidebarSubmenu {
    private parent: JQuery<HTMLElement>;
    private submenu: JQuery<HTMLElement>;
    private submenuBackButton: JQuery<HTMLElement>;
    private submenuBackBar: JQuery<HTMLElement>;

    constructor() {
        this.initializeSubmenu();
    }

    private initializeSubmenu() {
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
    }

    public animateIn() {
        this.submenu.animate({ width: "400px" }, "slow");
    }

    public animateOut() {
        this.submenu.animate({ width: 0 }, "slow");
    }
}